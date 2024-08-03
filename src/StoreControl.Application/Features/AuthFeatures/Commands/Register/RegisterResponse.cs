namespace StoreControl.Application.Features.AuthFeatures.Commands.Register
{
    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
