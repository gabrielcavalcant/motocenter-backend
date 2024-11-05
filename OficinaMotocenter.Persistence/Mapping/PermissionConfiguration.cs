using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Permission"/> entity.
    /// Inherits from <see cref="BaseEntityConfiguration{Permission}"/> to reuse common configurations.
    /// </summary>
    public sealed class PermissionConfiguration : BaseEntityConfiguration<Permission>
    {
        /// <summary>
        /// Configures the <see cref="Permission"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Permission}"/> used to configure the <see cref="Permission"/> entity.</param>
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            // Primary key
            builder.HasKey(x => x.PermissionId);

            // Property Name with a maximum length
            builder.Property(x => x.Name)
                .IsRequired() // Required field
                .HasMaxLength(255); // Maximum length of 255 characters

            // Property Description with a maximum length
            builder.Property(x => x.Description)
                .HasMaxLength(1024); // Optional field with a max length of 1024 characters

            // Many-to-many relationship with Role
            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Permissions);

            base.Configure(builder);
        }
    }
}
