using SalesOrderManagement.Domain.Errors;
using System.ComponentModel.DataAnnotations;
namespace SalesOrderManagement.Application.Dtos.Entities.OrderItem
{
    public class CreateOrderItemDto
    {
        [Required(ErrorMessage = $"Product {Error.INVALID_UUID} ")]
        public Guid ProductId { get; set; }


        public Guid? OrderId { get; set; }

        [Required(ErrorMessage = Error.INVALID_QUANTITY)]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantity { get; set; }

    }
}

