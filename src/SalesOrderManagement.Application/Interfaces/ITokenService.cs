using SalesOrderManagement.Application.Authentication;
using SalesOrderManagement.Application.Dtos.Auth;


namespace SalesOrderManagement.Application.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticationResponse> GenerateJwtToken(UserAuthenticateJWT userAuthenticateJwt);
        Task<string> GenerateRefreshToken(Guid userUuid);
        int GetTokenLifetimeInMinutes();
    }
}
