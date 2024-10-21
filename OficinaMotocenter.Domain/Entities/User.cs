namespace OficinaMotocenter.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; } // Usando Guid como chave primária

        public string Email { get; set; } // Email único

        public string FullName { get; set; } // Nome completo

        public string? Base64 { get; set; } // Propriedade opcional

        public string Hash { get; set; } // Hash da senha

        public string? HashedRt { get; set; } // Token de refresh hashed

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpiry { get; set; }

        public Guid? RoleId { get; set; }
        public Role? Role { get; set; } // Permite que a propriedade Role seja nula

        public User()
        {
        }

        // Construtor que aceita um objeto parcial
        public User(User partial)
        {
            UserId = partial.UserId;
            Email = partial.Email;
            FullName = partial.FullName;
            Base64 = partial.Base64;
            Hash = partial.Hash;
            HashedRt = partial.HashedRt;
            RoleId = partial.RoleId;
            Role = partial.Role; // Copiando a Role, que agora pode ser null
        }
    }
}
