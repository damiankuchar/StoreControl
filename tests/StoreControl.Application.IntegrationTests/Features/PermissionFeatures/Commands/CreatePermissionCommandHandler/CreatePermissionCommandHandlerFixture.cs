using StoreControl.Application.IntegrationTests.Abstractions;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.IntegrationTests.Features.PermissionFeatures.Commands.CreatePermissionCommandHandler
{
    [Collection(nameof(IntegrationTestCollection))]
    public class CreatePermissionCommandHandlerFixture : BaseIntegrationTest
    {
        public CreatePermissionCommandHandlerFixture(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        protected override async Task SeedDatabaseAsync()
        {
            var permissions = new List<Permission>
            {
                new()
                {
                    Name = "ExistingPermission",
                },
            };

            await DbContext.AddRangeAsync(permissions);
            await DbContext.SaveChangesAsync();
        }
    }
}
