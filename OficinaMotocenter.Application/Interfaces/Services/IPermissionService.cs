using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPermissionService : IGenericService<Permission>
    {
        Task<Permission> GetByNameAsync(string name);

        Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId);

        Task<PermissionDtoResponse> GetPermissionByIdAsync (Guid permissionId);

        Task<GetListPermissionResponse> GetListPermissionAsync(GetListPermissionRequest request, CancellationToken cancellationToken);

        Task<PermissionDtoResponse> CreatePermissionAsync(CreatePermissionRequest request);

        Task<PermissionDtoResponse> UpdatePermissionAsync(Guid id, UpdatePermissionRequest request);

        Task<bool> DeletePermissionAsync(Guid id);

    }
}
