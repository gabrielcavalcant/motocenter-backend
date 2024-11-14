namespace OficinaMotocenter.Application.Dto.Responses.User
{
    /// <summary>
    /// DTO for returning a paginated list of user. It includes a list of user,
    /// the current page index, and the total count of user.
    /// </summary>
    public class GetListUserResponse : GenericListResponse
    {
        /// <summary>
        /// A list of users for the current page.
        /// </summary>
        public List<UserDto> Users { get; set; }
    }

    /// <summary>
    /// DTO that represents a single user in the list.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; }
    }
}
