using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller responsible for managing permissions, including CRUD operations and role association.
    /// </summary>
    [ApiController]
    [Route("api/permission")]
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionController"/> class.
        /// </summary>
        /// <param name="permissionService">Service for handling permission operations.</param>
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Retrieves a specific permission by its unique identifier.
        /// </summary>
        /// <param name="permissionId">The unique identifier of the permission.</param>
        /// <returns>An <see cref="IActionResult"/> containing the permission details or NotFound if not found.</returns>
        [HttpGet("{permissionId}")]
        public async Task<IActionResult> GetPermission(Guid permissionId)
        {
            PermissionDtoResponse response = await _permissionService.GetPermissionByIdAsync(permissionId);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of permissions based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria for retrieving permissions.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of permissions or NotFound if none match.</returns>
        [HttpGet]
        public async Task<IActionResult> GetListPermissions([FromQuery] GetListPermissionRequest request, CancellationToken cancellationToken)
        {
            GetListPermissionResponse response = await _permissionService.GetListPermissionAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Creates a new permission with the specified details.
        /// </summary>
        /// <param name="newPermissionDto">The details of the permission to create.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created permission and its location URI.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest newPermissionDto)
        {
            if (newPermissionDto == null)
            {
                return BadRequest("Permission cannot be null.");
            }

            PermissionDtoResponse response = await _permissionService.CreatePermissionAsync(newPermissionDto);

            return CreatedAtAction(nameof(GetPermission), new { permissionId = response.PermissionId }, response);
        }

        /// <summary>
        /// Updates an existing permission with the specified identifier and details.
        /// </summary>
        /// <param name="permissionId">The unique identifier of the permission to update.</param>
        /// <param name="updatedPermissionDto">The updated permission details.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated permission.</returns>
        [HttpPut("{permissionId}")]
        public async Task<IActionResult> UpdatePermission(Guid permissionId, [FromBody] UpdatePermissionRequest updatedPermissionDto)
        {
            if (updatedPermissionDto == null)
            {
                return BadRequest("Permission cannot be null.");
            }

            PermissionDtoResponse response = await _permissionService.UpdatePermissionAsync(permissionId, updatedPermissionDto);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a permission by its unique identifier.
        /// </summary>
        /// <param name="permissionId">The unique identifier of the permission to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the deletion.</returns>
        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> DeletePermission(Guid permissionId)
        {
            bool success = await _permissionService.DeletePermissionAsync(permissionId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Associates a role with a specific permission.
        /// </summary>
        /// <param name="permissionId">The unique identifier of the permission.</param>
        /// <param name="roleId">The unique identifier of the role to associate.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success or NotFound if either the permission or role is not found.</returns>
        [HttpPost("{permissionId}/roles/{roleId}")]
        public async Task<IActionResult> AddRoleToPermission(Guid permissionId, Guid roleId)
        {
            bool result = await _permissionService.AddRoleToPermission(permissionId, roleId);
            if (!result)
            {
                return NotFound("Permission or Role not found.");
            }

            return Ok("Role successfully added to Permission.");
        }
    }
}
