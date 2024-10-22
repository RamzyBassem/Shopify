using Shopify.Application.Abstractions.Messaging;

namespace Shopify.Application.Users.RegisterAdmin;

public sealed record RegisterAdminCommand(string Email, string UserName, string FirstName, string LastName, string Password, string Phone)
    : ICommand<Guid>;
