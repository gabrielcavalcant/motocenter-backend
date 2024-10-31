namespace OficinaMotocenter.Application.Dto.Requests
{
    /// <summary>
    /// Generic DTO for retrieving a paginated list.
    /// </summary>
    public class GenericListRequest
    {

        /// <summary>
        /// The page number to retrieve, defaulting to 1.
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The number of items to retrieve per page, defaulting to 10.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
