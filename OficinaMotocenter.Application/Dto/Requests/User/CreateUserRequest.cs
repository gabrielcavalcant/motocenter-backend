using System.ComponentModel.DataAnnotations;

namespace OficinaMotocenter.Application.Dto.Requests.User
{
    /// <summary>
    /// DTO for creating a new user, containing required fields for defining user information.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Gets or sets the name of the user's role.
        /// </summary>
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessage = "A senha do usuário é obrigatória.")]
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; }
    }
}
