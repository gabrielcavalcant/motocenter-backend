namespace OficinaMotocenter.Application.Dto.Requests.Item
{
    /// <summary>
    /// DTO for retrieving a paginated list of itens with optional filters.
    /// </summary>
    public class GetListItemRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional filter by the supplier of the item.
        /// </summary>
        public string? Supplier { get; set; }
    }
}