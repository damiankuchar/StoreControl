using StoreControl.Application.IntegrationTests.Abstractions;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.IntegrationTests.Features.PermissionFeatures.Commands.CreatePermissionCommandHandler
{
    public class CreatePermissionCommandHandlerFixture : BaseIntegrationTest
    {
        public CreatePermissionCommandHandlerFixture(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
            DbContext.Permissions.Add(new Permission
            {
                Name = "ExistingPermission",
            });

            DbContext.SaveChangesAsync();
        }
    }
}
