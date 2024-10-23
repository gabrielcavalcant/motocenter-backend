using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Responses.Permission
{
    public class PermissionDtoResponse
    {
        public Guid PermissionId { get; set; } // ID da permissão
        public string Name { get; set; } // Nome da permissão

        [Required(ErrorMessage = "Uma descrição é obrigatório.")]
        public string Description { get; set; } // Descrição da permissão
    }
}
