using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _dbContext;

        public UserService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsUserUniqueAsync(User user, CancellationToken cancellationToken)
        {
            var isUserUnique = await _dbContext.Users
                .AnyAsync(x => x.Username == user.Username || x.Email == user.Email, cancellationToken);

            return !isUserUnique;
        }
    }
}
