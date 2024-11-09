using AutoMapper;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Item;
using OficinaMotocenter.Application.Dto.Responses.Item;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities.Stock;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service implementation for item-specific operations.
    /// Inherits basic CRUD operations from the generic service.
    /// </summary>
    public class ItemService : GenericService<Item>, IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemService"/> class.
        /// </summary>
        /// <param name="itemRepository">The repository for item operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="mapper">The auto mapper instance for mapping object operations.</param>
        /// <param name="unitOfWork">The unit of work for item operations.</param>
        public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger, IMapper mapper, IUnitOfWork unitOfWork)
            : base(itemRepository, unitOfWork, logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executes the creation of a new item.
        /// </summary>
        /// <param name="request">The request DTO containing item information.</param>
        /// <returns>A response DTO with the details of the created item.</returns>
        public async Task<ItemDtoResponse> CreateItemAsync(CreateItemRequest request)
        {
            Item item = _mapper.Map<Item>(request);
            _logger.LogInformation("Creating item: {@item}", item);
            Item createdItem = await base.CreateAsync(item);
            return _mapper.Map<ItemDtoResponse>(createdItem);
        }

        /// <summary>
        /// Executes the retrieval of an item by its ID.
        /// </summary>
        /// <param name="itemId">The unique ID of the item to retrieve.</param>
        /// <returns>A response DTO with the details of the item.</returns>
        public async Task<ItemDtoResponse> GetItemByIdAsync(Guid itemId)
        {
            _logger.LogInformation("Searching item using GUID: {@id}", itemId);
            Item item = await base.GetByIdAsync(itemId);
            return _mapper.Map<ItemDtoResponse>(item);
        }

        /// <summary>
        /// Executes the retrieval of all items with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of items and pagination details.</returns>
        public async Task<GetListItemResponse> GetListItemAsync(GetListItemRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get item list using: {@request}", request);

            IList<Item> itemList = await base.GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)) &&
                             (string.IsNullOrEmpty(request.Supplier) || m.Supplier.Contains(request.Supplier)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );
            GetListItemResponse response = _mapper.Map<GetListItemResponse>(itemList);
            response.TotalCount = itemList.Count;
            return response;
        }

        /// <summary>
        /// Executes the update of an existing item.
        /// </summary>
        /// <param name="itemId">Item ID</param>
        /// <param name="request">The request DTO containing item information.</param>
        /// <returns>A response DTO with the details of the updated item.</returns>
        public async Task<ItemDtoResponse> UpdateItemAsync(Guid itemId, UpdateItemRequest request)
        {
            Item item = await base.GetByIdAsync(itemId);
            _mapper.Map(request, item);
            Item updatedItem = await base.UpdateAsync(item);
            updatedItem = await base.GetByIdAsync(updatedItem.ItemId);
            ItemDtoResponse response = _mapper.Map<ItemDtoResponse>(updatedItem);
            return response;
        }

        /// <summary>
        /// Executes the soft delete of an existing item.
        /// </summary>
        /// <param name="itemId">Item ID</param>
        /// <returns>A boolean result</returns>
        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            bool itemDeleted = await base.DeleteAsync(itemId);
            return itemDeleted;
        }
    }
}
