using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Maintenance"/> entity.
    /// Inherits from <see cref="BaseEntityConfiguration{Maintenance}"/> to reuse common configurations.
    /// </summary>
    public sealed class MaintenanceConfiguration : BaseEntityConfiguration<Maintenance>
    {
        /// <summary>
        /// Configures the <see cref="Maintenance"/> entity.
        /// Defines primary key, properties, and relationships.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Maintenance}"/> used to configure the <see cref="Maintenance"/> entity.</param>
        public override void Configure(EntityTypeBuilder<Maintenance> builder)
        {
            builder.HasKey(x => x.MaintenanceId);

            builder.Property(x => x.Description)
                   .HasMaxLength(500)
                   .IsRequired(false); // Optional description

            builder.Property(x => x.MaintenanceStatus)
                   .IsRequired();

            builder.HasOne(x => x.Motorcycle)
                   .WithMany()
                   .HasForeignKey(x => x.MotorcycleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Team)
                   .WithMany()
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Cascade); 

            base.Configure(builder);
        }
    }
}
