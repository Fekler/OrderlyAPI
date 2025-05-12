using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using System.ComponentModel.DataAnnotations;
using static SalesOrderManagement.Domain.Entities._bases.Enums;
namespace SalesOrderManagement.Application.Dtos.Entities.Order
{
    public class CreateOrderDto
    {
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = Error.INVALID_ADDRESS)]
        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de entrega não pode exceder 255 caracteres.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = Error.INVALID_ADDRESS)]
        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de cobrança não pode exceder 255 caracteres.")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public PaymentMethod PaymentMethod { get; set; }

        public Guid? CreateByUserUuid { get; set; }

        [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}

