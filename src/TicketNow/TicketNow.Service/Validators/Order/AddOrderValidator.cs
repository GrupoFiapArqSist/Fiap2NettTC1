using FluentValidation;
using TicketNow.Domain.Dtos.Order;

namespace TicketNow.Service.Validators.Order
{
    public class AddOrderValidator : AbstractValidator<OrderDto>
    {
        public AddOrderValidator()
        {        
            RuleFor(x => x.EventId)
                .NotEmpty().WithMessage("Informe o id do evento");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Informe o metodo de pagamento");

            RuleFor(x => x.Tickets)
                .NotEmpty().WithMessage("Informe a quantidade de tickets que deseja comprar");
            
            RuleFor(x => x.Tickets)
                .LessThanOrEqualTo(x => x.TicketsAvaiable).WithMessage("Quantidade de tickets selecionado é maior do que tem disponível.");

            RuleFor(x => x.EventActive)
                .Equal(true).WithMessage("Evento indisponível.");
        }
    }
}