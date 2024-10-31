using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Application.Interfaces.Services;


namespace OficinaMotorcycle.API.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("{permissionId}")]
        public async Task<IActionResult> GetPermission(Guid permissionId)
        {
            PermissionDtoResponse response = await _permissionService.GetPermissionByIdAsync(permissionId);
            if (response == null)
            {
                return NotFound(); // Retorna 404 se a Permission não for encontrada
            }

            return Ok(response); // Retorna a Permission encontrada como DTO
        }

        [HttpGet]
        public async Task<IActionResult> GetListPermissions([FromQuery] GetListPermissionRequest request,CancellationToken cancellationToken)
        {
            GetListPermissionResponse response = await _permissionService.GetListPermissionAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest newPermissionDto)
        {
            if (newPermissionDto == null)
            {
                return BadRequest("Permission não pode ser nula."); // Retorna 400 se a Permission for nula
            }

            PermissionDtoResponse response = await _permissionService.CreatePermissionAsync(newPermissionDto);

            // Retorna 201 com a nova Permission mapeada para PermissionDTO
            return CreatedAtAction(nameof(GetPermission), new { permissionId = response.PermissionId }, response);
        }

        [HttpPut("{permissionId}")]
        public async Task<IActionResult> UpdatePermission(Guid permissionId, [FromBody] UpdatePermissionRequest updatedPermissionDto)
        {
            if (updatedPermissionDto == null)
            {
                return BadRequest("Permission não pode ser nula.");
            }

            PermissionDtoResponse response = await _permissionService.UpdatePermissionAsync(permissionId, updatedPermissionDto);

            // Retorna a Permission atualizada com status 200 OK
            return Ok(response);
        }

        [HttpDelete("{permissionId}")]
        public async Task<IActionResult> DeletePermission(Guid permissionId)
        {
            bool success = await _permissionService.DeletePermissionAsync(permissionId);
            if (!success)
            {
                return NotFound(); // Retorna 404 se a deleção falhar
            }
            return NoContent(); // Retorna 204 se a deleção for bem-sucedida
        }

        [HttpPost("{permissionId}/roles/{roleId}")]
        public async Task<IActionResult> AddRoleToPermission(Guid permissionId, Guid roleId)
        {
            bool result = await _permissionService.AddRoleToPermission(permissionId, roleId);
            if (!result)
            {
                return NotFound("Permission ou Role não encontrada.");
            }

            return Ok("Role adicionada à Permission com sucesso.");
        }

    }
}
