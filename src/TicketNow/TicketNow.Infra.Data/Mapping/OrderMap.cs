using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketNow.Domain.Entities;

namespace TicketNow.Infra.Data.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(prop => prop.Id);

            builder.HasOne(e => e.User)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(e => e.UserId)
                   .HasPrincipalKey(e => e.Id);

            builder.HasOne(e => e.Event)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(e => e.EventId)
                   .HasPrincipalKey(e => e.Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.OrderItens)
                   .WithOne(e => e.Order)
                   .HasForeignKey(e => e.OrderId)
                   .HasPrincipalKey(e => e.Id);               
        }
    }
}
