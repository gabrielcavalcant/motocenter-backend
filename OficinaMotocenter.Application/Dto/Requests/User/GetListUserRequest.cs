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
    public class GetListUserRequest
    {
        /// <summary>
        /// Optional filter by the name of the motorcycle.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// The page number to retrieve, defaulting to 1.
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// The number of items to retrieve per page, defaulting to 10.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
