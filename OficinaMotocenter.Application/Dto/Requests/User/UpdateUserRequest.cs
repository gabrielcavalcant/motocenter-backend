namespace OficinaMotocenter.Application.Dto.Requests.User
{
    /// <summary>
    /// DTO for updating an existing user's information, containing optional fields for modification.
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Gets or sets the full name of the user.
        /// This field is optional and only updated if provided.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the Base64-encoded data, potentially for profile images or other data.
        /// This field is optional.
        /// </summary>
        public string? Base64 { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the role to be assigned to the user.
        /// This field is optional and only updated if provided.
        /// </summary>
        public Guid? RoleId { get; set; }
    }
}
