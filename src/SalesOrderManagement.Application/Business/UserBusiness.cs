using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderManagement.Application.Business
{
    public class UserBusiness : IUserBusiness
    {


        public Task<Response<Guid>> Add(CreateUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserDto>> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<UserDto>> GetDto(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> GetEntity(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Update(UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }

}
