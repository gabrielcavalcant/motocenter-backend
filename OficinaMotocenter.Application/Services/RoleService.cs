using AutoMapper;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository,
                                 ILogger<RoleService> logger,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper)
                                 : base(roleRepository, unitOfWork, logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RoleDtoResponse> CreateRoleAsync(CreateRoleRequest request)
        {
            // Mapeia o DTO para a entidade Role
            Role newRole = _mapper.Map<Role>(request);

            // Cria a nova Role usando o serviço
            Role createdRole = await CreateAsync(newRole);

            // Mapeia a entidade criada para o DTO
            return _mapper.Map<RoleDtoResponse>(createdRole);
        }

        public async Task<RoleDtoResponse> UpdateRoleAsync(Guid id, UpdateRoleRequest request)
        {
            Role RoleToUpdate = await GetByIdAsync(id);

            if (RoleToUpdate == null)
            {
                throw new InvalidArgumentException("Role not found");
            }

            // Atualiza apenas as propriedades necessárias
            _mapper.Map(request, RoleToUpdate); // Atualiza a entidade com os dados do DTO

            await UpdateAsync(RoleToUpdate);

            // Mapeia a entidade Role para RoleDTO
            return _mapper.Map<RoleDtoResponse>(RoleToUpdate);
        }

        public async Task<Role> getByName(string name)
        {
            return await _roleRepository.GetByNameAsync(name);
        }

        public async Task<RoleDtoResponse> GetRoleByIdAsync(Guid roleId)
        {
            Role role = await GetByIdAsync(roleId);
            return _mapper.Map<RoleDtoResponse>(role);
        }

        public async Task<GetListRoleResponse> GetListRoleAsync(GetListRoleRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get Role list using: {@request}", request);

            IList<Role> roleList = await GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );

            GetListRoleResponse response = _mapper.Map<GetListRoleResponse>(roleList);
            response.TotalCount = roleList.Count;
            return response;
        }

        /// <summary>
        /// Executes the soft delete of a existent role.
        /// </summary>
        /// <param name="id">role Id</param>
        /// <returns>A boolean result</returns>
        public async Task<bool> DeleteRoleAsync(Guid id)
        {
            bool motorcycleDeleted = await base.DeleteAsync(id);
            return motorcycleDeleted;
        }
    }
}

