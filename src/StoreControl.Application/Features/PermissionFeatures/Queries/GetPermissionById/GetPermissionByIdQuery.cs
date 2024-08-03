using MediatR;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetPermissionById
{
    public class GetPermissionByIdQuery: IRequest<GetPermissionByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
