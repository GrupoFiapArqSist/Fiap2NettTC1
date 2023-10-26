using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.MockPayment;
using TicketNow.Domain.Dtos.Order;
using TicketNow.Domain.Entities;
using TicketNow.Domain.Filters;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<DefaultServiceResponseDto> InsertNewOrderAsync(OrderDto newOrderDto, List<AddOrderItemDto> addOrderItemDtos);
        Task<PaymentsDto> RequestMockApiPaymentsAsync(Order orderDb);
        List<OrderDto> GetUserOrders(OrderFilter filter, int idUser);
        Task<DefaultServiceResponseDto> ProcessPaymentsNotificationAsync(PaymentsDto paymentDto);
        Task<DefaultServiceResponseDto> CancelOrderByUserAsync(int userId, int eventId);

    }
}
