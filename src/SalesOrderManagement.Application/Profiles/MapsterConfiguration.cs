using Mapster;
using SalesOrderManagement.Application.Dtos.Entities.Product;
using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Domain.Entities;


namespace SalesOrderManagement.Application.Profiles
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            #region User
            TypeAdapterConfig<UserDto, User>.NewConfig().TwoWays();

            TypeAdapterConfig<CreateUserDto, User>.NewConfig()
                .Map(dest => dest.FullName, src => src.FullName.Trim())
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow);

            TypeAdapterConfig<UpdateUserDto, User>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Map(dest => dest.UpdateAt, src => DateTime.UtcNow);

            #endregion

            #region Product
            TypeAdapterConfig<Product, ProductDto>.NewConfig().TwoWays();

            TypeAdapterConfig<CreateProductDto, Product>.NewConfig()
                .Map(dest => dest.Name, src => src.Name.Trim())
                .Map(dest => dest.Description, src => src.Description.Trim())
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow);

            TypeAdapterConfig<UpdateProductDto, Product>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Map(dest => dest.UpdateAt, src => DateTime.UtcNow); 
            #endregion
        }
    }
}