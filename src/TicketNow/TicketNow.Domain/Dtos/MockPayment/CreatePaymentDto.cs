using TicketNow.Domain.Enums;

namespace TicketNow.Domain.Dtos.MockPayment
{
    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }

        public static PaymentMethodEnum ReturnPaymentMethodEnum(int paymentMethodId)
        {
            switch (paymentMethodId)
            {
                case 1:
                    return PaymentMethodEnum.Ticket;
                case 2:
                    return PaymentMethodEnum.Pix;
                case 3:
                    return PaymentMethodEnum.CreditCard;

                default:
                    return PaymentMethodEnum.CreditCard;
            }
        }
    }
}