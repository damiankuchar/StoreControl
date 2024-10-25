using StoreControl.Application.IntegrationTests.Abstractions;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.IntegrationTests.Features.PermissionFeatures.Queries.GetAllPermissionsQueryHandler
{
    [Collection(nameof(IntegrationTestCollection))]
    public class GetAllPermissionsQueryHandlerFixture : BaseIntegrationTest
    {
        public GetAllPermissionsQueryHandlerFixture(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        protected async Task SeedDatabaseAsync()
        {
            var permissions = new List<Permission>()
            {
                new()
                {
                    Name = "TestPermission1",
                },
                new() 
                {
                    Name = "TestPermission2",
                }
            };

            await DbContext.Permissions.AddRangeAsync(permissions);
            await DbContext.SaveChangesAsync();
        }
    }
}
