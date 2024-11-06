using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities.Stock;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="StockMovement"/> entity.
    /// </summary>
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(x => x.StockMovementId);

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.MovementDate)
                   .IsRequired();

            builder.Property(x => x.Notes)
                   .HasMaxLength(500);

            builder.HasOne(x => x.Item)
                   .WithMany()
                   .HasForeignKey(x => x.ItemId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.MovementType)
                   .IsRequired();
        }
    }
}
