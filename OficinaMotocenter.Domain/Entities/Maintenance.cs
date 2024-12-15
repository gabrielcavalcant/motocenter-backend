using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a maintenance record within the system.
    /// </summary>
    public class Maintenance : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the maintenance.
        /// </summary>
        public Guid MaintenanceId { get; set; }

        /// <summary>
        /// Gets or sets the status of the maintenance.
        /// </summary>
        public MaintenanceStatus MaintenanceStatus { get; set; }

        /// <summary>
        /// Gets or sets a description of the maintenance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// Gets or sets the associated motorcycle.
        /// </summary>
        public Motorcycle Motorcycle { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the team responsible for the maintenance.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team responsible for the maintenance.
        /// </summary>
        public Team Team { get; set; }
    }
}
