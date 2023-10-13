using Microsoft.EntityFrameworkCore;
using MockPayment.Api.Model;

namespace MockPayment.Api
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }        

        public DbSet<Application> Applications => Set<Application>();
        public DbSet<Payment> Payments => Set<Payment>();
    }
}
