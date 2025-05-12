using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces._bases;

namespace SalesOrderManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Product>> GetAllByCategory(string category);


    }
}
