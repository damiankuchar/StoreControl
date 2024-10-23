using MediatR;

namespace StoreControl.Application.Features.RolesFeatures.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
