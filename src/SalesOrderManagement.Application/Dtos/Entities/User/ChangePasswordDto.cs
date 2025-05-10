using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.Application.Dtos.Entities.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "O UUID do usuário é obrigatório.")]
        public Guid UUID { get; set; }

        [Required(ErrorMessage = "A senha antiga é obrigatória.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A nova senha deve ter pelo menos 8 caracteres.")]
        // Podemos adicionar aqui outras validações de força de senha, se necessário (ex: Regex)
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "A confirmação da nova senha é obrigatória.")]
        [Compare("NewPassword", ErrorMessage = "A nova senha e a confirmação não coincidem.")]
        public string ConfirmNewPassword { get; set; }
    }
}