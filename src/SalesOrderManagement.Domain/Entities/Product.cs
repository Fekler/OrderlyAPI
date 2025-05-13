using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Domain.Validations;

namespace SalesOrderManagement.Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<OrderItem> OrderItems { get; set; } = [];


        public override void Validate()
        {
            base.Validate();

            RuleValidator.Build()
                .When(string.IsNullOrWhiteSpace(Name) || Name?.Length > Const.NAME_MAX_LENGTH, Error.INVALID_NAME)
                .When(Price <= 0, Error.INVALID_PRICE)
                .When(Quantity < 0, Error.INVALID_QUANTITY)
                .When(string.IsNullOrWhiteSpace(Category) || Category?.Length > Const.CATEGORY_MAX_LENGTH, "Category is required and cannot exceed maximum length.")
                .ThrowExceptionIfExists();
        }
    }
}
