using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Role"/> entity.
    /// Defines properties and relationships for the entity.
    /// Inherits from <see cref="BaseEntityConfiguration{Role}"/> to reuse common configurations.
    /// </summary>
    public sealed class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        /// <summary>
        /// Configures the <see cref="Role"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Role}"/> used to configure the entity.</param>
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(x => x.RoleId);

            // Property Name with a maximum length
            builder.Property(x => x.Name)
                .IsRequired() // Required field
                .HasMaxLength(255); // Maximum length of 255 characters

            // Relationship with Users (One-to-Many)
            builder.HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            // Many-to-many relationship with Permission
            builder.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles);

            base.Configure(builder);
        }
    }
}
