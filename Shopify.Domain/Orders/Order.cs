using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Orders
{
    public sealed class Order : Entity
    {
        private Order(Guid id, List<OrderDetail> orderDetails, Guid userId, decimal totalCost, DateTime deliveryTime, DateTime createdDate, DeliveryAddress deliveryAddress) : base(id)
        {
            OrderDetails = orderDetails;
            UserId = userId;
            TotalCost = totalCost;
            DeliveryTime = deliveryTime;
            CreatedDate = createdDate;
            DeliveryAddress = deliveryAddress;
        }
        private Order() { }
        public List<OrderDetail> OrderDetails { get; private set; }
        public Guid UserId { get; private set; }
        public DeliveryAddress DeliveryAddress { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public DateTime DeliveryTime { get; private set; }

        public static Order CreateOrder(List<OrderDetail> orderDetails, Guid userId, decimal totalOrderPrice, DateTime deliveryTime, DeliveryAddress delieveryAddress)
        {
            var order = new Order(Guid.NewGuid(), orderDetails, userId, totalOrderPrice, deliveryTime, DateTime.Now, delieveryAddress);
            return order;
        }

        public void UpdateOrder(decimal totalCost, List<OrderDetail> orderDetails, DateTime deliveryTime, DeliveryAddress deliveryAddress)
        {
            if (totalCost < 0)
            {
                throw new ArgumentException("Total cost cannot be negative.");
            }
            foreach (var newDetail in orderDetails)
            {
                var existingDetail = OrderDetails.FirstOrDefault(od => od.ProductId == newDetail.ProductId);

                if (existingDetail is null)
                {
                    OrderDetails.Add(new OrderDetail(newDetail.ProductId, newDetail.Quantity));
                }
                else
                {
                    existingDetail.UpdateQuantity(newDetail.Quantity);
                }
            }
            var detailsToRemove = OrderDetails
                 .Where(existingDetail => !orderDetails.Any(newDetail => newDetail.ProductId == existingDetail.ProductId))
                 .ToList();

            foreach (var detailToRemove in detailsToRemove)
            {
                OrderDetails.Remove(detailToRemove);
            }
            TotalCost = totalCost;
            DeliveryTime = deliveryTime;
            UpdatedDate = DateTime.Now;
            DeliveryAddress = deliveryAddress;
        }


    }
}
