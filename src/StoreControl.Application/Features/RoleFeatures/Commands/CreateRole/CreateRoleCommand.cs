using MediatR;

namespace StoreControl.Application.Features.RoleFeatures.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public List<Guid> PermissionIds { get; set; } = new List<Guid>();
    }
}
