namespace OficinaMotocenter.Domain.Entities.Stock
{
    /// <summary>
    /// Represents an inventory item with details such as name, description, quantity, and associated category.
    /// </summary>
    public class Item
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
        /// Description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Quantity of the item in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Date the item was added to the inventory.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Foreign key identifier for the associated category, if any.
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Navigation property for the associated category.
        /// </summary>
        public Category Category { get; set; }
    }
}
