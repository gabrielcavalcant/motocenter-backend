using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Team"/> entity.
    /// </summary>
    public sealed class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        /// <summary>
        /// Configures the <see cref="Team"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Team}"/> used to configure the <see cref="Team"/> entity.</param>
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.TeamId);

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            // Configuração de relacionamento com TeamMember (1:N)
            builder.HasMany(x => x.Members)
                   .WithOne(m => m.Team)
                   .HasForeignKey(m => m.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}