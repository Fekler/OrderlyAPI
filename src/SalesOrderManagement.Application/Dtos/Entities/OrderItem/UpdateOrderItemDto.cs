using SalesOrderManagement.Domain.Errors;
using System.ComponentModel.DataAnnotations;

namespace SalesOrderManagement.Application.Dtos.Entities.OrderItem
{
    public class UpdateOrderItemDto
    {
        [Required(ErrorMessage = Error.INVALID_UUID)]
        public Guid UUID { get; set; }

        [Required(ErrorMessage = "O UUID do produto é obrigatório.")]
        public Guid ProductId { get; set; }        
        
        [Required(ErrorMessage = "O UUID do produto é obrigatório.")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantity { get; set; }
    }
}