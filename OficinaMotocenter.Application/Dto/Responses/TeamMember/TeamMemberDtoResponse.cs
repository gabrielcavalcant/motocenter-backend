namespace OficinaMotocenter.Application.Dto.Responses.TeamMember
{
    /// <summary>
    /// DTO for returning TeamMember information in responses, including details such as ID, name, and associated permissions.
    /// </summary>
    public class TeamMemberDtoResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the TeamMember.
        /// </summary>
        public Guid TeamMemberId { get; set; } // TeamMember ID

        /// <summary>
        /// Gets or sets the name of the TeamMember.
        /// </summary>
        public string Specialty { get; set; } // TeamMember name
    }
}
