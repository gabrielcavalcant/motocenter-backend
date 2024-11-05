using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.User;
using OficinaMotocenter.Application.Dto.Responses.User;
using OficinaMotocenter.Application.Interfaces.Services;

namespace OficinaMotorcycle.API.Controllers
{
    /// <summary>
    /// Controller for managing users, including actions to create, update, retrieve, and delete users.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The service used to manage user operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>An <see cref="IActionResult"/> containing the user details if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            UserDtoResponse response = await _userService.GetUserByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a paginated list of users based on filter criteria.
        /// </summary>
        /// <param name="request">Filter criteria and pagination settings.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing the list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetListUserRequest request, CancellationToken cancellationToken)
        {
            GetListUserResponse response = await _userService.GetListUserAsync(request, cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new user with the provided details.
        /// </summary>
        /// <param name="newUserDto">Details of the user to create.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created user details and location.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest newUserDto)
        {
            if (newUserDto == null)
            {
                return BadRequest("User não pode ser nulo.");
            }

            UserDtoResponse response = await _userService.CreateUserAsync(newUserDto);
            return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);
        }

        /// <summary>
        /// Updates an existing user with the specified ID and updated details.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="updatedUserDTO">The updated details of the user.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated user details or NotFound if the user does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest updatedUserDTO)
        {
            if (updatedUserDTO == null)
            {
                return BadRequest("User não pode ser nulo.");
            }

            UserDtoResponse response = await _userService.UpdateUserAsync(id, updatedUserDTO);
            if (response == null)
            {
                return NotFound("User não encontrado.");
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes an existing user based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        /// <returns>An <see cref="IActionResult"/> indicating success or NotFound if the user does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            bool success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
