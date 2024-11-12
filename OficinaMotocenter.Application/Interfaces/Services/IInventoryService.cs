using OficinaMotocenter.Application.Dto.Requests.Inventory;
using OficinaMotocenter.Domain.Entities.Stock;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    public interface IInventoryService
    {
        /// <summary>
        /// Executes the add or remove operation on the stock.
        /// </summary>
        /// <param name="request">The request DTO containing item information and quantity change.</param>
        /// <returns>A bool value representing sucess or fail.</returns>
        Task<bool> UpdateStockQuantityAsync(UpdateStockRequest request);
    }
}