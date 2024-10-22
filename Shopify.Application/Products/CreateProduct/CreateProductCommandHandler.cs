using Shopify.Application.Abstractions;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.CreateProduct
{
    public sealed class CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IStorageService storageService) : ICommandHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository productRepository = productRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IStorageService storageService = storageService;

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken ct)
        {
            if (await productRepository.ProductNameExistsAsync(request.Name, ct))
            {
                return Result.Failure<Guid>(ProductErrors.NameAlreadyExists);
            }

            var name = new Name(request.Name);
            var description = new Description(request.Description);
            var merchant = new Merchant(request.Merchant);
            ImagePath? imagePath = null;
            if (request.Image is not null)
            {
                imagePath = new ImagePath(await storageService.SaveFileAsync(request.Image));
            }
            var product = new Product(Guid.NewGuid(), name, description, request.Price, merchant, imagePath);

            await productRepository.AddAsync(product, ct);

            await unitOfWork.SaveChangesAsync(ct);

            return Result.Success(product.Id);
        }
    }
}
