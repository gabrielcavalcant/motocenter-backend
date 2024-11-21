namespace OficinaMotocenter.Application.Dto.Requests.Team
{
    /// <summary>
    /// DTO for retrieving a paginated list of Teams with optional filters.
    /// </summary>
    public class GetListTeamRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the Team.
        /// </summary>
        public string? Name { get; set; }

    }
}
