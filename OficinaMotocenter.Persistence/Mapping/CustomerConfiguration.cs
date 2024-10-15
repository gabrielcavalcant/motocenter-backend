using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;


namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="Customer"/> entity.
    /// Inherits from <see cref="BaseEntityConfiguration{Customer}"/> to reuse common configurations.
    /// </summary>
    public sealed class CustomerConfiguration : BaseEntityConfiguration<Customer>
    {
        /// <summary>
        /// Configures the <see cref="Customer"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{Customer}"/> used to configure the <see cref="Customer"/> entity.</param>
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Telephone)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.Property(x => x.Cpf)
                   .HasMaxLength(11)
                   .IsRequired();

            builder.HasMany(c => c.Motorcycles)
                   .WithOne(m => m.Customer)
                   .HasForeignKey(m => m.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .OnDelete(DeleteBehavior.SetNull); // Allows a customer not to have motorcycles initially
            ;

            base.Configure(builder);
        }
    }
}
