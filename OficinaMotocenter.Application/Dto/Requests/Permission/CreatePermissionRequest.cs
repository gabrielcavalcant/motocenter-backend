using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.Permission
{
    /// <summary>
    /// DTO for creating a new permission, containing required fields for defining a permission.
    /// </summary>
    public class CreatePermissionRequest
    {
        /// <summary>
        /// Gets or sets the name of the new permission.
        /// </summary>
        [Required(ErrorMessage = "O nome da permissão é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the new permission.
        /// </summary>
        [Required(ErrorMessage = "Uma descrição é obrigatória.")]
        public string Description { get; set; }
    }
}
