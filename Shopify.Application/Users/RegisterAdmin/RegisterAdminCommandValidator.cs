using FluentValidation;

namespace Shopify.Application.Users.RegisterAdmin;

internal sealed class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidator()
    {
        RuleFor(c => c.FirstName)
                 .NotEmpty()
                 .Matches("^[a-zA-Z]+$").WithMessage("First name must contain only alphabetic characters");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .Matches("^[a-zA-Z]+$").WithMessage("Last name must contain only alphabetic characters");

        RuleFor(c => c.UserName)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Username must be alphanumeric");

        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters");

        RuleFor(c => c.Phone)
            .NotEmpty()
            .Matches("^[0-9]+$").WithMessage("Phone number must be numeric");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email format");
    }
}
