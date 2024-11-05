using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OficinaMotocenter.Domain.Entities;

namespace OficinaMotocenter.Persistence.Mapping
{
    /// <summary>
    /// Provides configuration for the <see cref="User"/> entity.
    /// Inherits from <see cref="BaseEntityConfiguration{User}"/> to reuse common configurations.
    /// </summary>
    public sealed class UserConfiguration : BaseEntityConfiguration<User>
    {
        /// <summary>
        /// Configures the <see cref="User"/> entity.
        /// Defines primary key, properties, and other constraints.
        /// </summary>
        /// <param name="builder">A <see cref="EntityTypeBuilder{User}"/> used to configure the <see cref="User"/> entity.</param>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            // Configurando a chave primária
            builder.HasKey(x => x.UserId);

            // Propriedade Email com restrição de unicidade e tamanho máximo
            builder.Property(x => x.Email)
                .IsRequired() // Campo obrigatório
                .HasMaxLength(255); // Definindo o tamanho máximo do email

            builder.HasIndex(x => x.Email)
                .IsUnique(); // Índice único para o email

            // Propriedade FullName com tamanho máximo
            builder.Property(x => x.FullName)
                .IsRequired() // Campo obrigatório
                .HasMaxLength(255); // Definindo o tamanho máximo do nome completo

            // Propriedade Base64 que é opcional
            builder.Property(x => x.Base64)
                .HasMaxLength(2048); // Definindo o tamanho máximo para Base64

            // Propriedade Hash como obrigatória
            builder.Property(x => x.Hash)
                .IsRequired(); // Campo obrigatório

            // Propriedade HashedRt que é opcional
            builder.Property(x => x.HashedRt)
                .HasMaxLength(512); // Definindo o tamanho máximo para o hashed refresh token

            // Propriedade PasswordResetToken que é opcional
            builder.Property(x => x.PasswordResetToken)
                .HasMaxLength(512); // Definindo o tamanho máximo para o token de redefinição de senha

            // Propriedade ResetTokenExpiry que é opcional
            builder.Property(x => x.ResetTokenExpiry);

            // Relacionamento com a entidade Role (opcional)
            builder.HasOne(x => x.Role)
                .WithMany() 
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.SetNull); // Definindo comportamento de exclusão

            // Chamando a configuração base (caso tenha lógica adicional)
            base.Configure(builder);
        }
    }
}
