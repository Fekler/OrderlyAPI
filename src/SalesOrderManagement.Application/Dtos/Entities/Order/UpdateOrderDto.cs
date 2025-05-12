using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.Dtos.Entities.Order
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = Error.INVALID_UUID)]
        public Guid UUID { get; set; }

        public DateTime? OrderDate { get; set; }

        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de entrega não pode exceder 255 caracteres.")]
        public string ShippingAddress { get; set; }

        [MaxLength(Const.ADDRESS_MAX_LENGTH, ErrorMessage = "O endereço de cobrança não pode exceder 255 caracteres.")]
        public string BillingAddress { get; set; }

        public PaymentMethod? PaymentMethod { get; set; }

        public OrderStatus? Status { get; set; }

        public Guid? ActionedByUserUuid { get; set; }

        public ICollection<UpdateOrderItemDto> OrderItems { get; set; }
    }
}