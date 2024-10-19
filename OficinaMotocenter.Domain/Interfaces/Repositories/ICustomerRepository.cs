using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Defines a repository interface for performing CRUD operations on customers.
    /// Inherits from the generic repository interface and adds additional methods.
    /// </summary>
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        /// <summary>
        /// Asynchronously retrieves a customer entity by its CPF (Cadastro de Pessoas Físicas).
        /// </summary>
        /// <param name="cpf">The CPF of the customer to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer entity.</returns>
        Task<Customer> GetCustomerByCpfAsync(string cpf, CancellationToken cancellationToken);
    }
}
