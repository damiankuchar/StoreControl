using Microsoft.Extensions.Options;
using StoreControl.Domain.Options;

namespace StoreControl.WebAPI.OptionsSetup
{
    public class AuthorizationSettingsOptionsSetup : IConfigureOptions<AuthorizationSettingsOptions>
    {
        private const string SectionName = "AuthorizationSettingsOptions";
        private readonly IConfiguration _configuration;

        public AuthorizationSettingsOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(AuthorizationSettingsOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
