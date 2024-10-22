namespace Shopify.Domain.Abstraction
{
    public abstract class Entity
    {
        protected Entity(Guid Id)
        {
            this.Id = Id;
        }
        protected Entity()
        {

        }
        public Guid Id { get; init; }
    }
}
