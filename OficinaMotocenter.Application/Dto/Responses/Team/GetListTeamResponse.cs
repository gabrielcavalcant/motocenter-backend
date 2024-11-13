namespace OficinaMotocenter.Application.Dto.Responses.Team
{
    namespace OficinaMotocenter.Application.Dto.Responses
    {
        /// <summary>
        /// DTO for returning a paginated list of Team. It includes a list of Team,
        /// the current page index, and the total count of Team.
        /// </summary>
        public class GetListTeamResponse : GenericListResponse
        {
            /// <summary>
            /// A list of Teams for the current page.
            /// </summary>
            public List<TeamDtoResponse> Team { get; set; }
        }
    }

}