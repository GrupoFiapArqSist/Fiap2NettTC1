using System.Text.Json.Serialization;
using TicketNow.Domain.Enums;

namespace TicketNow.Domain.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public int PaymentMethod { get; set; }
        public long Tickets { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public long TicketsAvaiable { get; set; }
        [JsonIgnore]
        public bool EventActive { get; set; } = true;
        public List<OrderItemDto> OrderItemDto { get; set; }
    }
}
