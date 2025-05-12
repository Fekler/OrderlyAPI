using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces._bases;

namespace SalesOrderManagement.Domain.Interfaces.Repositories
{
    public interface IOrderItemRepository : IRepositoryBase<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId);
        Task<OrderItem> GetOrderItemByProductId(Guid productId);
        Task<OrderItem> GetOrderItemWithProduct(Guid uuid);
        Task<IEnumerable<OrderItem>> GetAllWithProductAsync();


    }

}
