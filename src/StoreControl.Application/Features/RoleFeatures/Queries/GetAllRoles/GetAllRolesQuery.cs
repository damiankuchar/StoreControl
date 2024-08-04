using MediatR;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<GetAllRolesResponse>>
    {
    }
}
