using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Persistence.Context;
using System.Threading;

namespace OficinaMotocenter.Persistence.Repositories
{
    /// <summary>
    /// Repository specific for the <see cref="Customer"/> entity.
    /// </summary>
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        /// <summary>
        /// Retrieves a customer by their CPF.
        /// </summary>
        /// <param name="cpf">The CPF of the customer to retrieve.</param>
        /// <returns>The customer corresponding to the provided CPF.</returns>
        public async Task<Customer> GetCustomerByCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            return await _context.Set<Customer>().FirstOrDefaultAsync(customer => customer.Cpf == cpf);
        }
    }
}
