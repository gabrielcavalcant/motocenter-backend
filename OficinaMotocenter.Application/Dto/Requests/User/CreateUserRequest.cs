using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.User
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; } // Novo nome da role
    }
}
