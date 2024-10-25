using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Features.PermissionsFeatures.Commands.CreatePermission;
using StoreControl.Application.IntegrationTests.Abstractions;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.IntegrationTests.Features.PermissionFeatures.Commands.CreatePermissionCommandHandler
{
    [Collection(nameof(IntegrationTestCollection))]
    public class CreatePermissionCommandHandlerTests : CreatePermissionCommandHandlerFixture
    {
        public CreatePermissionCommandHandlerTests(IntegrationTestWebAppFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Handle_Should_CreatePermission_WhenCommandIsValid()
        {
            // Arrange
            var command = new CreatePermissionCommand
            {
                Name = "TestPermission",
            };

            // Act
            var result = await Mediator.Send(command);

            // Assert
            var createdPermission = await DbContext.Permissions
                .FirstOrDefaultAsync(x => x.Name == command.Name);

            createdPermission.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_Should_ThrowBadRequestException_WhenPermissionIsNotUnique()
        {
            // Arrange
            var command = new CreatePermissionCommand
            {
                Name = "ExistingPermission"
            };

            // Act
            var action = () => Mediator.Send(command);

            // Assert
            await action.Should().ThrowAsync<BadRequestException>();
        }
    }
}
