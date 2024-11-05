namespace OficinaMotocenter.Application.Dto.Requests.Motorcycle
{
    /// <summary>
    /// DTO for retrieving a paginated list of motorcycles with optional filters.
    /// </summary>
    public class GetListMotorcycleRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Optional filter by the license plate of the motorcycle.
        /// </summary>
        public string? Plate { get; set; }
    }
}
