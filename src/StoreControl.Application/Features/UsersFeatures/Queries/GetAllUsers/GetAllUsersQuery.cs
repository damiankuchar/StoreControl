using MediatR;

namespace StoreControl.Application.Features.UsersFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}
