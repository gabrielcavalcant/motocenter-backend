using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPermissionService : IGenericService<Permission>
    {
        Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken);

        Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
    }
}
