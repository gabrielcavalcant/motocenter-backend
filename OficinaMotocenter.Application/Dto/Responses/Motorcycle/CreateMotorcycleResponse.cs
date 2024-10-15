using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Responses.Motorcycle
{
    /// <summary>
    /// DTO for the response after creating a new motorcycle. Contains the generated ID
    /// and the details of the created motorcycle.
    /// </summary>
    public class CreateMotorcycleResponse
    {
        /// <summary>
        /// The unique identifier of the created motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// The name of the motorcycle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the motorcycle (e.g., Cruiser, Sport, etc.).
        /// </summary>
        public MotorcycleType Type { get; set; }

        /// <summary>
        /// The license plate of the motorcycle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// The year the motorcycle was manufactured.
        /// </summary>
        public string YearManufacture { get; set; }
    }
}
