using MediatR;

namespace StoreControl.Application.Features.UserFeatures.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
