using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;
using Shopify.Domain.Products;

namespace Shopify.Application.Orders.UpdateOrder
{
    public class UpdateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork, PricingService pricingService) : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository orderRepository = orderRepository;
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly PricingService pricingService = pricingService;

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken ct)
        {
            var order = await orderRepository.GetByIdAsync(request.OrderId, ct);
            if (order is null)
            {
                return Result.Failure(OrderErrors.NotFound);
            }

            var validatedProductDetails = new List<(Product Product, int Quantity)>();

            foreach (var detail in request.OrderDetails)
            {
                var product = await productRepository.GetByIdAsync(detail.ProductId, ct);
                if (product is null)
                    return Result.Failure(ProductErrors.NotFound);

                validatedProductDetails.Add((product, detail.Quantity));
            }

            var totalPrice = pricingService.CalculateOrderTotalPrice(validatedProductDetails);
            var orderDetails = request.OrderDetails.Select(orderDetail => new OrderDetail(orderDetail.ProductId, orderDetail.Quantity)).ToList();

            order.UpdateOrder(totalPrice, orderDetails, order.DeliveryTime, new DeliveryAddress(request.DeliveryAddress));
            await unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}
