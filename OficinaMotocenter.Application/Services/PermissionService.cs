using AutoMapper;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for managing permissions, including CRUD operations and role association.
    /// </summary>
    public class PermissionService : GenericService<Permission>, IPermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService"/> class.
        /// </summary>
        /// <param name="roleRepository">Repository for accessing role data.</param>
        /// <param name="permissionRepository">Repository for accessing permission data.</param>
        /// <param name="unitOfWork">Unit of work for managing transactions.</param>
        /// <param name="mapper">Mapper for entity-DTO conversions.</param>
        /// <param name="logger">Logger for logging operations.</param>
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

        /// <summary>
        /// Creates a new permission based on the provided details.
        /// </summary>
        /// <param name="request">Details of the permission to create.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> containing the created permission details.</returns>
        public async Task<PermissionDtoResponse> CreatePermissionAsync(CreatePermissionRequest request)
        {
            Permission newPermission = _mapper.Map<Permission>(request);
            Permission createdPermission = await CreateAsync(newPermission);
            return _mapper.Map<PermissionDtoResponse>(createdPermission);
        }

        /// <summary>
        /// Updates an existing permission based on its identifier and update details.
        /// </summary>
        /// <param name="id">Unique identifier of the permission to update.</param>
        /// <param name="request">Updated permission details.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> containing the updated permission details.</returns>
        public async Task<PermissionDtoResponse> UpdatePermissionAsync(Guid id, UpdatePermissionRequest request)
        {
            Permission permissionToUpdate = await GetByIdAsync(id);

            if (permissionToUpdate == null)
            {
                throw new InvalidArgumentException("Permission not found");
            }

            _mapper.Map(request, permissionToUpdate);
            await UpdateAsync(permissionToUpdate);
            return _mapper.Map<PermissionDtoResponse>(permissionToUpdate);
        }

        /// <summary>
        /// Retrieves a permission by its name.
        /// </summary>
        /// <param name="name">Name of the permission.</param>
        /// <returns>The <see cref="Permission"/> entity if found; otherwise, null.</returns>
        public async Task<Permission> GetByNameAsync(string name)
        {
            return await _permissionRepository.GetByNameAsync(name);
        }

        /// <summary>
        /// Associates a role with a specific permission.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission.</param>
        /// <param name="roleId">Unique identifier of the role to associate.</param>
        /// <returns>A boolean indicating success or failure of the association.</returns>
        public async Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId)
        {
            var permission = await GetByIdAsync(permissionId);

            if (permission == null)
            {
                return false;
            }

            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            if (!permission.Roles.Contains(role))
            {
                permission.Roles.Add(role);
                await _unitOfWork.Commit();
            }

            return true;
        }

        /// <summary>
        /// Retrieves a permission by its unique identifier.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> with permission details, or null if not found.</returns>
        public async Task<PermissionDtoResponse> GetPermissionByIdAsync(Guid permissionId)
        {
            Permission permission = await GetByIdAsync(permissionId);
            return _mapper.Map<PermissionDtoResponse>(permission);
        }

        /// <summary>
        /// Retrieves a paginated list of permissions based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListPermissionResponse"/> containing the filtered permissions and total count.</returns>
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
        /// Executes the soft delete of an existing permission.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission to delete.</param>
        /// <returns>A boolean result indicating success or failure of the deletion.</returns>
        public async Task<bool> DeletePermissionAsync(Guid permissionId)
        {
            bool permissionDeleted = await base.DeleteAsync(permissionId);
            return permissionDeleted;
        }
    }
}
