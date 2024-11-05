namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a role within the system, defining a set of permissions and associated users.
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of users associated with this role.
        /// Represents a relationship between the role and the <see cref="User"/> entity.
        /// </summary>
        public IList<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the collection of permissions associated with this role.
        /// Represents a many-to-many relationship with the <see cref="Permission"/> entity.
        /// </summary>
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
