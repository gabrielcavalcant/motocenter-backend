namespace OficinaMotocenter.Application.Dto.Requests.TeamMember
{
    /// <summary>
    /// DTO for creating a new TeamMember.
    /// </summary>
    public class CreateTeamMemberRequest
    {
        /// <summary>
        /// Gets or sets the specialty of the team member.
        /// </summary>
        public string Specialty { get; set; }
        
        /// <summary>
        /// Gets or sets the ID of the team to which the member belongs.
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user associated with the team member.
        /// </summary>
        public Guid UserId { get; set; }
    }
}