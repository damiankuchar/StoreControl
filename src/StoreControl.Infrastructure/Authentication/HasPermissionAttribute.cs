using Microsoft.AspNetCore.Authorization;

namespace StoreControl.Infrastructure.Authentication
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
            : base(permission.ToString())
        {
        }
    }
}
