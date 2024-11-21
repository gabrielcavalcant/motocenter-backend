namespace OficinaMotocenter.Domain.Entities.Stock
{
    /// <summary>
    /// Represents an inventory item
    /// </summary>
    public class Item : BaseEntity
    {
        /// <summary>
        /// Unique identifier for the item.
        /// </summary>
        public Guid ItemId { get; set; }

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

        ///// <summary>
        ///// Date the item was added to the inventory.
        ///// </summary>
        //public DateTime DateAdded { get; set; }

        ///// <summary>
        ///// Foreign key identifier for the associated category, if any.
        ///// </summary>
        //public Guid? CategoryId { get; set; }

        ///// <summary>
        ///// Navigation property for the associated category.
        ///// </summary>
        //public Category Category { get; set; }
    }
}
