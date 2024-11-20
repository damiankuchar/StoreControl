using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreControl.Domain.Entities;

namespace StoreControl.Infrastructure.Persistence.Configurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);

            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.ModifiedBy).HasDefaultValue(null);
            builder.Property(x => x.ModifiedOn).HasDefaultValue(null);

            builder.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles);

            builder.HasMany(x => x.Users)
                .WithMany(x => x.Roles);
        }
    }
}
