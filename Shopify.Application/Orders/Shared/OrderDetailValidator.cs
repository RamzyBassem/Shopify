using FluentValidation;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.Shared
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailDTO>
    {
        public OrderDetailValidator()
        {
            RuleFor(detail => detail.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

            RuleFor(detail => detail.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
