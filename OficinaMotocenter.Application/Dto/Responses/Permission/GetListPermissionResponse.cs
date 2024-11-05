using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaMotocenter.Application.Dto.Responses.Permission
{
    /// <summary>
    /// DTO for returning a paginated list of permissions. It includes a list of permission,
    /// the current page index, and the total count of permissions.
    /// </summary>
    public class GetListPermissionResponse : GenericListResponse
    {
        /// <summary>
        /// A list of permissions for the current page.
        /// </summary>
        public List<PermissionDto> Permissions { get; set; }
    }

    /// <summary>
    /// DTO that represents a single permission in the list.
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// The unique identifier of the permission.
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// The name of the permission.
        /// </summary>
        public string Name { get; set; }
    }
}
