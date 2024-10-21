using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken);

    }
}
