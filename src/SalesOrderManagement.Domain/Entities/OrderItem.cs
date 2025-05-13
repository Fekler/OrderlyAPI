using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Validations;

namespace SalesOrderManagement.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public void CalculateTotalPrice() => TotalPrice = UnitPrice * Quantity;


        public override void Validate()
        {
            base.Validate();

            RuleValidator.Build()
                .When(OrderId == Guid.Empty, "OrderId is required.")
                .When(ProductId == Guid.Empty, "ProductId is required.")
                .When(Quantity <= 0, "Quantity must be greater than zero.")
                .When(UnitPrice < 0, "UnitPrice cannot be negative.")
                .When(TotalPrice < 0, "TotalPrice cannot be negative.")
                .When(TotalPrice != Quantity * UnitPrice, "TotalPrice must be equal to Quantity multiplied by UnitPrice.")
                .ThrowExceptionIfExists();

        }
    }
}
