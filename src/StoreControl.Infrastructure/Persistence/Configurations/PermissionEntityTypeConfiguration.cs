using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StoreControl.Domain.Entities;

namespace StoreControl.Infrastructure.Persistence.Configurations
{
    public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.ModifiedBy).HasDefaultValue(null);
            builder.Property(x => x.ModifiedOn).HasDefaultValue(null);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Permissions);
        }
    }
}
