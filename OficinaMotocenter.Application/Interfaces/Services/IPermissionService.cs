using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for permission-related services, providing methods for CRUD operations and role association.
    /// </summary>
    public interface IPermissionService : IGenericService<Permission>
    {
        /// <summary>
        /// Retrieves a permission by its name.
        /// </summary>
        /// <param name="name">Name of the permission.</param>
        /// <returns>The <see cref="Permission"/> entity if found; otherwise, null.</returns>
        Task<Permission> GetByNameAsync(string name);

        /// <summary>
        /// Associates a role with a specific permission.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission.</param>
        /// <param name="roleId">Unique identifier of the role to associate.</param>
        /// <returns>A boolean indicating success or failure of the association.</returns>
        Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId);

        /// <summary>
        /// Retrieves a permission by its unique identifier.
        /// </summary>
        /// <param name="permissionId">Unique identifier of the permission.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> with permission details.</returns>
        Task<PermissionDtoResponse> GetPermissionByIdAsync(Guid permissionId);

        /// <summary>
        /// Retrieves a paginated list of permissions based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListPermissionResponse"/> with the list of permissions and total count.</returns>
        Task<GetListPermissionResponse> GetListPermissionAsync(GetListPermissionRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new permission with the specified details.
        /// </summary>
        /// <param name="request">Details of the permission to create.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> with the created permission information.</returns>
        Task<PermissionDtoResponse> CreatePermissionAsync(CreatePermissionRequest request);

        /// <summary>
        /// Updates an existing permission with the specified identifier and details.
        /// </summary>
        /// <param name="id">Unique identifier of the permission to update.</param>
        /// <param name="request">Updated permission details.</param>
        /// <returns>A <see cref="PermissionDtoResponse"/> with the updated permission information.</returns>
        Task<PermissionDtoResponse> UpdatePermissionAsync(Guid id, UpdatePermissionRequest request);

        /// <summary>
        /// Executes a soft delete on a permission based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the permission to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        Task<bool> DeletePermissionAsync(Guid id);
    }
}
