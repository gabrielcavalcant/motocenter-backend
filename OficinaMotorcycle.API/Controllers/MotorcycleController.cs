using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller for managing motorcycle-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly IMotorcycleService _motorcycleService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="motorcycleService">Service to manage motorcycle operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public MotorcycleController(

            IMotorcycleService motorcycleService, ILogger<MotorcycleController> logger)
        {
            _motorcycleService = motorcycleService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new motorcycle.
        /// </summary>
        /// <param name="request">The motorcycle data transfer object containing motorcycle details.</param>
        /// <returns>A created motorcycle response along with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMotorcycleRequest request)
        {
            try
            {
                _logger.LogInformation("Request initiated");

                CreateMotorcycleResponse response = await _motorcycleService.CreateMotorcycleAsync(request);

                _logger.LogInformation("Response: {@response}", response);
                return CreatedAtAction(nameof(Get), new { id = response.MotorcycleId }, response);
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument provided: {Message}", ex.Message);
                return NotFound(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Request was cancelled.");
                return StatusCode(StatusCodes.Status408RequestTimeout, new { Message = "Operation was cancelled." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred." });
            }


        }

        /// <summary>
        /// Retrieves a motorcycle by its ID.
        /// </summary>
        /// <param name="id">The ID of the motorcycle to retrieve.</param>
        /// <returns>The motorcycle details if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            GetMotorcycleByIdResponse response = await _motorcycleService.GetMotorcycleByIdAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all motorcycles.
        /// </summary>
        /// <param name="request">The request object for retrieving all motorcycles.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of motorcycles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListMotorcycleRequest request, CancellationToken cancellationToken)
        {
            GetListMotorcycleResponse response = await _motorcycleService.GetListMotorcycleAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing motorcycle.
        /// </summary>
        /// <param name="motorcycleId">The ID of the motorcycle to update.</param>
        /// <param name="request">The motorcycle data transfer object containing updated details.</param>
        /// <returns>The updated motorcycle response.</returns>
        [HttpPut("{motorcycleId}")]
        public async Task<IActionResult> Put(Guid motorcycleId, [FromBody] UpdateMotorcycleRequest request )
        {
            UpdateMotorcycleResponse response = await _motorcycleService.UpdateMotorcycleAsync(motorcycleId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The ID of the motorcycle to delete.</param>
        /// <returns>A no-content response if successful; otherwise, a bad request response.</returns>
        [HttpDelete("{motorcycleId}")]
        public async Task<IActionResult> Delete(Guid motorcycleId)
        {
            bool response = await _motorcycleService.DeleteMotorcycleAsync(motorcycleId);
            if (response == true)
            {
                return NoContent();
            }

            return BadRequest("Something went wrong with the request");
        }
    }
}
