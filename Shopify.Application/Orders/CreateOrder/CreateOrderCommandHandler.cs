using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Orders;
using Shopify.Domain.Products;
using Shopify.Domain.Users;

namespace Shopify.Application.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler(
        IUserRepository userRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        PricingService pricingService)
        : ICommandHandler<CreateOrderCommand, Guid>
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IProductRepository productRepository = productRepository;
        private readonly IOrderRepository orderRepository = orderRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly PricingService pricingService = pricingService;

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, ct);
            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }
            var validatedProductDetails = new List<(Product Product, int Quantity)>();

            foreach (var detail in request.OrderDetails)
            {
                var product = await productRepository.GetByIdAsync(detail.ProductId, ct);
                if (product is null)
                {
                    return Result.Failure<Guid>(OrderErrors.NotFound);
                }
                validatedProductDetails.Add((product, detail.Quantity));
            }
            var totalPrice = pricingService.CalculateOrderTotalPrice(validatedProductDetails);
            var orderDetails = request.OrderDetails.Select(orderDetail => new OrderDetail(orderDetail.ProductId, orderDetail.Quantity)).ToList();

            var order = Order.CreateOrder(orderDetails, request.UserId, totalPrice, request.DeliveryDate, new DeliveryAddress(request.DelieveryAddress));
            await orderRepository.AddAsync(order, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return order.Id;

        }
    }
}
