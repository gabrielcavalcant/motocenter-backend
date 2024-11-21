using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Inventory;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller for managing inventory operations related to stock items.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="inventoryService">Service to manage inventory operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public InventoryController(
            IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        /// <summary>
        /// Updates the stock quantity of an item.
        /// </summary>
        /// <param name="request">The request DTO containing item details and quantity change.</param>
        /// <returns>An appropriate response indicating success or failure of the operation.</returns>
        [HttpPost("UpdateStockQuantity")]
        public async Task<IActionResult> UpdateStockQuantity([FromBody] UpdateStockRequest request)
        {
            try
            {
                _logger.LogInformation("Starting stock quantity update for item: {ItemId}", request.ItemId);

                bool success = await _inventoryService.UpdateStockQuantityAsync(request);

                if (success)
                {
                    _logger.LogInformation("Stock quantity successfully updated for item: {ItemId}", request.ItemId);
                    return Ok(new { Message = "Stock quantity updated successfully." });
                }
                else
                {
                    _logger.LogWarning("Failed to update stock quantity for item: {ItemId}", request.ItemId);
                    return BadRequest(new { Message = "Failed to update stock quantity." });
                }
            }
            catch (InvalidArgumentException ex)
            {
                _logger.LogError(ex, "Invalid argument provided: {Message}", ex.Message);
                return BadRequest(new { Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}
