using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Role
{
    /// <summary>
    /// DTO for creating a new role, containing required fields for defining a role.
    /// </summary>
    public class CreateRoleRequest
    {
        /// <summary>
        /// Gets or sets the name of the new role.
        /// </summary>
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; }
    }
}
