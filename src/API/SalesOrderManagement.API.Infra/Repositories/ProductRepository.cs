using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Context;
using SalesOrderManagement.API.Infra.Repositories._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces.Repositories;

namespace SalesOrderManagement.API.Infra.Repositories
{
    public class ProductRepository(AppDbContext context) : RepositoryBase<Product>(context), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllByCategory(string category)
        {
            return await _dbSet
                .Where(p => p.Category == category)
                .ToListAsync();
        }
    }
}
