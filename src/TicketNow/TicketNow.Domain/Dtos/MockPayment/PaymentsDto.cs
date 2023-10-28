using TicketNow.Domain.Enums;

namespace TicketNow.Domain.Dtos.MockPayment
{
    public class PaymentsDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int OrderId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}