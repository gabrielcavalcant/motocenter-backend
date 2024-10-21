using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Role
{
    public class UpdateRoleRequest
    {
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; } // Nome da nova role
    }
}
