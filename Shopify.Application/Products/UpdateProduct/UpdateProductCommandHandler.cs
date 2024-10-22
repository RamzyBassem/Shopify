using Shopify.Application.Abstractions;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.UpdateProduct
{
    public sealed class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IStorageService storageService) : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IStorageService storageService = storageService;

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken ct)
        {
            var existingProduct = await productRepository.GetByIdAsync(request.productId, ct);
            if (existingProduct == null)
            {
                return Result.Failure(ProductErrors.NotFound);
            }

            bool nameChanged = existingProduct.Name.Value != request.Name;

            if (nameChanged && await productRepository.ProductNameExistsAsync(request.Name, ct))
            {
                return Result.Failure(ProductErrors.NameAlreadyExists);
            }

            
            existingProduct.Update(
                new Name(request.Name),
                new Description(request.Description),
                request.Price,
                new Merchant(request.Merchant)
                 );

            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success(existingProduct.Id);
        }
    }
}
