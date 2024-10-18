using OficinaMotocenter.Domain.Entities.Enumerations;

namespace OficinaMotocenter.Application.Dto.Requests.Motorcycle
{
    /// <summary>
    /// DTO for creating a new motorcycle. Contains the necessary data for registering a motorcycle.
    /// </summary>
    public class CreateMotorcycleRequest
    {
        /// <summary>
        /// The name of the motorcycle.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of motorcycle (e.g., Cruiser, Sport, etc.).
        /// </summary>
        public MotorcycleType Type { get; set; }

        /// <summary>
        /// The license plate of the motorcycle.
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// The year the motorcycle was manufactured.
        /// </summary>
        public int YearManufacture { get; set; }

        /// <summary>
        /// The customer owner C    pf.
        /// </summary>
        public string CustomerCpf { get; set; }
    }
}
