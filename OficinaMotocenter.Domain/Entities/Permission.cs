namespace OficinaMotocenter.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public Guid PermissionId { get; set; } // Usando Guid como chave primária
        public string Name { get; set; }

        public string Description { get; set; }

        // Many-to-many relationship with Role
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
