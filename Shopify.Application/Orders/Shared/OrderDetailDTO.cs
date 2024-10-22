namespace Shopify.Application.Orders.Shared
{
    public sealed class OrderDetailDTO
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
    }
}