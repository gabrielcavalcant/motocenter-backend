using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Persistence.Context;
using OficinaMotocenter.Persistence.Repositories;

namespace OficinaMotocenter.Persistence.UnitOfWork
{
    /// <summary>
    /// Implements the Unit of Work pattern to manage transactions and coordinate database operations.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private  AppDbContext _context = null;
        private  GenericRepository<Motorcycle> motorcycleRepository = null;
        private  GenericRepository<Customer> customerRepository = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context used for database operations.</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commits all changes made in the current transaction to the database.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<Motorcycle> MotorcycleRepository
        {
            get
            {
                if (motorcycleRepository == null)
                {
                    motorcycleRepository = new GenericRepository<Motorcycle>(_context);
                }
                return motorcycleRepository;
            }
        }
        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new GenericRepository<Customer>(_context);
                }
                return customerRepository;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
