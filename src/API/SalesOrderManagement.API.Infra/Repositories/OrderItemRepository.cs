using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Context;
using SalesOrderManagement.API.Infra.Repositories._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces.Repositories;

namespace SalesOrderManagement.API.Infra.Repositories
{
    public class OrderItemRepository(AppDbContext context) : RepositoryBase<OrderItem>(context), IOrderItemRepository
    {
        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
        {
            return await _dbSet
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }
        public async Task<OrderItem> GetOrderItemByProductId(Guid productId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.ProductId == productId);
        }
        public async Task<OrderItem> GetOrderItemWithProduct(Guid uuid)
        {
            return await _dbSet
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.UUID == uuid);
        }
        public async Task<IEnumerable<OrderItem>> GetAllWithProductAsync()
        {
            return await _dbSet
                .Include(x => x.Product)
                .ToListAsync();
        }
        public async Task<IEnumerable<OrderItem>> GetAll() => await _dbSet.ToListAsync();
    }
}
