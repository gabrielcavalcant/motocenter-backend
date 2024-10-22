using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OficinaMotocenter.Application.Dto.Requests.User;
using OficinaMotocenter.Application.Dto.Responses.User;
using OficinaMotocenter.Application.Interfaces.Services;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotorcycle.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
        {
            User user = await _userService.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return NotFound(); // Retorna 404 se a role não for encontrada
            }

            // Mapeia a entidade Role para RoleDTO para a resposta
            UserDtoResponse userDto = _mapper.Map<UserDtoResponse>(user);
            return Ok(userDto); // Retorna a role encontrada como DTO
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetListUserRequest request,CancellationToken cancellationToken)
        {

            IList<User> users = await _userService.GetAllAsync(
                                cancellationToken,
                                filter: m => (string.IsNullOrEmpty(request.FullName) || m.FullName.Contains(request.FullName)),
                                skip: (request.PageIndex - 1) * request.PageSize,
                                take: request.PageSize
                            );

            IList<UserDtoResponse> userDTOs = users.Select(user => _mapper.Map<UserDtoResponse>(user)).ToList();

            return Ok(userDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest newUserDto, CancellationToken cancellationToken)
        {
            if (newUserDto == null)
            {
                return BadRequest("User não pode ser nula."); // Retorna 400 se a role for nula
            }

            User newUser = new()
            {
                FullName = newUserDto.FullName
            };

            var createdUser = await _userService.CreateAsync(newUser,cancellationToken);
            // Mapeia a entidade criada para o DTO para a resposta
            UserDtoResponse createdUserDto = _mapper.Map<UserDtoResponse>(createdUser);
            return CreatedAtAction(nameof(GetUser), new { id = createdUserDto.Id }, createdUserDto); // Retorna 201 com a nova role
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest updatedUserDTO, CancellationToken cancellationToken)
        {
            if (updatedUserDTO == null)
            {
                return BadRequest("User não pode ser nulo.");
            }

            var userToUpdate = await _userService.GetByIdAsync(id, cancellationToken);
            if (userToUpdate == null)
            {
                return NotFound("User não encontrado.");
            }

            // Atualiza apenas as propriedades necessárias
            _mapper.Map(updatedUserDTO, userToUpdate); // Atualiza a entidade com os dados do DTO

            await _userService.UpdateAsync(userToUpdate, cancellationToken);

            // Mapeia a entidade User para UserDTO
            var updatedUserDtoResult = _mapper.Map<UserDtoResponse>(userToUpdate);

            // Retorna o usuário atualizado com status 200 OK
            return Ok(updatedUserDtoResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            bool success = await _userService.DeleteAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
