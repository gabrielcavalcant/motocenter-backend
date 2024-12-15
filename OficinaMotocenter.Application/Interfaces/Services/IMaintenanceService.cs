using OficinaMotocenter.Application.Dto.Requests.Maintenance;
using OficinaMotocenter.Application.Dto.Responses.Maintenance;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for maintenance-related services, providing methods for creating, updating, retrieving, and deleting maintenance records.
    /// </summary>
    public interface IMaintenanceService : IGenericService<Maintenance>
    {
        /// <summary>
        /// Retrieves a maintenance record by its unique identifier.
        /// </summary>
        /// <param name="maintenanceId">Unique identifier of the maintenance.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> with the maintenance details.</returns>
        Task<MaintenanceDtoResponse> GetMaintenanceByIdAsync(Guid maintenanceId);

        /// <summary>
        /// Retrieves a paginated list of maintenance records based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListMaintenanceResponse"/> with the list of maintenance records and total count.</returns>
        Task<GetListMaintenanceResponse> GetListMaintenanceAsync(GetListMaintenanceRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new maintenance record based on the provided details.
        /// </summary>
        /// <param name="request">Details of the maintenance to create.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> with the created maintenance information.</returns>
        Task<MaintenanceDtoResponse> CreateMaintenanceAsync(CreateMaintenanceRequest request);

        /// <summary>
        /// Updates an existing maintenance record with the specified identifier and details.
        /// </summary>
        /// <param name="id">Unique identifier of the maintenance to update.</param>
        /// <param name="request">Updated maintenance details.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> with the updated maintenance information.</returns>
        Task<MaintenanceDtoResponse> UpdateMaintenanceAsync(Guid id, UpdateMaintenanceRequest request);

        /// <summary>
        /// Executes a soft delete on a maintenance record based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the maintenance to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        Task<bool> DeleteMaintenanceAsync(Guid id);
    }
}
