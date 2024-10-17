using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Products
{
    public sealed class Product : Entity
    {
        public Product(
            Guid id,
            Name name,
            Description description,
            decimal price,
            Merchant merchant
            )
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Merchant = merchant;

        }
        public Name Name { get; private set; }
        public Description Description { get; private set; }
        public decimal Price { get; set; }
        public Merchant Merchant { get; private set; }

        //TODO Blob ?
    }
}
