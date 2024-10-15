namespace OficinaMotocenter.Application.Dto.Responses.Customer
{
    /// <summary>
    /// DTO for the response when retrieving a customer by their unique identifier (ID).
    /// Contains the details of the customer.
    /// </summary>
    public class GetCustomerByIdResponse
    {
        /// <summary>
        /// The unique identifier of the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The name of the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The customer's telephone number.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// The CPF of the customer, which is a unique identifier in Brazil.
        /// </summary>
        public string Cpf { get; set; }
    }
}
