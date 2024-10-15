using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;


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

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorcycleService"/> class.
        /// </summary>
        /// <param name="motorcycleRepository">The repository for motorcycle operations.</param>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public MotorcycleService(IMotorcycleRepository motorcycleRepository, ILogger<MotorcycleService> logger)
            : base(motorcycleRepository, logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _logger = logger;
        }
    }
}
