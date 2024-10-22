using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(
         Guid productId,
         string Name,
         string Description,
         decimal Price,
         string Merchant
       ) : ICommand;
}
