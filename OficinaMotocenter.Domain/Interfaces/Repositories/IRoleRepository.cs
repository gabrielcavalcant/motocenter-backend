using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for role-specific data access operations, extending generic repository functionalities.
    /// </summary>
    public interface IRoleRepository : IGenericRepository<Role>
    {
        /// <summary>
        /// Retrieves a role entity by its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>A <see cref="Role"/> entity matching the specified name, or null if not found.</returns>
        Task<Role> GetByNameAsync(string name);
    }
}
