using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;

namespace OficinaMotocenter.Application.Services
{
    public class UserService : GenericService<User>, IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
                           IUnitOfWork unitOfWork,
                           ILogger<UserService> logger) : base(userRepository, unitOfWork, logger)
        {
            _userRepository = userRepository;
        }
    }
}
