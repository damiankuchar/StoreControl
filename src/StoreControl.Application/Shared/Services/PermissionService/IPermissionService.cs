using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<bool> IsPermissionUniqueAsync(Permission permission, CancellationToken cancellationToken);
    }
}
