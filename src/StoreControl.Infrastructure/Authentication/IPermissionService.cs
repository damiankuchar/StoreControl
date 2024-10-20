namespace StoreControl.Infrastructure.Authentication
{
    public interface IPermissionService
    {
        Task<List<string>> GetPermissionAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<string>> GetRoleAsync(Guid userId, CancellationToken cancellationToken);
    }
}
