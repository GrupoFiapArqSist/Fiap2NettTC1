using TicketNow.Domain.Dtos.Default;
using TicketNow.Domain.Dtos.MockPayment;
using TicketNow.Domain.Dtos.Order;
using TicketNow.Domain.Entities;

namespace TicketNow.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<DefaultServiceResponseDto> InsertNewOrderAsync(OrderDto newOrderDto);
        Task<PaymentsDto> RequestMockApiPaymentsAsync(Order orderDb);
        List<OrderDto> GetUserOrders(int idUser);
        Task<DefaultServiceResponseDto> ProcessPaymentsNotificationAsync(PaymentsDto paymentDto);
        Task<DefaultServiceResponseDto> CancelOrderByUserAsync(int userId, int eventId);

    }
}
