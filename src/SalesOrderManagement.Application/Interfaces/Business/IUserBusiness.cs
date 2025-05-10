using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Application.Interfaces._bases;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.Business
{
    public interface IUserBusiness : IBusinessBase<User, CreateUserDto, UpdateUserDto, UserDto>
    {
        Task<Response<User>> Get(string email);
    }
}
