namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines a generic repository interface for performing CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all entities as an IQueryable.
        /// </summary>
        /// <returns>An IQueryable of all entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Asynchronously adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Asynchronously updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Asynchronously saves all changes made to the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveChangesAsync();
    }
}
