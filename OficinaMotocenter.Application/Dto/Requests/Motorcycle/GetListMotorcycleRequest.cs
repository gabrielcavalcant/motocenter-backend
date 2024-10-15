namespace OficinaMotocenter.Application.Dto.Requests.Motorcycle
{
    /// <summary>
    /// DTO for retrieving a paginated list of motorcycles with optional filters.
    /// </summary>
    public class GetListMotorcycleRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional filter by the license plate of the motorcycle.
        /// </summary>
        public string? Plate { get; set; }

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
