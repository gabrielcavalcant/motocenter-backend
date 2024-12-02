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
        /// <param name="request">The request DTO containing motorcycle information.</param>
        /// <returns>A response DTO with the details of the created motorcycle.</returns>
        Task<MotorcycleDtoResponse> CreateMotorcycleAsync(CreateMotorcycleRequest request);

        /// <summary>
        /// Executes the retrieval of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to retrieve.</param>
        /// <returns>A response DTO with the details of the motorcycle.</returns>
        Task<MotorcycleDtoResponse> GetMotorcycleByIdAsync(Guid motorcycleId);

        /// <summary>
        /// Executes the retrieval of all motorcycles with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of motorcycles and pagination details.</returns>
        Task<GetListMotorcycleResponse> GetListMotorcycleAsync(GetListMotorcycleRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the update of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to update.</param>
        /// <param name="request">The request DTO containing the updated information.</param>
        /// <returns>A response DTO with the details of the updated motorcycle.</returns>
        Task<MotorcycleDtoResponse> UpdateMotorcycleAsync(Guid motorcycleId, UpdateMotorcycleRequest request);

        /// <summary>
        /// Executes the deletion of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteMotorcycleAsync(Guid motorcycleId );

    }
}
