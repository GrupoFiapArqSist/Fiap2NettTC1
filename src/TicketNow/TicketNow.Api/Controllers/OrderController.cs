using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.MockPayment;
using TicketNow.Domain.Dtos.Order;
using TicketNow.Domain.Extensions;
using TicketNow.Domain.Filters;
using TicketNow.Domain.Interfaces.Services;
using TicketNow.Infra.CrossCutting.Notifications;

namespace TicketNow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new order")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<Notification>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> NewOrder([FromBody] AddOrderDto AddOrderDto)
        {
            var newOrderDto = _mapper.Map<OrderDto>(AddOrderDto);
            
            newOrderDto.UserId = this.GetUserIdLogged();
            var response = await _orderService.InsertNewOrderAsync(newOrderDto, AddOrderDto.AddOrderItemDto);
            return Ok(response);
       
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get orders by id user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(List<OrderDto>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetOrders([FromQuery] OrderFilter filter)
        {
            var ltOrder = _orderService.GetUserOrders(filter, this.GetUserIdLogged());
            if (ltOrder is null || ltOrder.Count.Equals(0)) return NotFound(new DefaultServiceResponseDto() { Message = "Nenhum pedido foi encontrado", Success = true });
           
            return Ok(ltOrder);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("webhook/payments")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ReceivePaymentsNotification(PaymentsDto paymentsDto)
        {
            return Ok(await _orderService.ProcessPaymentsNotificationAsync(paymentsDto));
        }
        
        [HttpDelete]
        [SwaggerOperation(Summary = "Cancel order by user")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CancelOrder(int idOrder)
        {
            return Ok(await _orderService.CancelOrderByUserAsync(this.GetUserIdLogged(), idOrder));
        }
    }
}