using MediatR;

namespace StoreControl.Application.Features.PermissionsFeatures.Commands.DeletePermission
{
    public class DeletePermissionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
