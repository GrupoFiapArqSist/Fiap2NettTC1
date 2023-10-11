using FluentValidation;
using TicketNow.Domain.Dtos.Auth;

namespace TicketNow.Service.Validators.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Informe o usuario");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Informe o primeiro nome");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe o email")
                .EmailAddress().WithMessage("Informe um email valido");

            RuleFor(x => x.DocumentType)
                .NotNull().WithMessage("Informe o tipo de documento");

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("Informe o documento");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Informe a senha")
                    .MinimumLength(8).WithMessage("Sua senha deve conter no minimo 8 caracteres.")
                    .Matches(@"[A-Z]+").WithMessage("Sua senha deve conter no minimo uma letra maiuscula.")
                    .Matches(@"[a-z]+").WithMessage("Sua senha deve conter no minimo uma letra minuscula.")
                    .Matches(@"[0-9]+").WithMessage("Sua senha deve conter no minimo uma numero.")
                    .Matches("(\\W)+").WithMessage("Sua senha deve conter no minimo um caractere especial.");
        }
    }
}
