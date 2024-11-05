using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for permission-specific data access operations, extending generic repository functionalities.
    /// </summary>
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
        /// <summary>
        /// Retrieves a permission entity by its name.
        /// </summary>
        /// <param name="name">The name of the permission.</param>
        /// <returns>A <see cref="Permission"/> entity matching the specified name, or null if not found.</returns>
        Task<Permission> GetByNameAsync(string name);
    }
}
