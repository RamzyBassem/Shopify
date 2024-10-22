namespace Shopify.Application.Products.Shared
{
    public sealed class ProductResponse
    {
        public Guid Id { get; init; }
        public decimal Price { get; init; }
        public required string Name { get; init; }
        public required string Merchant { get; init; }
        public string Description { get; init; } = string.Empty;
        public byte[]? Image { get; init; }

    }
}
