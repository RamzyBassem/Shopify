using Shopify.Domain.Products;

namespace Shopify.Domain.Orders
{
    public sealed class PricingService
    {
        public decimal CalculateOrderTotalPrice(List<(Product Product, int Quantity)> productDetails)
        {
            decimal totalPrice = 0;

            foreach (var detail in productDetails)
            {
                totalPrice += detail.Product.Price * detail.Quantity;
            }

            return totalPrice;
        }
    }
}
