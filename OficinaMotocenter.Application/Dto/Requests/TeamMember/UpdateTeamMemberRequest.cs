using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Requests.TeamMember
{
    /// <summary>
    /// DTO for updating an existing TeamMember.
    /// </summary>
    public class UpdateTeamMemberRequest
    {
        /// <summary>
        /// TeamMemberId to updates.
        /// </summary>
        public Guid TeamMemberId { get; set; }
        /// <summary>
        /// Specialty to updates of the team member.
        /// </summary>
        public TeamMemberSpecialty Specialty { get; set; }
    }
}