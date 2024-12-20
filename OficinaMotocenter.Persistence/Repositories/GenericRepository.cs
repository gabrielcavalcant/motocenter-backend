﻿using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;
using System.Linq.Expressions;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Generic repository that provides basic CRUD methods for entities inheriting from <see cref="BaseEntity"/>.
    /// </summary>
    /// <typeparam name="T">The type of the entity that inherits from <see cref="BaseEntity"/>.</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity corresponding to the provided ID.</returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all entities that are not marked as deleted.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A query that returns non-deleted entities.</returns>
        public IQueryable<T> GetAll(CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(e => !e.DateDeleted.HasValue);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        public async Task AddAsync(T entity)
        {
            entity.DateCreated = DateTimeOffset.UtcNow;
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public async Task UpdateAsync(T entity)
        {
            entity.DateUpdated = DateTimeOffset.UtcNow;
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Marks an entity as deleted (soft delete).
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.DateDeleted = DateTimeOffset.UtcNow; // Marking as soft-deleted
                _dbSet.Update(entity);
            }
        }
    }
}
