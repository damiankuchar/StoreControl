using MediatR;

namespace StoreControl.Application.Features.PermissionsFeatures.Queries.GetAllPermisions
{
    public class GetAllPermisionsQuery : IRequest<IEnumerable<PermissionDto>>
    {
    }
}
