using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Domain.Entities.Stock;
using OficinaMotocenter.Application.Dto.Requests.Inventory;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service implementation for inventory-specific operations.
    /// Inherits basic CRUD operations from the generic service.
    /// </summary>
    public class InventoryService : IInventoryService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<InventoryService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        /// <param name="itemRepository">The repository for item operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="unitOfWork">The unit of work for inventory operations.</param>
        public InventoryService(IItemRepository itemRepository,
                                 ILogger<InventoryService> logger,
                                 IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Updates the quantity of a specific item in stock.
        /// </summary>
        /// <param name="request">The request DTO containing item information and quantity change.</param>
        /// <returns>A boolean indicating success or failure of the operation.</returns>
        public async Task<bool> UpdateStockQuantityAsync(UpdateStockRequest request)
        {
            // Retrieve the item by ID
            Item item = await _itemRepository.GetByIdAsync(request.ItemId);
            if (item == null)
            {
                _logger.LogWarning("Item not found with ID: {ItemId}", request.ItemId);
                throw new InvalidArgumentException("Item not found");
            }

            // Update the stock quantity
            item.StockQuantity += request.QuantityChange;

            // Validate that the new quantity is not negative
            if (item.StockQuantity < 0)
            {
                _logger.LogWarning("Insufficient stock for item ID: {ItemId}", request.ItemId);
                throw new InvalidArgumentException("Insufficient stock");
            }

            // Persist the changes
            await _itemRepository.UpdateAsync(item);
            await _unitOfWork.Commit();

            _logger.LogInformation("Stock quantity updated successfully for item ID: {ItemId}", request.ItemId);
            return true;
        }
    }
}
