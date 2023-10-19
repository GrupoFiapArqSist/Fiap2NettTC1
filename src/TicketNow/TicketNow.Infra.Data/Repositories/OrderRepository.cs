using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order, int, ApplicationDbContext>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<Order> InsertWithReturnId(Order obj)
        {
            _dataContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await _dataContext.SaveChangesAsync();

            obj.User = _dataContext.Set<User>().Find(obj.UserId);
            obj.Event = _dataContext.Set<Event>().Find(obj.EventId);

            return obj;
        }
    }
}
