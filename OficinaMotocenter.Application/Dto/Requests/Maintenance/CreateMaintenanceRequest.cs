using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Requests.Maintenance
{
    /// <summary>
    /// DTO for the request to create a new maintenance. Contains the necessary information
    /// to register a new maintenance in the system.
    /// </summary>
    public class CreateMaintenanceRequest
    {
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
        /// Gets or sets the identifier of the team responsible for the maintenance.
        /// </summary>
        public Guid TeamId { get; set; }
    }
}
