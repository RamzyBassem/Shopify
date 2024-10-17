using Shopify.Domain.Abstraction;
using Shopify.Domain.Users.Events;

namespace Shopify.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id, UserName userName, FirstName firstName, LastName lastName, Email email) : base(id)
        {
            UserName = userName;
        }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public UserName UserName { get; private set; }
        public static User Create(UserName userName, FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), userName, firstName, lastName, email);
            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));
            return user;
        }
    }
}
