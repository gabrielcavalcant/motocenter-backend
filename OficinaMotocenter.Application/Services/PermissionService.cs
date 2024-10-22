using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    public class PermissionService : GenericService<Permission>, IPermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;


        public PermissionService(IRoleRepository roleRepository,
                                 IPermissionRepository permissionRepository,
                                 IUnitOfWork unitOfWork, 
                                 ILogger<PermissionService> logger) : base(permissionRepository, unitOfWork, logger)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetByNameAsync(name, cancellationToken);
        }

        // Método para adicionar uma Role a uma Permission
        public async Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId, CancellationToken cancellationToken)
        {
            // Busca a permissão pelo ID
            var permission = await GetByIdAsync(permissionId, cancellationToken);

            if (permission == null)
            {
                return false; // Retorna falso se a permissão não for encontrada
            }

            // Busca a Role pelo ID
            var role = await _roleRepository.GetByIdAsync(roleId, cancellationToken);
            if (role == null)
            {
                return false; // Retorna falso se a Role não for encontrada
            }

            // Adiciona a Role à Permission, caso ainda não esteja associada
            if (!permission.Roles.Contains(role))
            {
                permission.Roles.Add(role);
                await _unitOfWork.Commit(cancellationToken); // Salva as alterações no banco de dados
            }

            return true; // Retorna verdadeiro se a operação for bem-sucedida
        }
    }
}
