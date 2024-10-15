using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides a base class for configuring entities in the Entity Framework.
    /// Implements the <see cref="IEntityTypeConfiguration{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The entity type to be configured.</typeparam>
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="T"/>.
        /// This method can be overridden in derived classes to provide custom configurations.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{T}"/> to configure the entity.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Default configuration for entities.
            // Derived classes can override this method to provide specific configurations.
        }
    }
}
