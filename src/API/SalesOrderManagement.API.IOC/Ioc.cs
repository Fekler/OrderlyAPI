using DotNetEnv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOrderManagement.API.Infra.Context;

namespace SalesOrderManagement.API.IOC
{
    public static class Ioc
    {
        public static void ConfigureServicesIoc(IServiceCollection services, IConfiguration configuration, ref string tokenSecret)
        {
            services.ConfigureEnvironmentVariables(configuration, ref tokenSecret, out string connectString);
            services.ConfigureDBContext(configuration, connectString);
            services.ConfigureRepositories();
            services.ConfigureValidators();
            services.ConfigureBusiness();
            services.ConfigureProfiles();
            services.ConfigureUseCases();
            services.ConfigureServices(tokenSecret);
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {

        }
        public static void ConfigureBusiness(this IServiceCollection services)
        {

            //services.AddScoped<IUserBusiness, UserBusiness>();
        }
        public static void ConfigureProfiles(this IServiceCollection services)
        {


        }
        public static void ConfigureServices(this IServiceCollection services, string tokenSecret)
        {
            //services.AddScoped<ITokenService, TokenBusiness>(); // Registra a implementação correta


        }
        public static void ConfigureValidators(this IServiceCollection services)
        {
            //services.AddSingleton<IValidatorProvider, ValidatorFactory>();
        }
        public static void ConfigureUseCases(this IServiceCollection services)
        {
            //services.AddScoped<IAuthenticationUseCase, AuthenticationUseCase>();
        }
        public static void ConfigureEnvironmentVariables(this IServiceCollection services, IConfiguration configuration, ref string tokenSecret, out string connectString)
        {
            Env.Load();
            Env.TraversePath().Load();
            connectString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            if (string.IsNullOrEmpty(connectString))
            {
                connectString = configuration.GetConnectionString("DefaultConnection");
            }
            tokenSecret = Environment.GetEnvironmentVariable("TOKEN_JWT_SECRET");
            if (string.IsNullOrEmpty(tokenSecret))
            {
                tokenSecret = configuration["Jwt:Key"];
            }
            string jwtSecret = tokenSecret;
        }
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration, string connectString)
        {

        }
    }
}
