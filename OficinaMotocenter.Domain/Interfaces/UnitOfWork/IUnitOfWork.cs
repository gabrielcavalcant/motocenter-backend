using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;

namespace OficinaMotocenter.Domain.Interfaces.UnitOfWork
{
    /// <summary>
    /// Represents the Unit of Work pattern for coordinating the writing of changes to the database,
    /// ensuring that all repositories work within a single transaction context.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the repository for managing <see cref="Motorcycle"/> entities.
        /// </summary>
        IGenericRepository<Motorcycle> MotorcycleRepository { get; }

        /// <summary>
        /// Gets the repository for managing <see cref="Customer"/> entities.
        /// </summary>
        IGenericRepository<Customer> CustomerRepository { get; }

        /// <summary>
        /// Gets the repository for managing <see cref="User"/> entities.
        /// </summary>
        IGenericRepository<User> UserRepository { get; }

        /// <summary>
        /// Gets the repository for managing <see cref="Role"/> entities.
        /// </summary>
        IGenericRepository<Role> RoleRepository { get; }

        /// <summary>
        /// Gets the repository for managing <see cref="Permission"/> entities.
        /// </summary>
        IGenericRepository<Permission> PermissionRepository { get; }

        /// <summary>
        /// Commits all changes made in the current transaction, ensuring data consistency.
        /// </summary>
        /// <returns>A task representing the asynchronous commit operation.</returns>
        Task Commit();
    }
}
