using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public PermissionService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsPermissionUniqueAsync(Permission permission, CancellationToken cancellationToken)
        {
            var isPermissionUnique = await _dbContext.Permissions
                .AnyAsync(x => x.Name ==  permission.Name, cancellationToken);

            return !isPermissionUnique;
        }
    }
}
