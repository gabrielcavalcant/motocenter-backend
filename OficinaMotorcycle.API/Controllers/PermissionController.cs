using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using System.Linq.Expressions;

namespace OficinaMotorcycle.API.Controllers
{
    [ApiController]
    [Route("api/permission")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionService permissionService, IRoleService roleService, IMapper mapper)
        {
            _permissionService = permissionService;
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermission(Guid id, CancellationToken cancellationToken)
        {
            var Permission = await _permissionService.GetByIdAsync(id, cancellationToken);
            if (Permission == null)
            {
                return NotFound(); // Retorna 404 se a Permission não for encontrada
            }

            // Mapeia a entidade Permission para PermissionDTO automaticamente usando AutoMapper
            var PermissionDto = _mapper.Map<PermissionDtoResponse>(Permission);

            return Ok(PermissionDto); // Retorna a Permission encontrada como DTO
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions(
        CancellationToken cancellationToken,
         string name = "",
         string orderBy = "",
         int pageNumber = 1,
         int pageSize = 10)
        {
            Expression<Func<Permission, bool>> filter = Permission => name == null || Permission.Name.Contains(name);

            Func<IQueryable<Permission>, IOrderedQueryable<Permission>> orderFunc = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    orderFunc = query => query.OrderBy(Permission => Permission.Name);
                }

            }

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            IList<Permission> Permissions = await _permissionService.GetAllAsync(cancellationToken, filter, orderFunc, skip, take);
            IList<PermissionDtoResponse> PermissionDtos = Permissions.Select(Permission => _mapper.Map<PermissionDtoResponse>(Permission)).ToList();

            return Ok(PermissionDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest newPermissionDto, CancellationToken cancellationToken)
        {
            if (newPermissionDto == null)
            {
                return BadRequest("Permission não pode ser nula."); // Retorna 400 se a Permission for nula
            }

            // Mapeia o DTO para a entidade Permission
            Permission newPermission = _mapper.Map<Permission>(newPermissionDto);

            // Cria a nova Permission usando o serviço
            Permission createdPermission = await _permissionService.CreateAsync(newPermission, cancellationToken);

            // Mapeia a entidade criada para o DTO
            PermissionDtoResponse createdPermissionDto = _mapper.Map<PermissionDtoResponse>(createdPermission);

            // Retorna 201 com a nova Permission mapeada para PermissionDTO
            return CreatedAtAction(nameof(GetPermission), new { id = createdPermissionDto.Id }, createdPermissionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] UpdatePermissionRequest updatedPermissionDto, CancellationToken cancellationToken)
        {
            if (updatedPermissionDto == null)
            {
                return BadRequest("Permission não pode ser nula.");
            }

            var PermissionToUpdate = await _permissionService.GetByIdAsync(id, cancellationToken);
            if (PermissionToUpdate == null)
            {
                return NotFound("Permission não encontrada.");
            }

            // Atualiza apenas as propriedades necessárias
            _mapper.Map(updatedPermissionDto, PermissionToUpdate); // Atualiza a entidade com os dados do DTO

            await _permissionService.UpdateAsync(PermissionToUpdate, cancellationToken);

            // Mapeia a entidade Permission para PermissionDTO
            var updatedPermissionDtoResult = _mapper.Map<PermissionDtoResponse>(PermissionToUpdate);

            // Retorna a Permission atualizada com status 200 OK
            return Ok(updatedPermissionDtoResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id, CancellationToken cancellationToken)
        {
            bool success = await _permissionService.DeleteAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound(); // Retorna 404 se a deleção falhar
            }
            return NoContent(); // Retorna 204 se a deleção for bem-sucedida
        }

        [HttpPost("{permissionId}/roles/{roleId}")]
        public async Task<IActionResult> AddRoleToPermission(Guid permissionId, Guid roleId, CancellationToken cancellationToken)
        {
            var result = await _permissionService.AddRoleToPermission(permissionId, roleId, cancellationToken);
            if (!result)
            {
                return NotFound("Permission ou Role não encontrada.");
            }

            return Ok("Role adicionada à Permission com sucesso.");
        }

    }
}
