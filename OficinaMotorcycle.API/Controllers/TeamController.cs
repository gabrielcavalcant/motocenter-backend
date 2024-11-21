using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Team;
using OficinaMotocenter.Application.Dto.Responses.Team;
using OficinaMotocenter.Application.Dto.Responses.Team.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotocenter.API.Controllers
{
    /// <summary>
    /// Controller for managing team-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamService _teamService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="teamService">Service to manage team operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a member to a specific team.
        /// </summary>
        /// <param name="teamId">The ID of the team to which the member will be added.</param>
        /// <param name="teamMemberId">The ID of the team member to add to the team.</param>
        /// <returns>A success response if the association was made, or an error message if unsuccessful.</returns>
        [HttpPost("{teamId}/members/{teamMemberId}")]
        public async Task<IActionResult> AddMemberToTeam(Guid teamId, Guid teamMemberId)
        {
            _logger.LogInformation("Request to add member {TeamMemberId} to team {TeamId} initiated", teamMemberId, teamId);

            bool result = await _teamService.AddMemberToTeam(teamId, teamMemberId);

            if (!result)
            {
                _logger.LogWarning("Failed to add member {TeamMemberId} to team {TeamId}", teamMemberId, teamId);
                return BadRequest("Failed to add member to the team. Either the team or member was not found, or they are already associated.");
            }

            _logger.LogInformation("Successfully added member {TeamMemberId} to team {TeamId}", teamMemberId, teamId);
            return Ok("Member successfully added to the team.");
        }


        /// <summary>
        /// Creates a new team.
        /// </summary>
        /// <param name="request">The team data transfer object containing team details.</param>
        /// <returns>A created team response along with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeamRequest request)
        {
            _logger.LogInformation("Team creation request initiated");

            TeamDtoResponse response = await _teamService.CreateTeamAsync(request);

            _logger.LogInformation("Team created: {@response}", response);
            return CreatedAtAction(nameof(Get), new { teamId = response.TeamId }, response);
        }

        /// <summary>
        /// Retrieves a team by its ID.
        /// </summary>
        /// <param name="teamId">The ID of the team to retrieve.</param>
        /// <returns>The team details if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{teamId}")]
        public async Task<IActionResult> Get(Guid teamId)
        {
            TeamDtoResponse response = await _teamService.GetTeamByIdAsync(teamId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of teams.
        /// </summary>
        /// <param name="request">The request object for retrieving a team list.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of teams.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListTeamRequest request, CancellationToken cancellationToken)
        {
            GetListTeamResponse response = await _teamService.GetListTeamAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing team.
        /// </summary>
        /// <param name="teamId">The ID of the team to update.</param>
        /// <param name="request">The team data transfer object containing updated details.</param>
        /// <returns>The updated team response.</returns>
        [HttpPatch("{teamId}")]
        public async Task<IActionResult> Put(Guid teamId, [FromBody] UpdateTeamRequest request)
        {
            TeamDtoResponse response = await _teamService.UpdateTeamAsync(teamId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a team by its ID.
        /// </summary>
        /// <param name="teamId">The ID of the team to delete.</param>
        /// <returns>A no-content response if successful; otherwise, a bad request response.</returns>
        [HttpDelete("{teamId}")]
        public async Task<IActionResult> Delete(Guid teamId)
        {
            bool response = await _teamService.DeleteTeamAsync(teamId);
            if (response)
            {
                return NoContent();
            }

            return BadRequest("Failed to delete the team");
        }
    }
}