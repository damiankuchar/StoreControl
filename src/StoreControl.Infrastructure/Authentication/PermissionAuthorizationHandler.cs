using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using StoreControl.Domain.Constants;
using StoreControl.Domain.Options;

namespace StoreControl.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly AuthorizationSettingsOptions _authorizationSettingsOptions;

        public PermissionAuthorizationHandler(IOptions<AuthorizationSettingsOptions> options)
        {
            _authorizationSettingsOptions = options.Value;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!_authorizationSettingsOptions.Enabled)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var permission = context.User.Claims
                .Where(x => x.Type == CustomClaims.Permissions)
                .Select(x => x.Value)
                .ToHashSet();

            if (permission.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
