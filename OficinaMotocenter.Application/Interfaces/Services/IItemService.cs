using OficinaMotocenter.Application.Dto.Requests.Item;
using OficinaMotocenter.Application.Dto.Responses.Item;
using OficinaMotocenter.Domain.Entities.Stock;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to items.
    /// Inherits from the generic service interface and adds additional methods.
    /// </summary>
    public interface IItemService : IGenericService<Item>
    {
        /// <summary>
        /// Executes the creation of a new item.
        /// </summary>
        /// <param name="request">The request DTO containing item information.</param>
        /// <returns>A response DTO with the details of the created item.</returns>
        Task<ItemDtoResponse> CreateItemAsync(CreateItemRequest request);

        /// <summary>
        /// Executes the retrieval of an item by its ID.
        /// </summary>
        /// <param name="itemId">The unique ID of the item to retrieve.</param>
        /// <returns>A response DTO with the details of the item.</returns>
        Task<ItemDtoResponse> GetItemByIdAsync(Guid itemId);

        /// <summary>
        /// Executes the retrieval of all items with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of items and pagination details.</returns>
        Task<GetListItemResponse> GetListItemAsync(GetListItemRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the update of an item by its ID.
        /// </summary>
        /// <param name="itemId">The unique ID of the item to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated item.</returns>
        Task<ItemDtoResponse> UpdateItemAsync(Guid itemId, UpdateItemRequest request);

        /// <summary>
        /// Executes the deletion of an item by its ID.
        /// </summary>
        /// <param name="itemId">The unique ID of the item to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteItemAsync(Guid itemId);
    }
}
