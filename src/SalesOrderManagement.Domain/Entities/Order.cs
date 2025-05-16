using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Domain.Validations;
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


        public Order()
        {
            if (string.IsNullOrWhiteSpace(OrderNumber))
                GenerateOrderNumber();
        }

        public void GenerateOrderNumber()
        {
            OrderNumber = $"ORD-{CreateAt:yyyyMMddHHmmss}-{UUID.ToString()[..4].ToUpper()}";
        }

        public void CalculateTotalAmount() => TotalAmount = OrderItems.Sum(item => item.TotalPrice);


        public override void Validate()
        {
            base.Validate();
            RuleValidator.Build()
                .When(string.IsNullOrWhiteSpace(OrderNumber), "OrderNumber is required.")
                .When(OrderDate == default, Error.INVALID_DATE)
                .When(TotalAmount < 0, "TotalAmount cannot be negative.")
                .When(string.IsNullOrWhiteSpace(ShippingAddress) || ShippingAddress?.Length > Const.ADDRESS_MAX_LENGTH, Error.INVALID_ADDRESS)
                .When(string.IsNullOrWhiteSpace(BillingAddress) || BillingAddress?.Length > Const.ADDRESS_MAX_LENGTH, Error.INVALID_ADDRESS)
                .When(CreateByUserUuid == Guid.Empty, "CreateByUserUuid is required.")
                .ThrowExceptionIfExists();

            //foreach (var item in OrderItems)
            //{
            //    item?.Validate();
            //}
        }
    }
}


