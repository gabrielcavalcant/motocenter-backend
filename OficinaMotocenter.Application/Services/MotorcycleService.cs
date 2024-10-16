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

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorcycleService"/> class.
        /// </summary>
        /// <param name="motorcycleRepository">The repository for motorcycle operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public MotorcycleService(IMotorcycleRepository motorcycleRepository, ILogger<MotorcycleService> logger, IMapper mapper)
            : base(motorcycleRepository, logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateMotorcycleResponse> CreateMotorcycleAsync(CreateMotorcycleRequest dto)
        {
            Motorcycle motorcycle = _mapper.Map<Motorcycle>(dto);
            _logger.LogInformation("Creating motorcycle: {@motorcycle}", motorcycle);
            Motorcycle createdMotorcycle = await base.CreateAsync(motorcycle);
            return _mapper.Map<CreateMotorcycleResponse>(createdMotorcycle);
        }

        public async Task<GetMotorcycleByIdResponse> GetMotorcycleByIdAsync(Guid id)
        {
            Motorcycle motorcycle = await base.GetByIdAsync(id);
            return _mapper.Map<GetMotorcycleByIdResponse>(motorcycle);
        }
    }
}
