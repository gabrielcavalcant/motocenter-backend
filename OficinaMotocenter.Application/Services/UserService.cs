using Microsoft.Extensions.Logging;
using OficinaMotocenter.Application.Dto.Requests.User;
using OficinaMotocenter.Application.Dto.Responses.User;
using OficinaMotocenter.Application.Exceptions;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;
using OficinaMotocenter.Domain.Interfaces.Repositories;
using OficinaMotocenter.Domain.Interfaces.UnitOfWork;
using AutoMapper;

namespace OficinaMotocenter.Application.Services
{
    /// <summary>
    /// Service for managing users, including operations for creating, updating, retrieving, and deleting users.
    /// </summary>
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           IUnitOfWork unitOfWork,
                           ILogger<UserService> logger,
                           IMapper mapper) : base(userRepository, unitOfWork, logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new user based on the provided request details.
        /// </summary>
        /// <param name="request">Details of the user to create.</param>
        /// <returns>A <see cref="UserDtoResponse"/> containing details of the newly created user.</returns>
        public async Task<UserDtoResponse> CreateUserAsync(CreateUserRequest request)
        {
            User newUser = _mapper.Map<User>(request);
            User createdUser = await CreateAsync(newUser);
            return _mapper.Map<UserDtoResponse>(createdUser);
        }

        /// <summary>
        /// Updates an existing user based on the specified ID and update details.
        /// </summary>
        /// <param name="id">Unique identifier of the user to update.</param>
        /// <param name="request">Updated user details.</param>
        /// <returns>A <see cref="UserDtoResponse"/> containing updated user information.</returns>
        public async Task<UserDtoResponse> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            User userToUpdate = await GetByIdAsync(id);

            if (userToUpdate == null)
            {
                throw new InvalidArgumentException("User not found");
            }

            _mapper.Map(request, userToUpdate);
            await UpdateAsync(userToUpdate);
            return _mapper.Map<UserDtoResponse>(userToUpdate);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>A <see cref="UserDtoResponse"/> with user details or null if not found.</returns>
        public async Task<UserDtoResponse> GetUserByIdAsync(Guid userId)
        {
            User user = await GetByIdAsync(userId);
            return _mapper.Map<UserDtoResponse>(user);
        }

        /// <summary>
        /// Retrieves a list of users based on filter criteria and pagination settings.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListUserResponse"/> containing the filtered users and total count.</returns>
        public async Task<GetListUserResponse> GetListUserAsync(GetListUserRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get User list using: {@request}", request);

            IList<User> userList = await GetAllAsync(
                cancellationToken,
                filter: m => (string.IsNullOrEmpty(request.FullName) || m.FullName.Contains(request.FullName)),
                skip: (request.PageIndex - 1) * request.PageSize,
                take: request.PageSize
            );

            GetListUserResponse response = _mapper.Map<GetListUserResponse>(userList);
            response.TotalCount = userList.Count;
            return response;
        }

        /// <summary>
        /// Executes the soft delete of an existing user.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to delete.</param>
        /// <returns>A boolean result indicating the success of the deletion.</returns>
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            bool userDeleted = await base.DeleteAsync(userId);
            return userDeleted;
        }
    }
}
