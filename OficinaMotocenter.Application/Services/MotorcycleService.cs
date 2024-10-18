using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.Motorcycle;
using OficinaMotocenter.Application.Dto.Responses.Motorcycle;
using AutoMapper;


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

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorcycleService"/> class.
        /// </summary>
        /// <param name="motorcycleRepository">The repository for motorcycle operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        /// <param name="mapper">The auto mapper instance for mapping object operations.</param>

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, 
                                 ILogger<MotorcycleService> logger, 
                                 IMapper mapper, 
                                 ICustomerService customerService)
                                 : base(motorcycleRepository, logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
            _customerService = customerService;
        }

        /// <summary>
        /// Executes the creation of a new motorcycle.
        /// </summary>
        /// <param name="dto">The request DTO containing motorcycle information.</param>
        /// <returns>A response DTO with the details of the created motorcycle.</returns>
        public async Task<CreateMotorcycleResponse> CreateMotorcycleAsync(CreateMotorcycleRequest request)
        {
            Customer customer = await _customerService.GetCustomerByCpfAsync(request.CustomerCpf);

            if (customer == null)
            {
                _logger.LogWarning("CPF not found", request.CustomerCpf);
                throw new Exception("Client not found");
            }

            Motorcycle motorcycle = _mapper.Map<Motorcycle>(request);
            motorcycle.CustomerId = customer.CustomerId;

            _logger.LogInformation("Creating motorcycle: {@motorcycle}", motorcycle);
            Motorcycle createdMotorcycle = await base.CreateAsync(motorcycle);
            return _mapper.Map<CreateMotorcycleResponse>(createdMotorcycle);
        }

        /// <summary>
        /// Executes the retrieval of a motorcycle by its ID.
        /// </summary>
        /// <param name="motorcycleId">The unique ID of the motorcycle to retrieve.</param>
        /// <returns>A response DTO with the details of the motorcycle.</returns>
        public async Task<GetMotorcycleByIdResponse> GetMotorcycleByIdAsync(Guid motorcycleId)
        {
            _logger.LogInformation("Searching motorcycle using GUID: {@id}", motorcycleId);
            Motorcycle motorcycle = await base.GetByIdAsync(motorcycleId);
            return _mapper.Map<GetMotorcycleByIdResponse>(motorcycle);
        }

        /// <summary>
        /// Executes the retrieval of all motorcycles with optional filtering.
        /// </summary>
        /// <param name="request">The request DTO containing filtering information.</param>
        /// <returns>A response DTO with the list of motorcycles and pagination details.</returns>
        public async Task<GetListMotorcycleResponse> GetListMotorcycleAsync(GetListMotorcycleRequest request)
        {
            _logger.LogInformation("Get motorcycle list using: {@request}", request);

            IList<Motorcycle> motorcycleList = await base.GetAllAsync(
                filter: m => (string.IsNullOrEmpty(request.Name) || m.Name.Contains(request.Name)) &&
                             (string.IsNullOrEmpty(request.Plate) || m.Plate.Contains(request.Plate)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );
            GetListMotorcycleResponse response = _mapper.Map<GetListMotorcycleResponse>(motorcycleList);
            response.TotalCount = motorcycleList.Count;
            return response;
        }

        public async Task<UpdateMotorcycleResponse> UpdateMotorcycleAsync(Guid motorcycleId, UpdateMotorcycleRequest request)
        {
            Motorcycle motorcycle = await base.GetByIdAsync(motorcycleId);
            _mapper.Map(request, motorcycle);
            Motorcycle updatedMotorcycle = await base.UpdateAsync(motorcycle);
            UpdateMotorcycleResponse response = _mapper.Map<UpdateMotorcycleResponse>(updatedMotorcycle);
            return response;
        }

        public async Task<bool> DeleteMotorcycleAsync(Guid motorcycleId)
        {
            bool motorcycleDeleted = await base.DeleteAsync(motorcycleId);
            return motorcycleDeleted;
        }
    }
}
