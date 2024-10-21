using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Permission
{
    public class CreatePermissionRequest
    {
        [Required(ErrorMessage = "O nome da permissão é obrigatório.")]
        public string Name { get; set; } // Nome da permissão

        [Required(ErrorMessage = "Uma descrição é obrigatório.")]
        public string Description { get; set; } // Descrição da permissão
    }
}
