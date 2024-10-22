using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Abstraction;

namespace Shopify.Application.Orders.GetOrderById
{
    public sealed record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderResponse>;
}
