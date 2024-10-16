using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Defines a service interface for performing CRUD operations and business logic related to motorcycles.
    /// Inherits from the generic service interface.
    /// </summary>
    public interface IMotorcycleService : IGenericService<Motorcycle>
    {
        /// <summary>
        /// Executes the creation of a new motorcycle.
        /// </summary>
        /// <param name="dto">The request DTO containing motorcycle information.</param>
        /// <returns>A response DTO with the details of the created motorcycle.</returns>
        Task<CreateMotorcycleResponse> CreateMotorcycleAsync(CreateMotorcycleRequest dto);

        /// <summary>
        /// Executes the retrieval of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to retrieve.</param>
        /// <returns>A response DTO with the details of the motorcycle.</returns>
        Task<GetMotorcycleByIdResponse> GetMotorcycleByIdAsync(Guid motorcycleId);

        /// <summary>
        /// Executes the retrieval of all motorcycles with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <returns>A response DTO with the list of motorcycles and pagination details.</returns>
        Task<GetListMotorcycleResponse> GetListMotorcycleAsync(GetListMotorcycleRequest request);

        /// <summary>
        /// Executes the update of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated motorcycle.</returns>
        Task<UpdateMotorcycleResponse> UpdateMotorcycleAsync(Guid motorcycleId, UpdateMotorcycleRequest request);

        /// <summary>
        /// Executes the deletion of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteMotorcycleAsync(Guid motorcycleId);

    }
}
