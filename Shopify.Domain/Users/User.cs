using Shopify.Domain.Abstraction;

namespace Shopify.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id, UserName userName, PasswordHash passwordHash, FirstName firstName, LastName lastName, Email email, Phone phone, Role role) : base(id)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Role = role;
            PasswordHash = passwordHash;
        }
        private User() { }

        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public UserName UserName { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Phone Phone { get; private set; }
        public Role Role { get; private set; }

        public static User Create(UserName userName, PasswordHash passwordHash, FirstName firstName, LastName lastName, Email email, Phone phone, Role role)
        {
            var user = new User(Guid.NewGuid(), userName, passwordHash, firstName, lastName, email, phone, role);
            return user;
        }
    }
}
