namespace OficinaMotocenter.Application.Dto.Responses.Item
{
    /// <summary>
    /// DTO for returning a paginated list of itens. It includes a list of itens,
    /// the current page index, and the total count of itens.
    /// </summary>
    public class GetListItemResponse : GenericListResponse
    {
        /// <summary>
        /// A list of itens for the current page.
        /// </summary>
        public List<ItemDtoResponse> Items { get; set; }
    }
}
