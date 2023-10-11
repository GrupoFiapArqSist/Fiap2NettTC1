using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TicketNow.Domain.Entities;
using TicketNow.Domain.Interfaces.Entities;
using TicketNow.Domain.Enums;

namespace TicketNow.Infra.Data.Mapping
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(prop => prop.Id);

            #region Fields            
            builder.Property(prop => prop.Name)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("Name")
                   .HasColumnType("varchar(150)");

            builder.Property(prop => prop.Address)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("Address")
                   .HasColumnType("varchar(150)");

            builder.Property(prop => prop.City)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("City")
                   .HasColumnType("varchar(50)");

            builder.Property(prop => prop.UF)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("UF")
                   .HasColumnType("varchar(2)");

            builder.Property(prop => prop.Description)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("Description")
                   .HasColumnType("varchar(max)");

            builder.Property(prop => prop.EventTime)
                   .HasConversion(prop => prop.ToString(), prop => prop)
                   .IsRequired()
                   .HasColumnName("EventTime")
                   .HasColumnType("varchar(5)");

            builder.Property(e => e.Category)
                   .HasMaxLength(50)
                   .HasConversion(
                       v => v.ToString(),
                       v => (Category)Enum.Parse(typeof(Category), v))
                       .IsUnicode(false);

            builder.Property(p => p.TicketPrice)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)"); 
            #endregion

            #region Maps
            builder.HasOne(e => e.Promoter)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.PrometerId)
                .HasPrincipalKey(e => e.Id);

            builder.HasMany(e => e.Orders)
                   .WithOne(e => e.Event)
                   .HasForeignKey(e => e.EventId)
                   .HasPrincipalKey(e => e.Id);
            #endregion
        }
    }
}
