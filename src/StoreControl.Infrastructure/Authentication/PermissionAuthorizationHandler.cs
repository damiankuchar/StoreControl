using Microsoft.AspNetCore.Authorization;

namespace StoreControl.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
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
