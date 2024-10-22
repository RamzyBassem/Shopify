using Shopify.Domain.Orders;

namespace Shopify.Application.Orders.Shared
{
    public sealed class OrderResponse
    {
        public Guid Id { get; init; }
        public List<OrderDetailDTO> OrderDetailResponse { get; init; } = new List<OrderDetailDTO>();
        public Guid UserId { get; init; }
        public decimal TotalCost { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime UpdatedDate { get; init; }
        public DateTime DeliveryTime { get; init; }
        public string DeliveryAddress { get; init; } = string.Empty;

    }
}