using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

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

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListRoleRequest request, CancellationToken cancellationToken)
        {
            GetListRoleResponse response = await _roleService.GetListRoleAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role não pode ser nula.");
            }

            RoleDtoResponse response = await _roleService.CreateRoleAsync(request);

            return CreatedAtAction(nameof(GetRoleById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleRequest request)
        {
            if (request == null)
            {
                return BadRequest("Role não pode ser nula.");
            }

            RoleDtoResponse response = await _roleService.UpdateRoleAsync(id, request);

            return Ok(response);
        }

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