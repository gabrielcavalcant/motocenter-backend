using AutoMapper;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Threading;

namespace OficinaMotocenter.Application.Services
{
    public class PermissionService : GenericService<Permission>, IPermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;


        public PermissionService(IRoleRepository roleRepository,
                                 IPermissionRepository permissionRepository,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 ILogger<PermissionService> logger) : base(permissionRepository, unitOfWork, logger)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PermissionDtoResponse> CreatePermissionAsync(CreatePermissionRequest request)
        {
            // Mapeia o DTO para a entidade Permission
            Permission newPermission = _mapper.Map<Permission>(request);

            // Cria a nova Permission usando o serviço
            Permission createdPermission = await CreateAsync(newPermission);

            // Mapeia a entidade criada para o DTO
            return _mapper.Map<PermissionDtoResponse>(createdPermission);
        }

        public async Task<PermissionDtoResponse> UpdatePermissionAsync(Guid id, UpdatePermissionRequest request)
        {
            Permission PermissionToUpdate = await GetByIdAsync(id);
            
            if (PermissionToUpdate == null)
            {
                throw new InvalidArgumentException("Permission not found");
            }

            // Atualiza apenas as propriedades necessárias
            _mapper.Map(request, PermissionToUpdate); // Atualiza a entidade com os dados do DTO

            await UpdateAsync(PermissionToUpdate);

            // Mapeia a entidade Permission para PermissionDTO
            return _mapper.Map<PermissionDtoResponse>(PermissionToUpdate);
        }

        public async Task<Permission> GetByNameAsync(string name)
        {
            return await _permissionRepository.GetByNameAsync(name);
        }

        // Método para adicionar uma Role a uma Permission
        public async Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId)
        {
            // Busca a permissão pelo ID
            var permission = await GetByIdAsync(permissionId);

            if (permission == null)
            {
                return false; // Retorna falso se a permissão não for encontrada
            }

            // Busca a Role pelo ID
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                return false; // Retorna falso se a Role não for encontrada
            }

            // Adiciona a Role à Permission, caso ainda não esteja associada
            if (!permission.Roles.Contains(role))
            {
                permission.Roles.Add(role);
                await _unitOfWork.Commit(); // Salva as alterações no banco de dados
            }

            return true; // Retorna verdadeiro se a operação for bem-sucedida
        }

        public async Task<PermissionDtoResponse> GetPermissionByIdAsync(Guid permissionId)
        {
            Permission permission = await GetByIdAsync(permissionId);
            return _mapper.Map<PermissionDtoResponse>(permission);
        }

        public async Task<GetListPermissionResponse> GetListPermissionAsync(GetListPermissionRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get permission list using: {@request}", request);

            IList<Permission> permissionList = await GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );

            GetListPermissionResponse response = _mapper.Map<GetListPermissionResponse>(permissionList);
            response.TotalCount = permissionList.Count;
            return response;
        }

        /// <summary>
        /// Executes the soft delete of a existent permission.
        /// </summary>
        /// <param name="id">permission Id</param>
        /// <returns>A boolean result</returns>
        public async Task<bool> DeletePermissionAsync(Guid id )
        {
            bool motorcycleDeleted = await base.DeleteAsync(id);
            return motorcycleDeleted;
        }
    }
}
