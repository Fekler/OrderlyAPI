using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using static global::SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.Dtos.Entities.Order
{
    public class OrderDto
    {
        public Guid UUID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public Guid CreateByUserUuid { get; set; }
        public string CreateByUserName { get; set; }
        public string CreateByUserEmail { get; set; }
        public Guid? ActionedByUserUuid { get; set; }
        public DateTime? ActionedAt { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }
    }
}