namespace OficinaMotocenter.Application.Dto.Requests.Role
{
    /// <summary>
    /// DTO for retrieving a paginated list of role with optional filters.
    /// </summary>
    public class GetListRoleRequest : GenericListRequest
    {

        /// <summary>
        /// Optional filter by the name of the role.
        /// </summary>
        public string? Name { get; set; }

    }
}
