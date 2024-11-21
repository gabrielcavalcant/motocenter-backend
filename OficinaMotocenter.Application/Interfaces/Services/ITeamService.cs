using OficinaMotocenter.Application.Dto.Requests.Team;
using OficinaMotocenter.Application.Dto.Responses.Team;
using OficinaMotocenter.Application.Dto.Responses.Team.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to teams.
    /// Inherits from the generic service interface.
    /// </summary>
    public interface ITeamService : IGenericService<Team>
    {
        /// <summary>
        /// Executes the creation of a new team.
        /// </summary>
        /// <param name="request">The request DTO containing team information.</param>
        /// <returns>A response DTO with the details of the created team.</returns>
        Task<TeamDtoResponse> CreateTeamAsync(CreateTeamRequest request);

        /// <summary>
        /// Executes the retrieval of a team by its ID.
        /// </summary>
        /// <param name="teamId">The unique ID of the team to retrieve.</param>
        /// <returns>A response DTO with the details of the team.</returns>
        Task<TeamDtoResponse> GetTeamByIdAsync(Guid teamId);

        /// <summary>
        /// Executes the retrieval of all teams with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of teams and pagination details.</returns>
        Task<GetListTeamResponse> GetListTeamAsync(GetListTeamRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the update of a team by its ID.
        /// </summary>
        /// <param name="teamId">The unique ID of the team to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated team.</returns>
        Task<TeamDtoResponse> UpdateTeamAsync(Guid teamId, UpdateTeamRequest request);

        /// <summary>
        /// Executes the deletion of a team by its ID.
        /// </summary>
        /// <param name="teamId">The unique ID of the team to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteTeamAsync(Guid teamId);

        Task<bool> AddMemberToTeam(Guid teamId, Guid teamMemberId);

    }
}