using MediatR;

namespace StoreControl.Application.Features.RolesFeatures.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }
}
