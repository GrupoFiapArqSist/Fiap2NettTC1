using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketNow.Domain.Entities;
using TicketNow.Infra.Data.Mapping;

namespace TicketNow.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }        
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<SeedHistory> SeedHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(new OrderMap().Configure);            
            modelBuilder.Entity<OrderItem>(new OrderItemMap().Configure);            
            modelBuilder.Entity<Event>(new EventMap().Configure);            
        }
    }
}