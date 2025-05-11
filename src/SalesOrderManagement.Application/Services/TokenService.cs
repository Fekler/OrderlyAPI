using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SalesOrderManagement.Application.Authentication;
using SalesOrderManagement.Application.Dtos.Auth;
using SalesOrderManagement.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesOrderManagement.Application.Services
{
    public class TokenBusiness : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly string _jwtKey;

        public TokenBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
            //Env.TraversePath().Load();
            string getToken = Environment.GetEnvironmentVariable("TOKEN_JWT_SECRET");
            if (string.IsNullOrEmpty(getToken))
            {
                getToken = configuration["Jwt:Key"];
            }
            _jwtKey = getToken;
        }

        public async Task<AuthenticationResponse> GenerateJwtToken(UserAuthenticateJWT userAuthenticateJwt)
        {
            AuthenticationResponse authenticationResponse = new();
            var claims = new ClaimsIdentity(
            [
                new(ClaimTypes.NameIdentifier, userAuthenticateJwt.UserUuid.ToString()),
                new(ClaimTypes.Email, userAuthenticateJwt.UserEmail),
                new(ClaimTypes.Name, userAuthenticateJwt.UserName),
                new(ClaimTypes.Role, userAuthenticateJwt.UserRole),

            ]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(GetTokenLifetimeInMinutes());

            var token = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],

                Subject = claims,
                Expires = expiry,
                SigningCredentials = creds
            };
            var handler = new JwtSecurityTokenHandler();
            var tokenCreate = handler.CreateToken(token);
            authenticationResponse.AccessToken = handler.WriteToken(tokenCreate);
            authenticationResponse.ExpiresIn = (int)expiry.Subtract(DateTime.UtcNow).TotalMinutes;
            return authenticationResponse;
        }

        public int GetTokenLifetimeInMinutes()
        {
            var tokenLifetime = _configuration["Jwt:TokenLifetimeMinutes"];
            if (int.TryParse(tokenLifetime, out int tokenLifetimeMinutes))
            {
                return tokenLifetimeMinutes;
            }
            return 120;
        }
    }
}
