using MediatR;

namespace StoreControl.Application.Features.UsersFeatures.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDetailedDto>
    {
        public Guid Id { get; set; }
    }
}
