using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Users;

namespace Shopify.Infrastructure.Repository
{
    public sealed class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        public async Task AddAsync(User user, CancellationToken ct)
        {
            await dbContext.Set<User>().AddAsync(user, ct);
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await dbContext.Set<User>().FirstOrDefaultAsync(user => user.Id == id, ct);
        }

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<User>().FirstOrDefaultAsync(u => u.UserName == new UserName(userName), cancellationToken);
        }

        public async Task<bool> UserExistsByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<User>().AnyAsync(u => u.Email == new Email(email), cancellationToken);
        }

        public async Task<bool> UserExistsByUserName(string userName, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<User>().AnyAsync(u => u.UserName == new UserName(userName), cancellationToken);
        }
    }
}
