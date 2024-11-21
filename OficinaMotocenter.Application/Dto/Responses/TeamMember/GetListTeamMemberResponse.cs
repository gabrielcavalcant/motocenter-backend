namespace OficinaMotocenter.Application.Dto.Responses.TeamMember
{
    namespace OficinaMotocenter.Application.Dto.Responses
    {
        /// <summary>
        /// DTO for returning a paginated list of TeamMember. It includes a list of TeamMember,
        /// the current page index, and the total count of TeamMember.
        /// </summary>
        public class GetListTeamMemberResponse : GenericListResponse
        {
            /// <summary>
            /// A list of TeamMembers for the current page.
            /// </summary>
            public List<TeamMemberDtoResponse> TeamMember { get; set; }
        }
    }

}
