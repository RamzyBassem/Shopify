namespace Shopify.Domain.Orders
{
    public sealed record OrderDetail
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public OrderDetail(Guid productId, int quantity)
        { 
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
        }
    }
}
