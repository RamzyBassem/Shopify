using FluentValidation;

namespace Shopify.Application.Users.Login
{
    public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
