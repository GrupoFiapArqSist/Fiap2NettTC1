using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Repositories;
using TicketNow.Infra.Data.Context;

namespace TicketNow.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User, int, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
