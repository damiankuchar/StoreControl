using Microsoft.EntityFrameworkCore;
using StoreControl.Infrastructure.Persistence;

namespace StoreControl.Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetPermissionAsync(Guid userId, CancellationToken cancellationToken)
        {
            var roles = await _dbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Roles)
                .ToListAsync(cancellationToken);

            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToList();
        }

        public async Task<List<string>> GetRoleAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Roles)
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
