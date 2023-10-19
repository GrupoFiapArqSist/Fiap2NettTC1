using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem, int, ApplicationDbContext>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
