using MediatR;

namespace StoreControl.Application.Features.UserFeatures.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDetailedDto>
    {
        public Guid Id { get; set; }
    }
}
