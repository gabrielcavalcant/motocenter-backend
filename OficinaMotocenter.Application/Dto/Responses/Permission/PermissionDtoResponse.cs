using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Responses.Permission
{
    /// <summary>
    /// DTO for returning permission information in responses, including details such as ID, name, and description.
    /// </summary>
    public class PermissionDtoResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the permission.
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the permission.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the permission.
        /// </summary>
        [Required(ErrorMessage = "Uma descrição é obrigatória.")]
        public string Description { get; set; }
    }
}
