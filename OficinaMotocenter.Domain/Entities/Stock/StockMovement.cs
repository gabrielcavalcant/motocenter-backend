using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Domain.Entities.Stock
{
    /// <summary>
    /// Represents a movement in the inventory, such as adding or removing items.
    /// </summary>
    public class StockMovement
    {
        /// <summary>
        /// Unique identifier for the stock movement.
        /// </summary>
        public int StockMovementId { get; set; }

        /// <summary>
        /// Foreign key identifier for the associated item.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Navigation property for the associated item.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Type of the movement, indicating whether it is an entry or exit of stock.
        /// </summary>
        public MovementType MovementType { get; set; }

        /// <summary>
        /// Quantity of the item involved in the movement.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Date and time when the movement was made.
        /// </summary>
        public DateTime MovementDate { get; set; }

        /// <summary>
        /// Additional notes about the movement, if any.
        /// </summary>
        public string Notes { get; set; }
    }
}
