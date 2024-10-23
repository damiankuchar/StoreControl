using MediatR;

namespace StoreControl.Application.Features.UsersFeatures.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
