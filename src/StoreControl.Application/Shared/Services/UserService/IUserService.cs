using StoreControl.Domain.Entities;

namespace StoreControl.Application.Shared.Services.UserService
{
    public interface IUserService
    {
        Task<bool> IsUserUniqueAsync(User user, CancellationToken cancellationToken);
    }
}
