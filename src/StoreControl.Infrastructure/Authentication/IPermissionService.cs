namespace StoreControl.Infrastructure.Authentication
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionAsync(Guid userId, CancellationToken cancellationToken);
    }
}
