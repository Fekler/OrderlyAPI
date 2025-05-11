using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces._bases;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Domain.Interfaces.Repositories
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId);
        Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status);
        Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);

        Task<Order> GetOrderWithOrdemItems(Guid uuid);
        Task<IEnumerable<Order>> GetAllWithItemsAsync();

    }
}
