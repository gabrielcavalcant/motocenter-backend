using AutoMapper;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Maintenance;
using OficinaMotocenter.Application.Dto.Responses.Maintenance;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for handling maintenance-related business logic, including CRUD operations and retrieval by filters.
    /// </summary>
    public class MaintenanceService : GenericService<Maintenance>, IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceService"/> class.
        /// </summary>
        /// <param name="maintenanceRepository">Repository for maintenance data access.</param>
        /// <param name="logger">Logger for logging operations.</param>
        /// <param name="unitOfWork">Unit of work for transaction management.</param>
        /// <param name="mapper">Mapper for mapping between entities and DTOs.</param>
        public MaintenanceService(IMaintenanceRepository maintenanceRepository,
                                  ILogger<MaintenanceService> logger,
                                  IUnitOfWork unitOfWork,
                                  IMapper mapper)
                                  : base(maintenanceRepository, unitOfWork, logger)
        {
            _maintenanceRepository = maintenanceRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new maintenance record based on the provided request details.
        /// </summary>
        /// <param name="request">Details of the maintenance to create.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> containing details of the newly created maintenance.</returns>
        public async Task<MaintenanceDtoResponse> CreateMaintenanceAsync(CreateMaintenanceRequest request)
        {
            Maintenance newMaintenance = _mapper.Map<Maintenance>(request);
            Maintenance createdMaintenance = await CreateAsync(newMaintenance);
            return _mapper.Map<MaintenanceDtoResponse>(createdMaintenance);
        }

        /// <summary>
        /// Updates an existing maintenance record based on the specified ID and update details.
        /// </summary>
        /// <param name="id">Unique identifier of the maintenance to update.</param>
        /// <param name="request">Updated maintenance details.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> containing updated maintenance information.</returns>
        public async Task<MaintenanceDtoResponse> UpdateMaintenanceAsync(Guid id, UpdateMaintenanceRequest request)
        {
            Maintenance maintenanceToUpdate = await GetByIdAsync(id);

            if (maintenanceToUpdate == null)
            {
                throw new InvalidArgumentException("Maintenance record not found");
            }

            _mapper.Map(request, maintenanceToUpdate);
            await UpdateAsync(maintenanceToUpdate);
            return _mapper.Map<MaintenanceDtoResponse>(maintenanceToUpdate);
        }

        /// <summary>
        /// Retrieves a maintenance record by its unique identifier.
        /// </summary>
        /// <param name="maintenanceId">Unique identifier of the maintenance.</param>
        /// <returns>A <see cref="MaintenanceDtoResponse"/> with maintenance details or null if not found.</returns>
        public async Task<MaintenanceDtoResponse> GetMaintenanceByIdAsync(Guid maintenanceId)
        {
            Maintenance maintenance = await GetByIdAsync(maintenanceId);
            return _mapper.Map<MaintenanceDtoResponse>(maintenance);
        }

        /// <summary>
        /// Retrieves a list of maintenance records based on filter criteria and pagination settings.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListMaintenanceResponse"/> containing the filtered maintenance records and total count.</returns>
        public async Task<GetListMaintenanceResponse> GetListMaintenanceAsync(GetListMaintenanceRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get maintenance list using: {@request}", request);

            IList<Maintenance> maintenanceList = await GetAllAsync(
                cancellationToken,
                filter: m => (!request.MaintenanceStatus.HasValue || m.MaintenanceStatus == request.MaintenanceStatus),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );

            GetListMaintenanceResponse response = _mapper.Map<GetListMaintenanceResponse>(maintenanceList);
            response.TotalCount = maintenanceList.Count;
            return response;
        }

        /// <summary>
        /// Executes the soft delete of an existing maintenance record.
        /// </summary>
        /// <param name="maintenanceId">Unique identifier of the maintenance to delete.</param>
        /// <returns>A boolean result indicating the success of the deletion.</returns>
        public async Task<bool> DeleteMaintenanceAsync(Guid maintenanceId)
        {
            bool maintenanceDeleted = await base.DeleteAsync(maintenanceId);
            return maintenanceDeleted;
        }
    }
}
