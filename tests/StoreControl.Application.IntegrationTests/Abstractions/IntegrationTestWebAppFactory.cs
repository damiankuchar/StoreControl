using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using StoreControl.Infrastructure.Persistence;
using System.Data.Common;
using Testcontainers.PostgreSql;

namespace StoreControl.Application.IntegrationTests.Abstractions
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("StoreControl")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        private DbConnection _dbConnection = null!;
        private Respawner _respawner = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));

                var serviceProvider = services.BuildServiceProvider();
              
                using var scope = serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Database.Migrate();
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

            await _dbConnection.OpenAsync();

            // Ensure that application is started
            CreateClient();

            await InitializeRespawnerAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
            await _dbConnection.DisposeAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        private async Task InitializeRespawnerAsync()
        {
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                SchemasToInclude = ["public"],
                DbAdapter = DbAdapter.Postgres
            });
        }
    }
}
