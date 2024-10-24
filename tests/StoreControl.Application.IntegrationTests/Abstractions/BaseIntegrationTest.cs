using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreControl.Application.Interfaces;

namespace StoreControl.Application.IntegrationTests.Abstractions
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        private readonly IServiceScope _serviceScope;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            DbContext = _serviceScope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            Mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }

        protected IApplicationDbContext DbContext { get; }
        protected IMediator Mediator { get; }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
