using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Motorcycle"/> entity.
    /// Inherits from <see cref="BaseEntityConfiguration{Motorcycle}"/> to reuse common configurations.
    /// </summary>
    public sealed class MotorcycleConfiguration : BaseEntityConfiguration<Motorcycle>
    {
        /// <summary>
        /// Configures the <see cref="Motorcycle"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Motorcycle}"/> used to configure the <see cref="Motorcycle"/> entity.</param>
        public override void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.HasKey(x => x.MotorcycleId);

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Plate)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(x => x.YearManufacture)
                   .IsRequired();

            builder.Property(x => x.Type)
                   .IsRequired();

            builder.HasOne(x => x.Customer)
                   .WithMany(c => c.Motorcycles)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
