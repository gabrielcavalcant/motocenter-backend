using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Responses.Motorcycle
{
    /// <summary>
    /// DTO for the response after updating a motorcycle. Contains the updated details
    /// of the motorcycle.
    /// </summary>
    public class UpdateMotorcycleResponse
    {
        /// <summary>
        /// The unique identifier of the updated motorcycle.
        /// </summary>
        public Guid MotorcycleId { get; set; }

        /// <summary>
        /// The updated name of the motorcycle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The updated year of manufacture.
        /// </summary>
        public string YearManufacture { get; set; }

        /// <summary>
        /// The updated license plate of the motorcycle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// The updated type of the motorcycle (Scooter, Sport, etc.).
        /// </summary>
        public MotorcycleType Type { get; set; }
    }
}
