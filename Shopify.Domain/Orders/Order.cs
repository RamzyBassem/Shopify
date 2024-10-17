using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Orders
{
    public sealed class Order : Entity
    {
        private Order(Guid id, List<OrderDetail> orderDetails, Guid userId, decimal totalCost, DateTime deliveryTime) : base(id)
        {
            OrderDetails = orderDetails;
            UserId = userId;
            TotalCost = totalCost;
            DeliveryTime = deliveryTime;
        }
        public List<OrderDetail> OrderDetails { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeliveryTime { get; set; }

        public static Order CreateOrder

    }
}
