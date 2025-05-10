using SalesOrderManagement.Application.Dtos.Auth;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.UseCases
{
    public interface IUserAuthentication
    {
        Task<Response<AuthenticationResponse>> Login(string email, string password);

    }
}
