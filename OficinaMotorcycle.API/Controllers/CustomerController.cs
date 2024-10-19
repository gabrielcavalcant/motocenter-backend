using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Customer;
using OficinaMotocenter.Application.Dto.Responses.Customer;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers

{
    /// <summary>
    /// Controller for managing customer-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="customerService">Service to manage customer operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public CustomerController(

            ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">The customer data transfer object containing customer details.</param>
        /// <returns>A created customer response along with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request initiated");

            CreateCustomerResponse response = await _customerService.CreateCustomerAsync(request, cancellationToken);

            _logger.LogInformation("Response: {@response}", response);
            return CreatedAtAction(nameof(Get), new { customerId = response.CustomerId }, response);
        }

        /// <summary>
        /// Retrieves a customer by its ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to retrieve.</param>
        /// <returns>The customer details if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(Guid customerId, CancellationToken cancellationToken)
        {
            GetCustomerByIdResponse response = await _customerService.GetCustomerByIdAsync(customerId, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a customer list.
        /// </summary>
        /// <param name="request">The request object for retrieving a customer list.</param>
        /// <returns>A list of customers.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListCustomerRequest request, CancellationToken cancellationToken)
        {
            GetListCustomerResponse response = await _customerService.GetListCustomerAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer to update.</param>
        /// <param name="request">The customer data transfer object containing updated details.</param>
        /// <returns>The updated customer response.</returns>
        [HttpPatch("{customerId}")]
        public async Task<IActionResult> Put(Guid customerId, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            UpdateCustomerResponse response = await _customerService.UpdateCustomerAsync(customerId, request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a customer by its ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to delete.</param>
        /// <returns>A no-content response if successful; otherwise, a bad request response.</returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(Guid customerId, CancellationToken cancellationToken)
        {
            bool response = await _customerService.DeleteCustomerAsync(customerId, cancellationToken);
            if (response == true)
            {
                return NoContent();
            }

            return BadRequest("Something went wrong with the request");
        }
    }
} 
