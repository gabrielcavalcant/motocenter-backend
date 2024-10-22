using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using System.Linq.Expressions;

namespace OficinaMotorcycle.API.Controllers
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            // Incluindo permissões na consulta
            var role = await _roleService.GetByIdAsync(
                id,
                cancellationToken // Passando o CancellationToken
            );

            if (role == null)
            {
                return NotFound();
            }

            // Mapeia a role para RoleDTO, incluindo as permissões
            var roleDto = _mapper.Map<RoleDtoResponse>(role);

            return Ok(roleDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(
            CancellationToken cancellationToken,
            string name = "",
            string orderBy = "",
            int pageNumber = 1,
            int pageSize = 10)
        {
            Expression<Func<Role, bool>> filter = role => string.IsNullOrEmpty(name) || role.Name.Contains(name);

            Func<IQueryable<Role>, IOrderedQueryable<Role>> orderFunc = null;
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    orderFunc = query => query.OrderBy(role => role.Name);
                }
            }

            int skip = (pageNumber - 1) * pageSize;
            int take = pageSize;

            // Incluindo permissões na consulta
            var roles = await _roleService.GetAllAsync(cancellationToken,
                filter,
                orderFunc,
                skip,
                take            );

            // Mapeia as roles para RoleDTO, incluindo as permissões
            IList<RoleDtoResponse> roleDtos = roles.Select(role => _mapper.Map<RoleDtoResponse>(role)).ToList();

            return Ok(roleDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest newRoleDto, CancellationToken cancellationToken)
        {
            if (newRoleDto == null)
            {
                return BadRequest("Role não pode ser nula."); // Retorna 400 se a role for nula
            }

            // Mapeia o DTO para a entidade Role
            var newRole = _mapper.Map<Role>(newRoleDto);

            // Cria a nova role usando o serviço
            Role createdRole = await _roleService.CreateAsync(newRole, cancellationToken);

            // Mapeia a entidade criada para o DTO
            RoleDtoResponse createdRoleDto = _mapper.Map<RoleDtoResponse>(createdRole);

            // Retorna 201 com a nova role mapeada para RoleDTO
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRoleDto.Id }, createdRoleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleRequest updatedRoleDto, CancellationToken cancellationToken)
        {
            if (updatedRoleDto == null)
            {
                return BadRequest("Role não pode ser nula.");
            }

            var roleToUpdate = await _roleService.GetByIdAsync(id, cancellationToken);
            if (roleToUpdate == null)
            {
                return NotFound("Role não encontrada.");
            }

            // Atualiza apenas as propriedades necessárias
            _mapper.Map(updatedRoleDto, roleToUpdate); // Atualiza a entidade com os dados do DTO

            await _roleService.UpdateAsync(roleToUpdate, cancellationToken);

            // Mapeia a entidade Role para RoleDTO
            var updatedRoleDtoResult = _mapper.Map<RoleDtoResponse>(roleToUpdate);

            // Retorna a role atualizada com status 200 OK
            return Ok(updatedRoleDtoResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            bool success = await _roleService.DeleteAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound(); // Retorna 404 se a deleção falhar
            }
            return NoContent(); // Retorna 204 se a deleção for bem-sucedida
        }
    }
}