namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a customer entity within the system.
    /// </summary>
    public sealed class Customer : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the telephone number of the customer.
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the CPF (Cadastro de Pessoas Físicas) of the customer.
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Gets or sets the collection of motorcycles associated with the customer.
        /// </summary>
        public ICollection<Motorcycle> Motorcycles { get; set; }
    }
}
