namespace OficinaMotocenter.Application.Dto.Responses.Motorcycle
{
    /// <summary>
    /// DTO for the response after linking a motorcycle to a customer. Contains the ID
    /// of the linked motorcycle.
    /// </summary>
    public class LinkMotorcycleToCustomerResponse
    {
        /// <summary>
        /// The unique identifier of the motorcycle that was linked to a customer.
        /// </summary>
        public Guid MotorcycleId { get; set; }
    }
}
