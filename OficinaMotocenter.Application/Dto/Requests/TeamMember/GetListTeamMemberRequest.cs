namespace OficinaMotocenter.Application.Dto.Requests.TeamMember
{
    /// <summary>
    /// DTO for retrieving a paginated list of TeamMembers with optional filters.
    /// </summary>
    public class GetListTeamMemberRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the Specialty of the Team.
        /// </summary>
        public string? Specialty { get; set; }

    }
}
