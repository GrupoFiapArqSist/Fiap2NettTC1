using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Repositories
{
    public class ApplicationRepository : BaseRepository<Application, int, ApplicationDbContext>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
