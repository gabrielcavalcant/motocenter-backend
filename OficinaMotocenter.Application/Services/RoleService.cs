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
    /// <summary>
    /// Service for handling role-related business logic, including CRUD operations and role retrieval by filters.
    /// </summary>
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="roleRepository">Repository for role data access.</param>
        /// <param name="logger">Logger for logging operations.</param>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        /// <param name="mapper">Mapper for mapping between entities and DTOs.</param>
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

        /// <summary>
        /// Creates a new role based on the provided request details.
        /// </summary>
        /// <param name="request">Details of the role to create.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> containing details of the newly created role.</returns>
        public async Task<RoleDtoResponse> CreateRoleAsync(CreateRoleRequest request)
        {
            Role newRole = _mapper.Map<Role>(request);
            Role createdRole = await CreateAsync(newRole);
            return _mapper.Map<RoleDtoResponse>(createdRole);
        }

        /// <summary>
        /// Updates an existing role based on the specified ID and update details.
        /// </summary>
        /// <param name="id">Unique identifier of the role to update.</param>
        /// <param name="request">Updated role details.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> containing updated role information.</returns>
        public async Task<RoleDtoResponse> UpdateRoleAsync(Guid id, UpdateRoleRequest request)
        {
            Role RoleToUpdate = await GetByIdAsync(id);

            if (RoleToUpdate == null)
            {
                throw new InvalidArgumentException("Role not found");
            }

            _mapper.Map(request, RoleToUpdate);
            await UpdateAsync(RoleToUpdate);
            return _mapper.Map<RoleDtoResponse>(RoleToUpdate);
        }

        /// <summary>
        /// Retrieves a role by its name.
        /// </summary>
        /// <param name="name">Name of the role.</param>
        /// <returns>The <see cref="Role"/> entity if found; otherwise, null.</returns>
        public async Task<Role> getByName(string name)
        {
            return await _roleRepository.GetByNameAsync(name);
        }

        /// <summary>
        /// Retrieves a role by its unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> with role details or null if not found.</returns>
        public async Task<RoleDtoResponse> GetRoleByIdAsync(Guid roleId)
        {
            Role role = await GetByIdAsync(roleId);
            return _mapper.Map<RoleDtoResponse>(role);
        }

        /// <summary>
        /// Retrieves a list of roles based on filter criteria and pagination settings.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListRoleResponse"/> containing the filtered roles and total count.</returns>
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
        /// Executes the soft delete of an existing role.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete.</param>
        /// <returns>A boolean result indicating the success of the deletion.</returns>
        public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            bool roleDeleted = await base.DeleteAsync(roleId);
            return roleDeleted;
        }
    }
}
