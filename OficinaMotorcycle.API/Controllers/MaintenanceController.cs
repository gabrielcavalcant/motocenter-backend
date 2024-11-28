using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Maintenance;
using OficinaMotocenter.Application.Dto.Responses.Maintenance;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller responsible for handling maintenance management operations, including CRUD actions.
    /// </summary>
    [ApiController]
    [Route("api/maintenance")]
    [Authorize]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceController"/> class.
        /// </summary>
        /// <param name="maintenanceService">Service to handle maintenance operations.</param>
        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        /// <summary>
        /// Retrieves a maintenance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the maintenance record.</param>
        /// <returns>An <see cref="IActionResult"/> containing the maintenance details or NotFound if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaintenanceById(Guid id)
        {
            MaintenanceDtoResponse response = await _maintenanceService.GetMaintenanceByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a list of maintenance records based on the specified filter criteria.
        /// </summary>
        /// <param name="request">The filter criteria for retrieving maintenance records.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of maintenance records or NotFound if no records match.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListMaintenanceRequest request, CancellationToken cancellationToken)
        {
            GetListMaintenanceResponse response = await _maintenanceService.GetListMaintenanceAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Creates a new maintenance record with the specified details.
        /// </summary>
        /// <param name="request">The details of the maintenance to create.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created maintenance record and its location URI.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateMaintenance([FromBody] CreateMaintenanceRequest request)
        {
            if (request == null)
            {
                return BadRequest("Maintenance cannot be null.");
            }

            MaintenanceDtoResponse response = await _maintenanceService.CreateMaintenanceAsync(request);

            return CreatedAtAction(nameof(GetMaintenanceById), new { id = response.MaintenanceId }, response);
        }

        /// <summary>
        /// Updates an existing maintenance record with the specified identifier and details.
        /// </summary>
        /// <param name="id">The unique identifier of the maintenance record to update.</param>
        /// <param name="request">The updated maintenance details.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated maintenance record.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenance(Guid id, [FromBody] UpdateMaintenanceRequest request)
        {
            if (request == null)
            {
                return BadRequest("Maintenance cannot be null.");
            }

            MaintenanceDtoResponse response = await _maintenanceService.UpdateMaintenanceAsync(id, request);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a maintenance record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the maintenance record to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the success or failure of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(Guid id)
        {
            bool success = await _maintenanceService.DeleteMaintenanceAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
