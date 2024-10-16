using Microsoft.Extensions.Logging;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service implementation for customer-specific operations.
    /// Inherits basic CRUD operations from the generic service.
    /// </summary>
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The repository for customer operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
            : base(customerRepository, logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a customer by their CPF asynchronously.
        /// </summary>
        /// <param name="cpf">The CPF of the customer to retrieve.</param>
        /// <returns>The customer entity, or null if not found.</returns>
        public async Task<Customer> GetCustomerByCpf(string cpf)
        {
            return await _customerRepository.GetCustomerByCpfAsync(cpf);
        }
    }
}
