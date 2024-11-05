namespace OficinaMotocenter.Application.Dto.Requests.Permission
{
    /// <summary>
    /// DTO for retrieving a paginated list of permission with optional filters.
    /// </summary>
    public class GetListPermissionRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the permission.
        /// </summary>
        public string? Name { get; set; }
    }
}
