using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Orders.DeleteOrder
{
    public sealed record DeleteOrderCommand(Guid OrderId) : ICommand;

}
