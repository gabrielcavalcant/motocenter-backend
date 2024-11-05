namespace OficinaMotocenter.Application.Dto.Responses
{
    /// <summary>
    /// Generic DTO for returning a paginated list.
    /// </summary>
    public class GenericListResponse
    {

        /// <summary>
        /// The current page index being retrieved.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The total number of permissions in the system.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
