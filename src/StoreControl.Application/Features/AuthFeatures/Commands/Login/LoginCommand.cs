using MediatR;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
