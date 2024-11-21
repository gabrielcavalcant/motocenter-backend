using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.TeamMember;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.TeamMember;
using OficinaMotocenter.Application.Dto.Responses.TeamMember.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Application.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller for managing team member-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamMemberController : ControllerBase
    {
        private readonly ILogger<TeamMemberController> _logger;
        private readonly ITeamMemberService _teamMemberService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="teamMemberService">Service to manage team member operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public TeamMemberController(
            ITeamMemberService teamMemberService, ILogger<TeamMemberController> logger)
        {
            _teamMemberService = teamMemberService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new team member.
        /// </summary>
        /// <param name="request">The team member data transfer object containing team member details.</param>
        /// <returns>A created team member response along with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTeamMemberRequest request)
        {
            try
            {
                _logger.LogInformation("Request initiated for creating a new team member");

                TeamMemberDtoResponse response = await _teamMemberService.CreateTeamMemberAsync(request);

                _logger.LogInformation("Response: {@response}", response);
                return CreatedAtAction(nameof(Get), new { teamMemberId = response.TeamMemberId }, response);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument provided: {Message}", ex.Message);
                return NotFound(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }

        }

        /// <summary>
        /// Retrieves a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The ID of the team member to retrieve.</param>
        /// <returns>The team member details if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{teamMemberId}")]
        public async Task<IActionResult> Get(Guid teamMemberId)
        {
            TeamMemberDtoResponse response = await _teamMemberService.GetTeamMemberByIdAsync(teamMemberId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of team members.
        /// </summary>
        /// <param name="request">The request object for retrieving a list of team members.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of team members.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListTeamMemberRequest request, CancellationToken cancellationToken)
        {
            GetListTeamMemberResponse response = await _teamMemberService.GetListTeamMemberAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing team member.
        /// </summary>
        /// <param name="teamMemberId">The ID of the team member to update.</param>
        /// <param name="request">The team member data transfer object containing updated details.</param>
        /// <returns>The updated team member response.</returns>
        [HttpPatch("{teamMemberId}")]
        public async Task<IActionResult> Put(Guid teamMemberId, [FromBody] UpdateTeamMemberRequest request)
        {
            TeamMemberDtoResponse response = await _teamMemberService.UpdateTeamMemberAsync(teamMemberId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a team member by its ID.
        /// </summary>
        /// <param name="teamMemberId">The ID of the team member to delete.</param>
        /// <returns>A no-content response if successful; otherwise, a bad request response.</returns>
        [HttpDelete("{teamMemberId}")]
        public async Task<IActionResult> Delete(Guid teamMemberId)
        {
            bool response = await _teamMemberService.DeleteTeamMemberAsync(teamMemberId);
            if (response)
            {
                return NoContent();
            }

            return BadRequest("Something went wrong with the request");
        }
    }
}
