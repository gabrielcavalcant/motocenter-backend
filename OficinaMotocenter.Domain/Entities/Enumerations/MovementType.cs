namespace OficinaMotocenter.Domain.Entities.Enumerations
{
    /// <summary>
    /// Enum representing the type of stock movement.
    /// </summary>
    public enum MovementType : short
    {
        /// <summary>
        /// Represents the entry of stock into the inventory.
        /// </summary>
        StockIn = 1,

        /// <summary>
        /// Represents the exit of stock from the inventory.
        /// </summary>
        StockOut = 2
    }
}
