using Microsoft.EntityFrameworkCore;
using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Repositories
{
    public class EventRepository : BaseRepository<Event, int, ApplicationDbContext>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByEventIdAndPromoterId(int idEvent, int promoterId)
        {
            return await _dataContext.Event.AnyAsync(t => t.Id == idEvent && 
                                                     t.PromoterId == promoterId);
        }

        public async Task<Event> ExistsByName(string name)
        {
            return await _dataContext.Event.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<Event> SelectByIds(int idEvent, int promoterId)
        {
            return await _dataContext.Event.FirstOrDefaultAsync(t => t.Id == idEvent && t.PromoterId == promoterId);
        }
    }
}
