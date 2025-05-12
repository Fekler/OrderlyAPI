namespace SalesOrderManagement.Application.Dtos.Entities.OrderItem
{
    public class OrderItemDto
    {
        public Guid UUID { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}