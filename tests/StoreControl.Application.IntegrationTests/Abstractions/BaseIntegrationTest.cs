using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreControl.Infrastructure.Persistence;

namespace StoreControl.Application.IntegrationTests.Abstractions
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime
    {
        private readonly IServiceScope _serviceScope;
        private readonly IntegrationTestWebAppFactory _factory;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _factory = factory;
            _serviceScope = factory.Services.CreateScope();

            DbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            Mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }

        protected ApplicationDbContext DbContext { get; }
        protected IMediator Mediator { get; }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }

        public async Task DisposeAsync()
        {
            _serviceScope.Dispose();
            await _factory.ResetDatabaseAsync();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
