using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.RoleService
{
    public interface IRoleService
    {
        Task<bool> IsRoleUniqueAsync(Role role, CancellationToken cancellationToken);
    }
}
