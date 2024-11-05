using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling role management operations, including CRUD actions.
    /// </summary>
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="roleService">Service to handle role operations.</param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Retrieves a role by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the role.</param>
        /// <returns>An <see cref="IActionResult"/> containing the role details or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            RoleDtoResponse response = await _roleService.GetRoleByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of roles based on the specified filter criteria.
        /// </summary>
        /// <param name="request">The filter criteria for retrieving roles.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of roles or NotFound if no roles match.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListRoleRequest request, CancellationToken cancellationToken)
        {
            GetListRoleResponse response = await _roleService.GetListRoleAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Creates a new role with the specified details.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created role and its location URI.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role cannot be null.");
            }

            RoleDtoResponse response = await _roleService.CreateRoleAsync(request);

            return CreatedAtAction(nameof(GetRoleById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Updates an existing role with the specified identifier and details.
        /// </summary>
        /// <param name="id">The unique identifier of the role to update.</param>
        /// <param name="request">The updated role details.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated role.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role cannot be null.");
            }

            RoleDtoResponse response = await _roleService.UpdateRoleAsync(id, request);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a role by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the role to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            bool success = await _roleService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
