using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Orders
{
    public sealed class OrderDetail : Entity
    {
        public Guid OrderId { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; private set; }
        public OrderDetail(Guid productId, int quantity) : base(Guid.NewGuid())
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            ProductId = productId;
            Quantity = quantity;
        }
        private OrderDetail() { }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            Quantity = newQuantity;
        }
    }
}
