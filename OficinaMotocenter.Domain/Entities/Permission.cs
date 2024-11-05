namespace OficinaMotocenter.Domain.Entities
{
    /// <summary>
    /// Represents a permission within the system, defining access rights that can be associated with roles.
    /// </summary>
    public class Permission : BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the permission.
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the permission.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of what the permission allows.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of roles associated with this permission.
        /// Represents a many-to-many relationship with the <see cref="Role"/> entity.
        /// </summary>
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
