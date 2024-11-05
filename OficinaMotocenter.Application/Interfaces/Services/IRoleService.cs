using OficinaMotocenter.Application.Dto.Requests.Role;
using OficinaMotocenter.Application.Dto.Responses.Role;
using OficinaMotocenter.Application.Dto.Responses.Role.OficinaMotocenter.Application.Dto.Responses;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for role-related services, providing methods for creating, updating, retrieving, and deleting roles.
    /// </summary>
    public interface IRoleService : IGenericService<Role>
    {
        /// <summary>
        /// Retrieves a role based on its name.
        /// </summary>
        /// <param name="name">Name of the role.</param>
        /// <returns>The <see cref="Role"/> entity if found; otherwise, null.</returns>
        Task<Role> getByName(string name);

        /// <summary>
        /// Retrieves a role by its unique identifier.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> with the role details.</returns>
        Task<RoleDtoResponse> GetRoleByIdAsync(Guid roleId);

        /// <summary>
        /// Retrieves a paginated list of roles based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListRoleResponse"/> with the list of roles and total count.</returns>
        Task<GetListRoleResponse> GetListRoleAsync(GetListRoleRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new role based on the provided details.
        /// </summary>
        /// <param name="request">Details of the role to create.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> with the created role information.</returns>
        Task<RoleDtoResponse> CreateRoleAsync(CreateRoleRequest request);

        /// <summary>
        /// Updates an existing role with the specified identifier and details.
        /// </summary>
        /// <param name="id">Unique identifier of the role to update.</param>
        /// <param name="request">Updated role details.</param>
        /// <returns>A <see cref="RoleDtoResponse"/> with the updated role information.</returns>
        Task<RoleDtoResponse> UpdateRoleAsync(Guid id, UpdateRoleRequest request);

        /// <summary>
        /// Executes a soft delete on a role based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the role to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        Task<bool> DeleteRoleAsync(Guid id);
    }
}
