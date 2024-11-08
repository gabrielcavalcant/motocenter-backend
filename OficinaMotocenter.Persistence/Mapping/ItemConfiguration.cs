using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OficinaMotocenter.Domain.Entities.Stock;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Item"/> entity.
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemId);

            builder.Property(x => x.Name)   
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.SerialCode)
           .HasMaxLength(15)
           .IsRequired();

            builder.Property(x => x.Supplier)
           .HasMaxLength(100)
           .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(250);

            builder.Property(x => x.StockQuantity)
                   .IsRequired();

            //builder.Property(x => x.DateAdded)
            //       .IsRequired();

            //builder.HasOne(x => x.Category)
            //       .WithMany()
            //       .HasForeignKey(x => x.CategoryId)
            //       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
