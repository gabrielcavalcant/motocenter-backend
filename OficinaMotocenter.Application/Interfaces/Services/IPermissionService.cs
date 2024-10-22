using OficinaMotocenter.Application.Dto.Requests.Permission;
using OficinaMotocenter.Application.Dto.Responses.Permission;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPermissionService : IGenericService<Permission>
    {
        Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId, CancellationToken cancellationToken);

        Task<PermissionDtoResponse> GetPermissionByIdAsync (Guid permissionId, CancellationToken cancellationToken);

        Task<GetListPermissionResponse> GetListPermissionAsync(GetListPermissionRequest request, CancellationToken cancellationToken);

    }
}
