namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a user within the system, containing authentication details, role association, and optional profile data.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; } 

        /// <summary>
        /// Gets or sets the unique email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets an optional base64-encoded string, potentially for profile images or other data.
        /// </summary>
        public string? Base64 { get; set; }

        /// <summary>
        /// Gets or sets the hashed password for user authentication.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the hashed refresh token, used for renewing the access token.
        /// </summary>
        public string? HashedRt { get; set; }

        /// <summary>
        /// Gets or sets the password reset token for account recovery.
        /// </summary>
        public string? PasswordResetToken { get; set; }

        /// <summary>
        /// Gets or sets the expiry date for the password reset token.
        /// </summary>
        public DateTime? ResetTokenExpiry { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated role, if applicable.
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role associated with the user. This property can be null if no role is assigned.
        /// </summary>
        public Role? Role { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class using a partial user object, copying over relevant properties.
        /// </summary>
        /// <param name="partial">The partial user object with properties to copy.</param>
        public User(User partial)
        {
            UserId = partial.UserId;
            Email = partial.Email;
            FullName = partial.FullName;
            Base64 = partial.Base64;
            Hash = partial.Hash;
            HashedRt = partial.HashedRt;
            RoleId = partial.RoleId;
            Role = partial.Role;
        }
    }
}
