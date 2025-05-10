using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces._bases;

namespace SalesOrderManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByDocument(string document);
        Task<User> GetByPhone(string phone);

    }
}
