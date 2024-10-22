using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.DeleteProduct
{
    public sealed class DeleteProductCommandHandler(IProductRepository productRepository,IUnitOfWork unitOfWork) : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken ct)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId, ct);
            if (product == null)
                return Result.Failure(ProductErrors.NotFound);

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
