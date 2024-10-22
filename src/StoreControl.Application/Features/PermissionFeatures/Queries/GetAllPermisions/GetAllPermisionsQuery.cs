using MediatR;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetAllPermisions
{
    public class GetAllPermisionsQuery : IRequest<IEnumerable<PermissionDto>>
    {
    }
}
