namespace OficinaMotocenter.Application.Dto.Requests.Item
{
    /// <summary>
    /// DTO for the request to create a new item. Contains the necessary information
    /// to register a new item in the system.
    /// </summary>
    /// <summary>
    /// Represents an inventory item
    /// </summary>
    public class CreateItemRequest
    {
        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Serial Code of the item.
        /// </summary>
        public string SerialCode { get; set; }

        /// <summary>
        /// Supplier of the item.
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Quantity of the item in stock.
        /// </summary>
        public int StockQuantity { get; set; }
    }
}
