using TicketNow.Domain.Entities;

namespace TicketNow.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem, int>
    {
    }
}
