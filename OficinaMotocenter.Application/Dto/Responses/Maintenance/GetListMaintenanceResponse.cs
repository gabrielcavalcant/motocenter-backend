namespace OficinaMotocenter.Application.Dto.Responses.Maintenance
{
    /// <summary>
    /// DTO for returning a paginated list of maintenances.
    /// </summary>
    public class GetListMaintenanceResponse : GenericListResponse
    {
        /// <summary>
        /// A list of Maintenances.
        /// </summary>
        public List<MaintenanceDtoResponse> Maintenances { get; set; }
    }
}
