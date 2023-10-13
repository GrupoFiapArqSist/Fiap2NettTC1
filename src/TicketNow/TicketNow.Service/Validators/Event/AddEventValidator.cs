using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketNow.Domain.Dtos.Event;

namespace TicketNow.Service.Validators.Event
{
    public class AddEventValidator : AbstractValidator<AddEventDto>
    {
        public AddEventValidator() 
        {
            RuleFor(x => x.PromoterId)
                .NotEmpty().WithMessage("Informe o id do promoter do evento");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Informe o nome do evento");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Informe o endereço do evento");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Informe a cidade");

            RuleFor(x => x.UF)
                .NotNull().WithMessage("Informe a UF");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Informe a descrição do evento");

            RuleFor(x => x.EventTime)
                .NotEmpty().WithMessage("Informe o horário do evento");

            RuleFor(x => x.EventDate)
                .NotEmpty().WithMessage("Informe a data do evento");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Informe a categoria do evento");

            RuleFor(x => x.TicketPrice)
                .NotEmpty().WithMessage("Informe o preço do ingresso");

            RuleFor(x => x.TicketAmount)
                .NotEmpty().WithMessage("Informe a quantidade de ingressos disponíveis");
        }
    }
}
