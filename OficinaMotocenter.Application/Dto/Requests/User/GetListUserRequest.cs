using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficinaMotocenter.Application.Dto.Requests.User
{
    /// <summary>
    /// DTO for retrieving a paginated list of motorcycles with optional filters.
    /// </summary>
    public class GetListUserRequest : GenericListRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? FullName { get; set; }
    }
}
