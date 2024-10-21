using OficinaMotocenter.Application.Dto.Responses.Permission;
using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Responses.Role
{
    internal class RoleDtoResponse
    {
        public Guid Id { get; set; } // ID da role

        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; } // Nome da role


        // Lista detalhada de permissões associadas à role
        public IList<PermissionDtoResponse> Permissions { get; set; }
    }
}
