using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Common;
using StoreControl.Domain.Entities;
using StoreControl.Infrastructure.Authentication;

namespace StoreControl.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserClaimsService userClaimsService)
            : base(options)
        {
            _userClaimsService = userClaimsService;
        }

        private readonly IUserClaimsService _userClaimsService;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IAuditableEntity>();
            var claims = _userClaimsService.GetUserClaims();

            var userName = claims.TryGetValue(CustomClaims.FullName, out var name) ? name : "Unknown";

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = userName;
                    entry.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.ModifiedBy).CurrentValue = userName;
                    entry.Property(x => x.ModifiedOn).CurrentValue = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await Database.BeginTransactionAsync(cancellationToken);

            return transaction;
        }
    }
}
