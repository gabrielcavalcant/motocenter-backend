namespace OficinaMotocenter.Application.Dto.Requests.Customer
{
    /// <summary>
    /// DTO for retrieving a paginated list of customers with optional filters.
    /// </summary>
    public class GetListCustomerRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional filter by the telephone number of the customer.
        /// </summary>
        public string? Telephone { get; set; }
    }
}
