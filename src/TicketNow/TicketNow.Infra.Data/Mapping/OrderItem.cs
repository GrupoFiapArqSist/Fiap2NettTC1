using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketNow.Domain.Entities;

namespace TicketNow.Infra.Data.Mapping
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(prop => prop.Id);

            #region Fields
            builder.Property(prop => prop.Name)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("varchar(150)");

            builder.Property(prop => prop.Name)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()                   
                   .HasColumnName("Email")
                   .HasColumnType("varchar(150)");
            #endregion

            #region Maps            
            builder.HasOne(e => e.Order)
                   .WithMany(e => e.OrderItens)
                   .HasForeignKey(e => e.OrderId)
                   .HasPrincipalKey(e => e.Id);
            #endregion
        }
    }
}
