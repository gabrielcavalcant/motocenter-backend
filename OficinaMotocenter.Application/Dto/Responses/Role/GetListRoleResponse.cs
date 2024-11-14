namespace OficinaMotocenter.Application.Dto.Responses.Role
{
    namespace OficinaMotocenter.Application.Dto.Responses
    {
        /// <summary>
        /// DTO for returning a paginated list of role. It includes a list of role,
        /// the current page index, and the total count of role.
        /// </summary>
        public class GetListRoleResponse : GenericListResponse
        {
            /// <summary>
            /// A list of roles for the current page.
            /// </summary>
            public List<RoleDto> Roles { get; set; }
        }

        /// <summary>
        /// DTO that represents a single role in the list.
        /// </summary>
        public class RoleDto
        {
            /// <summary>
            /// The unique identifier of the role.
            /// </summary>
            public Guid RoleId { get; set; }

            /// <summary>
            /// The name of the role.
            /// </summary>
            public string Name { get; set; }
        }
    }

}
