using StoreControl.Domain.Entities;

namespace StoreControl.Application.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken);
        string GenerateRefreshToken();
    }
}
