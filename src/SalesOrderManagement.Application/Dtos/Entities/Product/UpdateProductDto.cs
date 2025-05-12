using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.Application.Dtos.Entities.Product
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = Error.INVALID_UUID)]
        public Guid UUID { get; set; }

        [MaxLength(Const.NAME_MAX_LENGTH, ErrorMessage = "O nome do produto não pode exceder 255 caracteres.")]
        public string Name { get; set; }

        [MaxLength(Const.DESCRIPTION_MAX_LENGTH, ErrorMessage = "A descrição do produto não pode exceder 1000 caracteres.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
        public decimal? Price { get; set; } 

        [Range(0, int.MaxValue, ErrorMessage = "A quantidade do produto não pode ser negativa.")]
        public int? Quantity { get; set; } 

        [MaxLength(Const.CATEGORY_MAX_LENGTH, ErrorMessage = "A categoria do produto não pode exceder 100 caracteres.")]
        public string Category { get; set; }

        public bool? IsActive { get; set; } 
    }
}