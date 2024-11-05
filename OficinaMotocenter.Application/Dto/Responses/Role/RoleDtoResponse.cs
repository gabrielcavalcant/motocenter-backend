using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OficinaMotocenter.Application.Dto.Responses.Permission;

namespace OficinaMotocenter.Application.Dto.Responses.Role
{
    /// <summary>
    /// DTO for returning role information in responses, including details such as ID, name, and associated permissions.
    /// </summary>
    public class RoleDtoResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the role.
        /// </summary>
        public Guid Id { get; set; } // Role ID

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; } // Role name

        /// <summary>
        /// Gets or sets the list of permissions associated with this role.
        /// </summary>
        public IList<PermissionDtoResponse> Permissions { get; set; } // Associated permissions
    }
}
