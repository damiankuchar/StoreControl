using MediatR;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
