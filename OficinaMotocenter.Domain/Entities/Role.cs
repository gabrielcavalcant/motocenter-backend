namespace OficinaMotocenter.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; }

        // Relationship with Users
        public IList<User> Users { get; set; }

        // Many-to-many relationship with Permission
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
