using FluentValidation;

namespace Penta.Service.ViewModels.Validation
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Password)
                .NotNull()
                .MaximumLength(50);
        }
    }
}
