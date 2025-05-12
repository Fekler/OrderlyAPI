using SalesOrderManagement.Domain.Entities._bases;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Domain.Entities
{
    public class Order : EntityBase
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        public Guid CreateByUserUuid { get; set; }
        public User CreateByUser { get; set; }

        public Guid? ActionedByUserUuid { get; set; } 
        public User ActionedByUser { get; set; }
        public DateTime? ActionedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }

}