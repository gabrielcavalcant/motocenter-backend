namespace OficinaMotocenter.Domain.Entities.Stock
{
    /// <summary>
    /// Represents a category for items in the inventory.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the category.
        /// </summary>
        public string Description { get; set; }
    }
}
