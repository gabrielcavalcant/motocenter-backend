﻿namespace OficinaMotocenter.Application.Dto.Responses.Customer
{
    /// <summary>
    /// DTO for returning a paginated list of customers. It includes a list of customers,
    /// the current page index, and the total count of customers.
    /// </summary>
    public class GetListCustomerResponse : GenericListResponse
    {
        /// <summary>
        /// A list of customer for the current page.
        /// </summary>
        public List<CustomerDto> Customers { get; set; }
    }

    /// <summary>
    /// DTO that represents a single customer in the list.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// The unique identifier of the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The name of the customer.
        /// </summary>
        public string Name { get; set; }
    }
}
