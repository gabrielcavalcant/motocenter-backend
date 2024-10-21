namespace OficinaMotocenter.Application.Dto.Requests.User
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; } // Nome completo a ser atualizado
        public string? Base64 { get; set; } // Foto ou dado em Base64 (opcional)
        public Guid? RoleId { get; set; } // ID do papel a ser atualizado

    }
}
