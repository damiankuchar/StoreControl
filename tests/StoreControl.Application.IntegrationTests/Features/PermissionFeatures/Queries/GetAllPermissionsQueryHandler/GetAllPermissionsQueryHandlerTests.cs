using FluentAssertions;
using StoreControl.Application.Features.PermissionsFeatures.Queries.GetAllPermisions;
using StoreControl.Application.IntegrationTests.Abstractions;

namespace StoreControl.Application.IntegrationTests.Features.PermissionFeatures.Queries.GetAllPermissionsQueryHandler
{
    public class GetAllPermissionsQueryHandlerTests : GetAllPermissionsQueryHandlerFixture
    {
        public GetAllPermissionsQueryHandlerTests(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Handle_Should_ReturnAllPermissions_WhenPermissionsExist()
        {
            // Arrange
            var query = new GetAllPermisionsQuery();
            await SeedDatabase();

            // Act
            var result = await Mediator.Send(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_Should_ReturnEmptyList_WhenNoPermissionsExist()
        {
            // Arrange
            var query = new GetAllPermisionsQuery();

            // Act
            var result = await Mediator.Send(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
