using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a motorcycle entity within the system.
    /// </summary>
    public sealed class Motorcycle : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// Gets or sets the name or model of the motorcycle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the motorcycle (e.g., Sport, Cruiser, etc.).
        /// </summary>
        public MotorcycleType Type { get; set; }

        /// <summary>
        /// Gets or sets the license plate of the motorcycle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// Gets or sets the year the motorcycle was manufactured.
        /// </summary>
        public int YearManufacture { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier associated with the motorcycle, if applicable.
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the motorcycle.
        /// </summary>
        public Customer? Customer { get; set; }
    }
}
