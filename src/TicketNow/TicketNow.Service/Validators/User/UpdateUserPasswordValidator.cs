using FluentValidation;
using TicketNow.Domain.Dtos.User;

namespace TicketNow.Service.Validators.User
{
    public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordDto>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(p => p.NewPassword).ValidPassword();
        }
    }
}
