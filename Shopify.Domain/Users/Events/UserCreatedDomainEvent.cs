using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UerId) : IDomainEvent;
}
