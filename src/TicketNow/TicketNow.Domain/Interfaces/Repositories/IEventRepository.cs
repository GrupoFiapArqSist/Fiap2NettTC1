using TicketNow.Domain.Entities;

namespace TicketNow.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event, int>
    {
        Task<Event> ExistsByName(string name);
        Task<Event> SelectByIds(int idEvent, int promoterId);
        Task<bool> ExistsByEventIdAndPromoterId(int idEvent, int promoterId);
    }
}
