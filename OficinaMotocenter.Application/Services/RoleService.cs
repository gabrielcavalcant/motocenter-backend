using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository,
                                 ILogger<RoleService> logger,
                                 IUnitOfWork unitOfWork)
                                 : base(roleRepository, unitOfWork, logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> getByName(string name, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetByNameAsync(name, cancellationToken);
        }
    }
}

