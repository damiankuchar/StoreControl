using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreControl.Infrastructure.Persistence;

namespace StoreControl.Application.IntegrationTests.Abstractions
{
    [Collection(nameof(IntegrationTestCollection))]
    public abstract class BaseIntegrationTest : IAsyncLifetime
    {
        private readonly IServiceScope _serviceScope;
        private readonly IntegrationTestWebAppFactory _factory;

        protected ApplicationDbContext DbContext { get; }
        protected IMediator Mediator { get; }

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _factory = factory;
            _serviceScope = factory.Services.CreateScope();

            DbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            _serviceScope.Dispose();
            await _factory.ResetDatabaseAsync();
        }

        protected virtual Task SeedDatabaseAsync() => Task.CompletedTask;
    }
}
