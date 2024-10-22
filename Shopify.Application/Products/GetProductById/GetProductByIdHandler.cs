using Shopify.Application.Abstractions;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Application.Products.Shared;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Products;

namespace Shopify.Application.Products.GetProductById
{
    public sealed class GetProductByIdHandler(IProductRepository productRepository, IStorageService storageService) : IQueryHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository productRepository = productRepository;
        private readonly IStorageService storageService = storageService;

        public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken ct)
        {
            var product = await productRepository.GetByIdAsync(request.productId, ct);
            if (product is null)
                return Result.Failure<ProductResponse>(ProductErrors.NotFound);

            byte[]? image = null;
            if (product.ImagePath is not null && !string.IsNullOrEmpty(product.ImagePath.Value))
            {
                 image = await storageService.GetFileAsync(product.ImagePath.Value);
            }

            return Result.Success<ProductResponse>(new ProductResponse
            {
                Id = product.Id,
                Merchant = product.Merchant.Value,
                Name = product.Name.Value,
                Description = product.Description.Value,
                Price = product.Price,
                Image = image
            });
        }
    }
}
