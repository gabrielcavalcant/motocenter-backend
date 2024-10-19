using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using System.Linq.Expressions;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using System.Threading;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Generic service that provides basic CRUD operations for any entity type.
    /// </summary>
    /// <typeparam name="T">The entity type that this service handles.</typeparam>
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenericService<T>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericService{T}"/> class.
        /// </summary>
        /// <param name="genericRepository">The generic repository for the entity type.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public GenericService(IGenericRepository<T> genericRepository,IUnitOfWork unitOfWork, ILogger<GenericService<T>> logger)
        {
            _genericRepository = genericRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity to retrieve.</param>
        /// <returns>The entity instance, or null if not found.</returns>
        public async Task<T> GetByIdAsync(Guid entityId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to retrieve entity of type {EntityType} with ID {EntityId}.", typeof(T).Name, entityId);
            var entity = await _genericRepository.GetByIdAsync(entityId, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} with ID {EntityId} was not found.", typeof(T).Name, entityId);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved entity of type {EntityType} with ID {EntityId}.", typeof(T).Name, entityId);
            }
            return entity;
        }

        /// <summary>
        /// Retrieves all entities based on optional filter, ordering, skip, and take parameters asynchronously.
        /// </summary>
        /// <param name="filter">The expression filter to apply to the query.</param>
        /// <param name="orderBy">The ordering function for the query.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="take">The number of records to take.</param>
        /// <returns>A list of entities that match the query.</returns>
        public async Task<List<T>> GetAllAsync(
                   CancellationToken cancellationToken,
                   Expression<Func<T, bool>> filter = null,
                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                   int? skip = null,
                   int? take = null)
        {
            _logger.LogInformation("Retrieving all entities of type {EntityType} with filter {Filter}.", typeof(T).Name, filter);

            IQueryable<T> query = _genericRepository.GetAll(cancellationToken);

            if (filter != null)
            {
                query = query.Where(filter);
                _logger.LogInformation("Applied filter to the query.");
            }

            if (orderBy != null)
            {
                query = orderBy(query);
                _logger.LogInformation("Applied ordering to the query.");
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
                _logger.LogInformation("Applied skip of {Skip} records to the query.", skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
                _logger.LogInformation("Applied take of {Take} records to the query.", take.Value);
            }

            var result = await query.ToListAsync();
            _logger.LogInformation("Successfully retrieved {Count} entities of type {EntityType}.", result.Count, typeof(T).Name);

            return result;
        }

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new entity of type {EntityType}.", typeof(T).Name);
            await _genericRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            _logger.LogInformation("Successfully created entity of type {EntityType}.", typeof(T).Name);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        /// <param name="updatedEntity">The updated entity to save.</param>
        /// <returns>The updated entity.</returns>
        public async Task<T> UpdateAsync(T updatedEntity, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating entity of type {EntityType}.", typeof(T).Name);
            await _genericRepository.UpdateAsync(updatedEntity, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            _logger.LogInformation("Successfully updated entity of type {EntityType}.", typeof(T).Name);
            return updatedEntity;
        }

        /// <summary>
        /// Deletes an entity by its unique identifier asynchronously.
        /// </summary>
        /// <param name="entityId">The unique identifier of the entity to delete.</param>
        /// <returns>A boolean indicating whether the entity was successfully deleted.</returns>
        public async Task<bool> DeleteAsync(Guid entityId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete entity of type {EntityType} with ID {EntityId}.", typeof(T).Name, entityId);
            await _genericRepository.DeleteAsync(entityId, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);
            _logger.LogInformation("Successfully deleted entity of type {EntityType} with ID {EntityId}.", typeof(T).Name, entityId);
            return true;
        }
    }
}
