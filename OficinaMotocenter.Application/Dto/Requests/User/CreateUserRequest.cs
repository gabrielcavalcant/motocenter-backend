using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.User
{
        public class CreateUserRequest
        {
            [Required(ErrorMessage = "O nome da role é obrigatório.")]
            public string Name { get; set; } // Nome da nova role

            [Required(ErrorMessage = "O email do usuário é obrigatório.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O nome completo é obrigatório.")]
            public string FullName { get; set; }
        }
}
