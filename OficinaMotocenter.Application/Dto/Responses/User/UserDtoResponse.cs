using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaMotocenter.Application.Dto.Responses.User
{
    public class UserDtoResponse
    {
        public Guid Id { get; set; } // ID do usuário
        public string Email { get; set; } // Email do usuário
        public string FullName { get; set; } // Nome completo do usuário
        public string? Base64 { get; set; } // Foto ou dado em Base64 (opcional)
        public Guid? RoleId { get; set; } // ID do papel
        public string? RoleName { get; set; } // Nome do papel (pode ser null)
    }
}
