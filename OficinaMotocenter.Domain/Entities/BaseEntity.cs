namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents the base entity with common properties for all domain entities.
    /// </summary>
    public abstract class BaseEntity
    {

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        public DateTimeOffset? DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was deleted.
        /// </summary>
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
