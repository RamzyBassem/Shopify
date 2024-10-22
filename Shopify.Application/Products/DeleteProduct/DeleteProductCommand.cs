using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Products.DeleteProduct
{
    public sealed record DeleteProductCommand(Guid ProductId) : ICommand;
}
