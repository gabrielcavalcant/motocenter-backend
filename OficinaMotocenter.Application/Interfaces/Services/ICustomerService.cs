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
        Task<Customer> GetCustomerByCpf(string cpf);
    }
}
