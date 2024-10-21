using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IPermissionService : IGenericService<Permission>
    {
        Task<Permission> GetByNameAsync(string name);

        Task<bool> AddRoleToPermission(Guid permissionId, Guid roleId);
    }
}
