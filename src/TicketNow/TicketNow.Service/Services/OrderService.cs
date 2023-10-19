using AutoMapper;
using Microsoft.Extensions.Configuration;
using RestSharp;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.MockPayment;
using TicketNow.Domain.Dtos.Order;
using TicketNow.Domain.Entities;
using TicketNow.Domain.Enums;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Domain.Interfaces.Services;
using TicketNow.Infra.CrossCutting.Notifications;
using TicketNow.Service.Validators.Order;

namespace TicketNow.Service.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly NotificationContext _notificationContext;
        private static string _apiKey;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMapper mapper, IConfiguration configuration,
            NotificationContext notificationContext, IEventRepository eventRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
            _configuration = configuration;
            _notificationContext = notificationContext;
            _eventRepository = eventRepository;
            _apiKey = StaticMockPaymentApiKey.ApiKey;
        }

        public async Task<DefaultServiceResponseDto> InsertNewOrderAsync(OrderDto newOrderDto)
        {
            _apiKey = StaticMockPaymentApiKey.ApiKey;
            var eventDb = _eventRepository.Select(newOrderDto.EventId);
            newOrderDto.EventActive = eventDb.Active;
            newOrderDto.TicketsAvaiable = eventDb.TicketAvailable;

            var validationResult = Validate(newOrderDto, Activator.CreateInstance<AddOrderValidator>());
            if (!validationResult.IsValid) 
            {
                _notificationContext.AddNotifications(validationResult.Errors);
                return default(DefaultServiceResponseDto);
            }
            
            var orderDb = _mapper.Map<Order>(newOrderDto);
            orderDb.CreatedAt = DateTime.Now;
            orderDb.Status = OrderStatusEnum.Active;
            orderDb.PaymentStatus = PaymentStatusEnum.WaitingPayment;
            orderDb.Price = eventDb.TicketPrice * newOrderDto.Tickets;

            var newOrderDb = await _orderRepository.InsertWithReturnId(orderDb);

            for (var ticket = 0; ticket < newOrderDto.Tickets; ticket++)
            {
                _orderItemRepository.Insert(new OrderItem(newOrderDb.Id, $"{newOrderDb.User.FirstName} {newOrderDb.User.LastName}", newOrderDb.User.Email)); 
            }

            var paymentResponse = await RequestMockApiPaymentsAsync(newOrderDb);

            newOrderDb.PaymentId = paymentResponse.Id;
            _orderRepository.Update(newOrderDb);
            await _orderRepository.SaveChangesAsync();

            if (!paymentResponse.PaymentStatus.Equals(PaymentStatusEnum.Unauthorized))
            {
                eventDb.TicketAvailable -= newOrderDto.Tickets;
                _eventRepository.Update(eventDb);
                await _eventRepository.SaveChangesAsync();

                if (paymentResponse.PaymentStatus.Equals(PaymentStatusEnum.Paid))
                {
                    return new DefaultServiceResponseDto()
                    {
                        Message = StaticNotifications.OrderSucess.Message,
                        Success = true
                    };
                }

                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.OrderSucessWaitingPayment.Message,
                    Success = true
                };
            }
            else
            {
                newOrderDb.Status = OrderStatusEnum.Canceled;
                _orderRepository.Update(newOrderDb);
                await _orderRepository.SaveChangesAsync();

                return new DefaultServiceResponseDto()
                {
                    Message = StaticNotifications.OrderSucessButPaymentUnauthorized.Message,
                    Success = false
                };
            }
        }

        public async Task<PaymentsDto> RequestMockApiPaymentsAsync(Order orderDb)
        {
            var url = _configuration.GetSection("MockPayment:UrlBase").Value.ToString() + _configuration.GetSection("MockPayment:Payment").Value.ToString();
            
            var client = new RestClient(url);
            var request = new RestRequest()
            {
                Method = Method.Post,
            }.AddHeader("API-KEY", _apiKey)
            .AddBody(new CreatePaymentDto()
            {
                OrderId = orderDb.Id,
                PaymentMethod = CreatePaymentDto.ReturnPaymentMethodEnum(orderDb.PaymentMethod)
            });

            return await client.PostAsync<PaymentsDto>(request);
        }

        public List<OrderDto> GetUserOrders(int idUser)
        {
            var ltOrderDb = _orderRepository.Select().Where(db => db.UserId.Equals(idUser)).ToList();
            return _mapper.Map<List<OrderDto>>(ltOrderDb);
        }

        public async Task<DefaultServiceResponseDto> ProcessPaymentsNotificationAsync(PaymentsDto paymentDto)
        {
            var orderDb = _orderRepository.Select(paymentDto.OrderId);

            orderDb.PaymentStatus = paymentDto.PaymentStatus;
            _orderRepository.Update(orderDb);
            await _orderRepository.SaveChangesAsync();

            if (paymentDto.PaymentStatus != PaymentStatusEnum.Paid)
            {
                orderDb.Status = OrderStatusEnum.Canceled;
                _orderRepository.Update(orderDb);

                orderDb.Event.TicketAvailable += orderDb.Tickets;
                _eventRepository.Update(orderDb.Event);

                await _orderRepository.SaveChangesAsync();
                await _eventRepository.SaveChangesAsync();
            }

            return new DefaultServiceResponseDto()
            {
                Message = StaticNotifications.PaymentsNotificationProcessSucess.Message,
                Success = true
            };
        }

        public async Task<DefaultServiceResponseDto> CancelOrderByUserAsync(int userId, int eventId)
        {
            var orderDb = _orderRepository.Select().Where(db => db.Event.Id.Equals(eventId) && db.UserId.Equals(userId))?.FirstOrDefault();

            if (orderDb is null) return new DefaultServiceResponseDto() { Message = StaticNotifications.CancelOrderByUserEventNotFound.Message, Success = false };
            if (orderDb.Status.Equals(0)) return new DefaultServiceResponseDto() { Message = StaticNotifications.CancelOrderByUserEventAlreadyCanceled.Message, Success = false };

            orderDb.Status = OrderStatusEnum.Canceled;

            _orderRepository.Update(orderDb);
            await _orderRepository.SaveChangesAsync();

            orderDb.Event.TicketAvailable += orderDb.Tickets;
            _eventRepository.Update(orderDb.Event);
            await _eventRepository.SaveChangesAsync();

            return new DefaultServiceResponseDto() { Message = StaticNotifications.OrderCanceledSucess.Message, Success = true };
        }
    }
}