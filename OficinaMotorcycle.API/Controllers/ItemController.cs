using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.Item;
using OficinaMotocenter.Application.Dto.Responses.Item;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller for managing item-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;

        /// <summary>
        /// Constructor for dependency injection.
        /// </summary>
        /// <param name="itemService">Service to manage item operations.</param>
        /// <param name="logger">Logger for logging information.</param>
        public ItemController(
            IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new item.
        /// </summary>
        /// <param name="request">The item data transfer object containing item details.</param>
        /// <returns>A created item response along with a location header.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemRequest request)
        {
            _logger.LogInformation("Request initiated");

            ItemDtoResponse response = await _itemService.CreateItemAsync(request);

            _logger.LogInformation("Response: {@response}", response);
            return CreatedAtAction(nameof(Get), new { itemId = response.ItemId }, response);
        }

        /// <summary>
        /// Retrieves an item by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to retrieve.</param>
        /// <returns>The item details if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet("{itemId}")]
        public async Task<IActionResult> Get(Guid itemId)
        {
            ItemDtoResponse response = await _itemService.GetItemByIdAsync(itemId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves an item list.
        /// </summary>
        /// <param name="request">The request object for retrieving an item list.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A list of items.</returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListItemRequest request, CancellationToken cancellationToken)
        {
            GetListItemResponse response = await _itemService.GetListItemAsync(request, cancellationToken);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing item.
        /// </summary>
        /// <param name="itemId">The ID of the item to update.</param>
        /// <param name="request">The item data transfer object containing updated details.</param>
        /// <returns>The updated item response.</returns>
        [HttpPatch("{itemId}")]
        public async Task<IActionResult> Put(Guid itemId, [FromBody] UpdateItemRequest request)
        {
            ItemDtoResponse response = await _itemService.UpdateItemAsync(itemId, request);
            return Ok(response);
        }

        /// <summary>
        /// Deletes an item by its ID.
        /// </summary>
        /// <param name="itemId">The ID of the item to delete.</param>
        /// <returns>A no-content response if successful; otherwise, a bad request response.</returns>
        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid itemId)
        {
            bool response = await _itemService.DeleteItemAsync(itemId);
            if (response == true)
            {
                return NoContent();
            }

            return BadRequest("Something went wrong with the request");
        }
    }
}
