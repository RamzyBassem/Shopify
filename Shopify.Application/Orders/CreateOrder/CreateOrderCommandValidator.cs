using FluentValidation;
using Shopify.Application.Orders.Shared;

namespace Shopify.Application.Orders.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("UserId is required.")
                 .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

            RuleFor(x => x.DeliveryDate)
                .GreaterThan(DateTime.Now).WithMessage("Delivery date must be in the future.");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Order must contain at least one order detail.");

            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailValidator());

        }
    }
}
