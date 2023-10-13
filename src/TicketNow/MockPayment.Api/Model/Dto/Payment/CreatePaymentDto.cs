using MockPayment.Api.Model.Enums;

namespace MockPayment.Api.Model.Dto.Payment
{
    public class CreatePaymentDto
    {       
        public int OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }        
    }
}
