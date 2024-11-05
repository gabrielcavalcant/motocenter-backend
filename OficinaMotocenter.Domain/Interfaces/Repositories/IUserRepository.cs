using System.Threading.Tasks;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface for user-specific data access operations, extending generic repository functionalities.
    /// </summary>
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Retrieves a user entity by its email address.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A <see cref="User"/> entity matching the specified email, or null if not found.</returns>
        Task<User> GetByEmailAsync(string email);
    }
}
