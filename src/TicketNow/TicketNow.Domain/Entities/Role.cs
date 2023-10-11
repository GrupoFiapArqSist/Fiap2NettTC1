using Microsoft.AspNetCore.Identity;
using TicketNow.Domain.Interfaces.Entities;

namespace TicketNow.Domain.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public Role(string roleName)
        {
            Name = roleName;
            NormalizedName = roleName;
        }
        public Role() { }
    }
}