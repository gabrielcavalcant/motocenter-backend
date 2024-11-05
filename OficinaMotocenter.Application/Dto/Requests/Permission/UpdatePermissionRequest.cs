using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Permission
{
    /// <summary>
    /// DTO for updating a permission, containing required fields for modification.
    /// </summary>
    public class UpdatePermissionRequest
    {
        /// <summary>
        /// Gets or sets the name of the permission.
        /// </summary>
        [Required(ErrorMessage = "O nome da permissão é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the permission.
        /// </summary>
        [Required(ErrorMessage = "Uma descrição é obrigatória.")]
        public string Description { get; set; }
    }
}
