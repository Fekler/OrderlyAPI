using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Application.Interfaces.Business._bases;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.Business
{
    public interface IUserBusiness : IBusinessBase<User, CreateUserDto, UpdateUserDto, UserDto>
    {
        Task<Response<User>> Get(string email);
        Task<Response<bool>> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<Response<IEnumerable<UserDto>>> GetAll();
        Task<Response<IEnumerable<UserDto>>> GetAllByRole(string role);

    }
}
