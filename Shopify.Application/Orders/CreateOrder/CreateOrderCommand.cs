using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Orders.Shared;
using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.CreateOrder
{
    public record CreateOrderCommand(List<OrderDetailDTO> OrderDetails, Guid UserId, DateTime DeliveryDate,string DelieveryAddress) : ICommand<Guid>;
}
