namespace OficinaMotocenter.Application.Dto.Responses.User
{
    /// <summary>
    /// DTO for returning user information in responses, including details such as ID, email, full name, and role information.
    /// </summary>
    public class UserDtoResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; } // User ID

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the optional Base64-encoded data, potentially for profile images or other data.
        /// </summary>
        public string? Base64 { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user's role, if assigned.
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user's role, if assigned.
        /// </summary>
        public string? RoleName { get; set; }
    }
}
