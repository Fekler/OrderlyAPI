using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Domain.Entities._bases;
using System.ComponentModel.DataAnnotations;
using static SalesOrderManagement.Domain.Entities._bases.Enums;
namespace SalesOrderManagement.Application.Dtos.Entities.Order
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "O endereço de entrega é obrigatório.")]
        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de entrega não pode exceder 255 caracteres.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "O endereço de cobrança é obrigatório.")]
        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de cobrança não pode exceder 255 caracteres.")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required(ErrorMessage = "O UUID do usuário que criou o pedido é obrigatório.")]
        public Guid CreateByUserUuid { get; set; }

        [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
        public ICollection<CreateOrderItemDto> OrderItems { get; set; }
    }
}

