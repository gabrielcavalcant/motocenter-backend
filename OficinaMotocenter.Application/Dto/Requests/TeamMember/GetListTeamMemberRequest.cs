using OficinaMotocenter.Domain.Entities.Enumerations;

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
        public TeamMemberSpecialty? Specialty { get; set; }

    }
}
