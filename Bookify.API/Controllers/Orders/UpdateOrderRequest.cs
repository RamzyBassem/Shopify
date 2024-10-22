using Shopify.Application.Orders.Shared;

namespace Bookify.API.Controllers.Order
{
    public sealed record UpdateOrderRequest(Guid OrderId,
         string DeliveryAddress,
         List<OrderDetailDTO> OrderDetails,
         Guid UserId,
         DateTime DeliveryTime);
}