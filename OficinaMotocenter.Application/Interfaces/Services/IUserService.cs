using OficinaMotocenter.Application.Dto.Requests.User;
using OficinaMotocenter.Application.Dto.Responses.User;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Application.Interfaces.Services
{
    /// <summary>
    /// Interface for user-related services, providing methods for creating, updating, retrieving, and deleting users.
    /// </summary>
    public interface IUserService : IGenericService<User>
    {
        /// <summary>
        /// Retrieves a user by its unique identifier.
        /// </summary>
        /// <param name="userId">Unique identifier of the user.</param>
        /// <returns>A <see cref="UserDtoResponse"/> with the user details.</returns>
        Task<UserDtoResponse> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// Retrieves a paginated list of users based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A <see cref="GetListUserResponse"/> with the list of users and total count.</returns>
        Task<GetListUserResponse> GetListUserAsync(GetListUserRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new user based on the provided details.
        /// </summary>
        /// <param name="request">Details of the user to create.</param>
        /// <returns>A <see cref="UserDtoResponse"/> with the created user information.</returns>
        Task<UserDtoResponse> CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Updates an existing user with the specified identifier and details.
        /// </summary>
        /// <param name="id">Unique identifier of the user to update.</param>
        /// <param name="request">Updated user details.</param>
        /// <returns>A <see cref="UserDtoResponse"/> with the updated user information.</returns>
        Task<UserDtoResponse> UpdateUserAsync(Guid id, UpdateUserRequest request);

        /// <summary>
        /// Executes a soft delete on a user based on its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the user to delete.</param>
        /// <returns>A boolean indicating the success of the deletion.</returns>
        Task<bool> DeleteUserAsync(Guid id);
    }
}
