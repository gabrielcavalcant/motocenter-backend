using Microsoft.Extensions.Logging;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using AutoMapper;
using OficinaMotocenter.Application.Dto.Responses.Customer;
using OficinaMotocenter.Application.Dto.Requests.Customer;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Application.Exceptions;

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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The repository for customer operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="mapper">The auto mapper instance for mapping object operations.</param>
        /// <param name="unitOfWork">The unit of work for customer operations.</param>

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger, IMapper mapper, IUnitOfWork unitOfWork)
            : base(customerRepository, unitOfWork, logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves a customer by their CPF asynchronously.
        /// </summary>
        /// <param name="cpf">The CPF of the customer to retrieve.</param>
        /// <returns>The customer entity, or null if not found.</returns>
        public async Task<Customer> GetCustomerByCpfAsync(string cpf)
        {
            return await _customerRepository.GetCustomerByCpfAsync(cpf);
        }

        /// <summary>
        /// Executes the creation of a new customer.
        /// </summary>
        /// <param name="request">The request DTO containing customer information.</param>
        /// <returns>A response DTO with the details of the created customer.</returns>
        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            Customer customer = _mapper.Map<Customer>(request);
            _logger.LogInformation("Creating customer: {@customer}", customer);
            Customer createdCustomer = await base.CreateAsync(customer);
            return _mapper.Map<CreateCustomerResponse>(createdCustomer);
        }

        /// <summary>
        /// Executes the retrieval of a customer by its ID.
        /// </summary>
        /// <param name="customerId">The unique ID of the customer to retrieve.</param>
        /// <returns>A response DTO with the details of the customer.</returns>
        public async Task<GetCustomerByIdResponse> GetCustomerByIdAsync(Guid customerId)
        {
            _logger.LogInformation("Searching customer using GUID: {@id}", customerId);
            Customer customer = await base.GetByIdAsync(customerId);
            if(customer == null)
            {
                throw new InvalidArgumentException("Customer not found");
            }
            return _mapper.Map<GetCustomerByIdResponse>(customer);
        }

        /// <summary>
        /// Executes the retrieval of all customers with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of customers and pagination details.</returns>
        public async Task<GetListCustomerResponse> GetListCustomerAsync(GetListCustomerRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get customer list using: {@request}", request);

            IList<Customer> customerList = await base.GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)) &&
                             (string.IsNullOrEmpty(request.Telephone) || m.Telephone.Contains(request.Telephone)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );
            GetListCustomerResponse response = _mapper.Map<GetListCustomerResponse>(customerList);
            response.TotalCount = customerList.Count;
            return response;
        }

        /// <summary>
        /// Executes the update of a existent customer.
        /// </summary>
        /// <param name="customerId">customer Id</param>
        /// <param name="request">The request DTO containing customer information.</param>
        /// <returns>A response DTO with the details of the updated customer.</returns>
        public async Task<UpdateCustomerResponse> UpdateCustomerAsync(Guid customerId, UpdateCustomerRequest request)
        {
            Customer customer = await base.GetByIdAsync(customerId);
            _mapper.Map(request, customer);
            Customer updatedCustomer = await base.UpdateAsync(customer);
            updatedCustomer = await base.GetByIdAsync(updatedCustomer.CustomerId);
            UpdateCustomerResponse response = _mapper.Map<UpdateCustomerResponse>(updatedCustomer);
            return response;
        }

        /// <summary>
        /// Executes the soft delete of a existent customer.
        /// </summary>
        /// <param name="customerId">customer Id</param>
        /// <returns>A boolean result</returns>
        public async Task<bool> DeleteCustomerAsync(Guid customerId)
        {
            bool customerDeleted = await base.DeleteAsync(customerId);
            return customerDeleted;
        }
    }
}
