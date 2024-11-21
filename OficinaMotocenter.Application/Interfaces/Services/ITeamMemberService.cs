using OficinaMotocenter.Application.Dto.Requests.TeamMember;
using OficinaMotocenter.Application.Dto.Responses.TeamMember;
using OficinaMotocenter.Application.Dto.Responses.TeamMember.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to team members.
    /// Inherits from the generic service interface and adds additional methods.
    /// </summary>
    public interface ITeamMemberService : IGenericService<TeamMember>
    {

        /// <summary>
        /// Executes the creation of a new team member.
        /// </summary>
        /// <param name="request">The request DTO containing team member information.</param>
        /// <returns>A response DTO with the details of the created team member.</returns>
        Task<TeamMemberDtoResponse> CreateTeamMemberAsync(CreateTeamMemberRequest request);

        /// <summary>
        /// Executes the retrieval of a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member to retrieve.</param>
        /// <returns>A response DTO with the details of the team member.</returns>
        Task<TeamMemberDtoResponse> GetTeamMemberByIdAsync(Guid teamMemberId);

        /// <summary>
        /// Executes the retrieval of all team members with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of team members and pagination details.</returns>
        Task<GetListTeamMemberResponse> GetListTeamMemberAsync(GetListTeamMemberRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the update of a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated team member.</returns>
        Task<TeamMemberDtoResponse> UpdateTeamMemberAsync(Guid teamMemberId, UpdateTeamMemberRequest request);

        /// <summary>
        /// Executes the deletion of a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The unique ID of the team member to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteTeamMemberAsync(Guid teamMemberId);
    }
}
