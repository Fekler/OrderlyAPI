using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.API.Infra.Context;
using SalesOrderManagement.API.Infra.Repositories._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.API.Infra.Repositories
{
    public class UserRepository(AppDbContext context) : RepositoryBase<User>(context), IUserRepository
    {
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllByRole(UserRole role)
        {
            return await _dbSet
                .Where(u => u.UserRole == role)
                .ToListAsync();
        }

        public async Task<User> GetByDocument(string document)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<User> GetByEmail(string email)

        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByPhone(string phone)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Phone == phone);
        }
    }
}

