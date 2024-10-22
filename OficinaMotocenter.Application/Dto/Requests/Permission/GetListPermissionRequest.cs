namespace OficinaMotocenter.Application.Dto.Requests.Permission
{
    /// <summary>
    /// DTO for retrieving a paginated list of permission with optional filters.
    /// </summary>
    public class GetListPermissionRequest
    {
        /// <summary>
        /// Optional filter by the name of the permission.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The page number to retrieve, defaulting to 1.
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The number of items to retrieve per page, defaulting to 10.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
