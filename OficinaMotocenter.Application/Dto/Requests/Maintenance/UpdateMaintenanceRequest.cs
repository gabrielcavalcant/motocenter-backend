using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Requests.Maintenance
{
    public class UpdateMaintenanceRequest
    {
        /// <summary>
        /// status of the maintenance.
        /// </summary>
        public MaintenanceStatus? MaintenanceStatus { get; set; }

        /// <summary>
        /// description of the maintenance.
        /// </summary>
        public string? Description { get; set; }

    }
}
