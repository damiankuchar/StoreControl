using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _dbContext;

        public RoleService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsRoleUniqueAsync(Role role, CancellationToken cancellationToken)
        {
            var isRoleUnique = await _dbContext.Roles
                .AnyAsync(x => x.Name != role.Name, cancellationToken);

            return isRoleUnique;
        }
    }
}
