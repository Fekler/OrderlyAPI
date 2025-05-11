using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Context;
using SalesOrderManagement.API.Infra.Repositories._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.API.Infra.Repositories
{
    public class OrderRepository(AppDbContext context) : RepositoryBase<Order>(context), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrdersByUserId(Guid userId)
        {
            return await _dbSet
                .Where(o => o.CreateByUserUuid == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByStatus(OrderStatus status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }
        public async Task<Order> GetOrderWithOrdemItems(Guid uuid)
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UUID == uuid);
        }
        public async Task<IEnumerable<Order>> GetAllWithItemsAsync()
        {
            return await _dbSet
                .Include(o => o.OrderItems)
                .ToListAsync();
        }
    }
}
