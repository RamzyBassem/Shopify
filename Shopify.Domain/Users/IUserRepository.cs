namespace Shopify.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<bool> UserExistsByEmail(string email, CancellationToken cancellationToken = default);
        Task<bool> UserExistsByUserName(string userName, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken ct);
    }
}
