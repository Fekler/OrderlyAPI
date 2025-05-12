using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesOrderManagement.API.Infra.Context;
using SalesOrderManagement.API.Infra.Repositories;
using SalesOrderManagement.Application.Business;
using SalesOrderManagement.Application.Dtos.Dashboard;
using SalesOrderManagement.Application.Interfaces;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Application.Interfaces.UseCases;
using SalesOrderManagement.Application.Profiles;
using SalesOrderManagement.Application.Services;
using SalesOrderManagement.Application.UseCases;
using SalesOrderManagement.Domain.Interfaces.Repositories;

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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }

        public static void ConfigureBusiness(this IServiceCollection services)
        {

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderItemBusiness, OrderItemBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
        }

        public static void ConfigureProfiles(this IServiceCollection services)
        {
            MapsterConfiguration.Configure();
        }

        public static void ConfigureServices(this IServiceCollection services, string tokenSecret)
        {
            services.AddScoped<ITokenService, TokenBusiness>();
        }

        public static void ConfigureValidators(this IServiceCollection services)
        {
            //services.AddSingleton<IValidatorProvider, ValidatorFactory>();
        }

        public static void ConfigureUseCases(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthentication, UserAuthentication>();
            services.AddScoped<IOrderProcessing, OrderProcessing>();
            services.AddScoped<IDashboard, Dashboard>();
        }

        public static void ConfigureEnvironmentVariables(this IServiceCollection services, IConfiguration configuration, ref string tokenSecret, out string connectString)
        {
            Env.TraversePath().Load();
            connectString = Environment.GetEnvironmentVariable("DATABASE_URL");
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
            services.AddDbContextPool<AppDbContext>(options =>
                  options.UseNpgsql(connectString, x => x.MigrationsAssembly("SalesOrderManagement.API.Infra")));
        }
    }
}
