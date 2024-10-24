using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreControl.Application.Interfaces;
using StoreControl.Infrastructure.Persistence;

namespace StoreControl.Application.IntegrationTests.Abstractions
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        private readonly IServiceScope _serviceScope;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();
            
            var dbContext = _serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            InitializeDatabase(dbContext);

            DbContext = dbContext;
            Mediator = _serviceScope.ServiceProvider.GetRequiredService<IMediator>();
        }

        protected IApplicationDbContext DbContext { get; }
        protected IMediator Mediator { get; }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }

        private static void InitializeDatabase(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }
    }
}
