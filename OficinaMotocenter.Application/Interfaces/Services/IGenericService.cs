using System.Linq.Expressions;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a generic service interface for performing CRUD operations and business logic.
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on.</typeparam>
    public interface IGenericService<T> where T : class
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<T> GetByIdAsync(Guid entityId);

        /// <summary>
        /// Asynchronously retrieves a list of entities, optionally filtered, ordered, skipped, and taken.
        /// </summary>
        /// <param name="filter">An optional expression to filter the entities.</param>
        /// <param name="orderBy">An optional function to order the entities.</param>
        /// <param name="skip">An optional parameter to skip a number of entities.</param>
        /// <param name="take">An optional parameter to take a limited number of entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// Asynchronously creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created entity.</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="updatedEntity">The entity with updated values.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        Task<T> UpdateAsync(T updatedEntity);

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(Guid entityId);
    }
}
