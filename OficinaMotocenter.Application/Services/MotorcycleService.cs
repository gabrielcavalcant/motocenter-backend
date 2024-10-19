using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using AutoMapper;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using OficinaMotocenter.Application.Exceptions;


namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service implementation for motorcycle-specific operations.
    /// Inherits basic CRUD operations from the generic service.
    /// </summary>
    public class MotorcycleService : GenericService<Motorcycle>, IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ILogger<MotorcycleService> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorcycleService"/> class.
        /// </summary>
        /// <param name="motorcycleRepository">The repository for motorcycle operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="mapper">The auto mapper instance for mapping object operations.</param>
        /// <param name="unitOfWork">The unit of work for motorcycle operations.</param>


        public MotorcycleService(IMotorcycleRepository motorcycleRepository, 
                                 ILogger<MotorcycleService> logger, 
                                 IMapper mapper, 
                                 ICustomerService customerService,
                                 IUnitOfWork unitOfWork)
                                 : base(motorcycleRepository,unitOfWork, logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executes the creation of a new motorcycle.
        /// </summary>
        /// <param name="request">The request DTO containing motorcycle information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the details of the created motorcycle.</returns>
        public async Task<CreateMotorcycleResponse> CreateMotorcycleAsync(CreateMotorcycleRequest request, CancellationToken cancellationToken)
        {
            Customer customer = await _customerService.GetCustomerByCpfAsync(request.CustomerCpf, cancellationToken);

            if (customer == null)
            {
                _logger.LogWarning("CPF not found", request.CustomerCpf);
                throw new InvalidArgumentException("Client not found");
            }

            Motorcycle motorcycle = _mapper.Map<Motorcycle>(request);
            motorcycle.CustomerId = customer.CustomerId;

            _logger.LogInformation("Creating motorcycle: {@motorcycle}", motorcycle);
            Motorcycle createdMotorcycle = await base.CreateAsync(motorcycle, cancellationToken);
            return _mapper.Map<CreateMotorcycleResponse>(createdMotorcycle);
        }

        /// <summary>
        /// Executes the retrieval of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to retrieve.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the details of the motorcycle.</returns>
        public async Task<GetMotorcycleByIdResponse> GetMotorcycleByIdAsync(Guid motorcycleId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Searching motorcycle using GUID: {@id}", motorcycleId);
            Motorcycle motorcycle = await base.GetByIdAsync(motorcycleId, cancellationToken);
            return _mapper.Map<GetMotorcycleByIdResponse>(motorcycle);
        }

        /// <summary>
        /// Executes the retrieval of all motorcycles with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the list of motorcycles and pagination details.</returns>
        public async Task<GetListMotorcycleResponse> GetListMotorcycleAsync(GetListMotorcycleRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get motorcycle list using: {@request}", request);

            IList<Motorcycle> motorcycleList = await base.GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)) &&
                             (string.IsNullOrEmpty(request.Plate) || m.Plate.Contains(request.Plate)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );

            GetListMotorcycleResponse response = _mapper.Map<GetListMotorcycleResponse>(motorcycleList);
            response.TotalCount = motorcycleList.Count;
            return response;
        }

        /// <summary>
        /// Executes the update of a existent customer.
        /// </summary>
        /// <param name="motorcycleId">Motorcycle Id</param>
        /// <param name="request">The request DTO containing motorcycle information.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A response DTO with the details of the updated motorcycle.</returns>
        public async Task<UpdateMotorcycleResponse> UpdateMotorcycleAsync(Guid motorcycleId, UpdateMotorcycleRequest request, CancellationToken cancellationToken)
        {
            Motorcycle motorcycle = await base.GetByIdAsync(motorcycleId, cancellationToken);
            _mapper.Map(request, motorcycle);
            Motorcycle updatedMotorcycle = await base.UpdateAsync(motorcycle, cancellationToken);
            UpdateMotorcycleResponse response = _mapper.Map<UpdateMotorcycleResponse>(updatedMotorcycle);
            return response;
        }

        /// <summary>
        /// Executes the soft delete of a existent motorcycle.
        /// </summary>
        /// <param name="motorcycleId">motorcycle Id</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>A boolean result</returns>
        public async Task<bool> DeleteMotorcycleAsync(Guid motorcycleId, CancellationToken cancellationToken)
        {
            bool motorcycleDeleted = await base.DeleteAsync(motorcycleId, cancellationToken);
            return motorcycleDeleted;
        }
    }
}
