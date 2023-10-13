using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketNow.Domain.Entities;

namespace TicketNow.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IBaseRepository<Event, int>
    {
        Task<Event> ExistsByName(string name);
        Task<Event> SelectByIds(int idEvent, int promoterId);
    }
}
