using OficinaMotocenter.Application.Dto.Responses.TeamMember;

namespace OficinaMotocenter.Application.Dto.Responses.Team
{
    /// <summary>
    /// DTO for returning Team information in responses, including details such as ID, name, and associated permissions.
    /// </summary>
    public class TeamDtoResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the Team.
        /// </summary>
        public Guid TeamId { get; set; } // Team ID

        /// <summary>
        /// Gets or sets the name of the Team.
        /// </summary>
        public string Name { get; set; } // Team name

        public List<TeamMemberDtoResponse> Members { get; set; }
    }
}
