using SalesOrderManagement.Domain.Entities._bases;

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

    }
}
