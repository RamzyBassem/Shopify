using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.UpdateOrder
{
    public sealed record UpdateOrderCommand(Guid OrderId, Guid UserId, List<OrderDetailDTO> OrderDetails, DateTime DeliveryTime,string DeliveryAddress) : ICommand;
}
