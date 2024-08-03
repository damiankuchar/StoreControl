using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreControl.Domain.Entities;

namespace StoreControl.Infrastructure.Persistence.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(512);
            builder.Property(x => x.RegistrationDate).IsRequired();
            builder.Property(x => x.RefreshToken).HasMaxLength(128).HasDefaultValue(null);
            builder.Property(x => x.RefreshTokenExpiryTime).HasDefaultValue(null);

            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.ModifiedBy).HasDefaultValue(null);
            builder.Property(x => x.ModifiedOn).HasDefaultValue(null);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users);
        }
    }
}
