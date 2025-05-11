namespace SalesOrderManagement.Application.Dtos.Entities.Product
{
    public class ProductDto
    {
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
    }
}