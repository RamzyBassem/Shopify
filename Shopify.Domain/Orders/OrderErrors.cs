using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Orders
{
    public static class OrderErrors
    {
        public static Error NotFound = new(
            "Order.Found",
            "The Order with the specified identifier was not found");
    }
}
