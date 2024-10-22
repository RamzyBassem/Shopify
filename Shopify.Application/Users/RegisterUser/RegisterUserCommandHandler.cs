using Shopify.Application.Abstractions.Authentication;
using Shopify.Application.Abstractions.Messaging;
using Shopify.Domain.Abstraction;
using Shopify.Domain.Users;

namespace Shopify.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken ct)
    {
        if (await userRepository.UserExistsByEmail(command.Email, ct))
        {
            return Result.Failure<Guid>(UserErrors.EmailExists);
        }
        if (await userRepository.UserExistsByUserName(command.UserName, ct))
        {
            return Result.Failure<Guid>(UserErrors.UserNameExists);

        }

        var hashedPassword = new PasswordHash(passwordHasher.Hash(command.Password));

        var user = User.Create(new UserName(command.UserName), hashedPassword, new FirstName(command.FirstName), new LastName(command.LastName), new Email(command.Email), new Phone(command.Phone), Role.User);
        await userRepository.AddAsync(user, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return user.Id;
    }
}
