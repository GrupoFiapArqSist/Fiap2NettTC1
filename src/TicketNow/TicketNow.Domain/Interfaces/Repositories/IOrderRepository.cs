using TicketNow.Domain.Entities;

namespace TicketNow.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order, int>
    {
        Task<Order> InsertWithReturnId(Order obj);
    }
}
