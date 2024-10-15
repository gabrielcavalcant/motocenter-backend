namespace OficinaMotocenter.Application.Dto.Requests.Motorcycle
{
    /// <summary>
    /// DTO for linking a motorcycle to a customer based on their CPF.
    /// </summary>
    public class LinkMotorcycleToCustomerRequest
    {
        /// <summary>
        /// The unique identifier of the motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// The CPF of the customer to link the motorcycle to.
        /// </summary>
        public string CustomerCpf { get; set; }
    }
}
