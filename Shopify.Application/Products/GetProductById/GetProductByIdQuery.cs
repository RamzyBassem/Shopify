using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Products.Shared;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.GetProductById
{
    public sealed record GetProductByIdQuery(Guid productId) : IQuery<ProductResponse>;
}
