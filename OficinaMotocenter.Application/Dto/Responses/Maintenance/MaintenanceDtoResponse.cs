using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Responses.Maintenance
{
    /// <summary>
    /// DTO for returning Maintenance information.
    /// </summary>
    public class MaintenanceDtoResponse
    {
        /// <summary>
        /// Unique identifier for the maintenance.
        /// </summary>
        public Guid MaintenanceId { get; set; }

        /// <summary>
        /// Status of the maintenance.
        /// </summary>
        public MaintenanceStatus MaintenanceStatus { get; set; }

        /// <summary>
        /// Description of the maintenance.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identifier of the associated motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// Motorcycle name.
        /// </summary>
        public string MotorcycleName { get; set; }

        /// <summary>
        /// Motorcycle plate.
        /// </summary>
        public string MotorcyclePlate { get; set; }

        /// <summary>
        /// Team Identifier of the responsible for the maintenance.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Team Name of the responsible for the maintenance.
        /// </summary>
        public string TeamName { get; set; }
    }
}
