using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="TeamMember"/> entity.
    /// </summary>
    public sealed class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        /// <summary>
        /// Configures the <see cref="TeamMember"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{TeamMember}"/> used to configure the <see cref="TeamMember"/> entity.</param>
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(x => x.TeamMemberId);

            builder.Property(x => x.Specialty)
                   .HasMaxLength(100)
                   .IsRequired();

            // Configuração de chave estrangeira para Team
            builder.HasOne(x => x.Team)
                   .WithMany(t => t.Members)
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Configuração de chave estrangeira para User
            builder.HasOne(x => x.User)
                   .WithMany() // Se o User não precisa de uma coleção de TeamMembers, deixe sem coleção
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}