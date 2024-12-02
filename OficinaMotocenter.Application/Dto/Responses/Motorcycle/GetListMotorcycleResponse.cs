namespace OficinaMotocenter.Application.Dto.Responses.Motorcycle
{
    /// <summary>
    /// DTO for returning a paginated list of motorcycles. It includes a list of motorcycles,
    /// the current page index, and the total count of motorcycles.
    /// </summary>
    public class GetListMotorcycleResponse : GenericListResponse
    {
        /// <summary>
        /// A list of motorcycles for the current page.
        /// </summary>
        public List<MotorcycleDtoResponse> Motorcycles { get; set; }
    }

    /// <summary>
    /// DTO that represents a single motorcycle in the list.
    /// </summary>
    public class MotorcycleDto
    {
        /// <summary>
        /// The unique identifier of the motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// The name of the motorcycle.
        /// </summary>
        public string Name { get; set; }
    }
}
