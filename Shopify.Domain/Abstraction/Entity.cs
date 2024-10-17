namespace Shopify.Domain.Abstraction
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        protected Entity(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; init; }

        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        protected void RaiseDomainEvents(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
