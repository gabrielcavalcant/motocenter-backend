using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Tokens"/> entity.
    /// Defines properties for token storage.
    /// Inherits from <see cref="BaseEntityConfiguration{Tokens}"/> to reuse common configurations.

    /// </summary>
    public sealed class TokensConfiguration : IEntityTypeConfiguration<Tokens>
    {
        /// <summary>
        /// Configures the <see cref="Tokens"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Tokens}"/> used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Tokens> builder)
        {
            // Configura Tokens como uma entidade sem chave
            builder.HasNoKey();

            // Property AccessToken (required)
            builder.Property(x => x.AccessToken)
                .IsRequired() // Required field
                .HasMaxLength(1024); // Maximum length of 1024 characters

            // Property RefreshToken (required)
            builder.Property(x => x.RefreshToken)
                .IsRequired() // Required field
                .HasMaxLength(1024); // Maximum length of 1024 characters
        }
    }
}
