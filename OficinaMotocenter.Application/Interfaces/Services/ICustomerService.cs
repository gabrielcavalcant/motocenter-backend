using OficinaMotocenter.Application.Dto.Requests.Customer;
using OficinaMotocenter.Application.Dto.Responses.Customer;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to customers.
    /// Inherits from the generic service interface and adds additional methods.
    /// </summary>
    public interface ICustomerService : IGenericService<Customer>
    {
        /// <summary>
        /// Asynchronously retrieves a customer entity by its CPF (Cadastro de Pessoas Físicas).
        /// </summary>
        /// <param name="cpf">The CPF of the customer to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer entity.</returns>
        Task<Customer> GetCustomerByCpfAsync(string cpf);

        /// <summary>
        /// Executes the creation of a new customer.
        /// </summary>
        /// <param name="request">The request DTO containing customer information.</param>
        /// <returns>A response DTO with the details of the created customer.</returns>
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);

        /// <summary>
        /// Executes the retrieval of a customer by its ID.
        /// </summary>
        /// <param name="customerId">The unique ID of the customer to retrieve.</param>
        /// <returns>A response DTO with the details of the customer.</returns>
        Task<GetCustomerByIdResponse> GetCustomerByIdAsync(Guid customerId);

        /// <summary>
        /// Executes the retrieval of all customers with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <returns>A response DTO with the list of customer and pagination details.</returns>
        Task<GetListCustomerResponse> GetListCustomerAsync(GetListCustomerRequest request);

        /// <summary>
        /// Executes the update of a customer by its ID.
        /// </summary>
        /// <param name="customerId">The unique ID of the customer to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated customer.</returns>
        Task<UpdateCustomerResponse> UpdateCustomerAsync(Guid customerId, UpdateCustomerRequest request);

        /// <summary>
        /// Executes the deletion of a customer by its ID.
        /// </summary>
        /// <param name="customerId">The unique ID of the customer to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteCustomerAsync(Guid customerId);
    }
}
