using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StoreControl.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly JwtOptions _jwtOptions;
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtProvider(
            IApplicationDbContext dbContext,
            IOptions<JwtOptions> jwtOptions,
            IPermissionService permissionService,
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _jwtOptions = jwtOptions.Value;
            _permissionService = permissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GenerateAccessTokenAsync(User user, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(CustomClaims.FullName, $"{user.FirstName} {user.LastName}"),
            };

            var permissions = await _permissionService
                .GetPermissionAsync(user.Id, cancellationToken);

            var roles = await _permissionService
                .GetRoleAsync(user.Id, cancellationToken);

            foreach (var permission in permissions)
            {
                claims.Add(new(CustomClaims.Permissions, permission));
            }

            foreach (var role in roles)
            {
                claims.Add(new(CustomClaims.Roles, role));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                DateTime.UtcNow.AddSeconds(_jwtOptions.ExpirationTime),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenValue;
        }

        public async Task<string> GenerateAndSaveRefreshTokenAsync(User user, CancellationToken cancellationToken)
        {
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddSeconds(_jwtOptions.RefreshTokenExpirationTime);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
