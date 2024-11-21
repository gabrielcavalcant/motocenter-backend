namespace OficinaMotocenter.Application.Dto.Requests.Item
{
    public class UpdateItemRequest
    {
        /// <summary>
        /// Identifier of the item.
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
    }
}
