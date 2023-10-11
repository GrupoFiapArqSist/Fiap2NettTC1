using TicketNow.Domain.Interfaces.Entities;

namespace TicketNow.Domain.Entities
{
    public class OrderItem : BaseEntity, IEntity<int>
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }        
        public virtual Order Order { get; set; }
    }
}