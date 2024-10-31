﻿using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;

namespace OficinaMotocenter.Domain.Interfaces.UnitOfWork
{

    /// <summary>
    /// Represents the Unit of Work pattern for coordinating the writing of changes to the database.
    /// </summary>
    public interface IUnitOfWork
    {
        IGenericRepository<Motorcycle> MotorcycleRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Permission> PermissionRepository { get; }

        /// <summary>
        /// Commits all changes made in the current transaction.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Commit();
    }
}

