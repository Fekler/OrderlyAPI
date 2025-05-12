using SalesOrderManagement.Domain.Entities._bases;
using System.ComponentModel.DataAnnotations;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.Dtos.Entities.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "O Nome Completo é obrigatório.")]
        [MaxLength(Const.NAME_MAX_LENGTH, ErrorMessage = "O Nome Completo não pode exceder 255 caracteres.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        [MaxLength(Const.EMAIL_MAX_LENGTH, ErrorMessage = "O Email não pode exceder 255 caracteres.")]
        public string Email { get; set; }

        [MaxLength(Const.PHONE_MAX_LENGTH, ErrorMessage = "O Telefone não pode exceder 20 caracteres.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string Password { get; set; }

        [MaxLength(Const.DOCUMENT_MAX_LENGTH, ErrorMessage = "O Documento não pode exceder 18 caracteres.")]
        public string? Document { get; set; }

        [Required(ErrorMessage = "O Tipo de Usuário é obrigatório.")]
        [EnumDataType(typeof(UserRole), ErrorMessage = "O Tipo de Usuário não é válido.")]
        public UserRole UserRole { get; set; }
    }
}
