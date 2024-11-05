using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Role
{
    /// <summary>
    /// DTO for updating an existing role, containing required fields for modification.
    /// </summary>
    public class UpdateRoleRequest
    {
        /// <summary>
        /// Gets or sets the updated name of the role.
        /// </summary>
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; } // Updated name of the role
    }
}
