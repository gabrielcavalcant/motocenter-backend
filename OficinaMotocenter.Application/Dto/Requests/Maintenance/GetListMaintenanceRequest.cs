using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Requests.Maintenance
{
    public class GetListMaintenanceRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the status of the maintenance.
        /// </summary>
        public MaintenanceStatus? MaintenanceStatus { get; set; }
    }
}
