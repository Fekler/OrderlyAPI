using Mapster;
using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Dtos.Entities.Product;
using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Domain.Entities;
using static SalesOrderManagement.Domain.Entities._bases.Enums;


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
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow)
                .Map(dest => dest.UUID, src => Guid.CreateVersion7());

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
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow)
                .Map(dest => dest.UUID, src => Guid.CreateVersion7());


            TypeAdapterConfig<UpdateProductDto, Product>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Map(dest => dest.UpdateAt, src => DateTime.UtcNow);
            #endregion

            #region Order
            TypeAdapterConfig<Order, OrderDto>.NewConfig().TwoWays();

            TypeAdapterConfig<CreateOrderDto, Order>.NewConfig()
                .Map(dest => dest.OrderDate, src => DateTime.UtcNow)
                .Map(dest => dest.ShippingAddress, src => src.ShippingAddress.Trim())
                .Map(dest => dest.BillingAddress, src => src.BillingAddress.Trim())
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow)
                .Map(dest => dest.Status, src => OrderStatus.Pending)
                .Map(dest => dest.UUID, src => Guid.CreateVersion7());


            TypeAdapterConfig<UpdateOrderDto, Order>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Ignore(dest => dest.OrderItems) 
                .Map(dest => dest.UpdateAt, src => DateTime.UtcNow)
                .Map(dest => dest.ShippingAddress, src => src.ShippingAddress.Trim())
                .Map(dest => dest.BillingAddress, src => src.BillingAddress.Trim());
            #endregion

            #region OrderItem
            TypeAdapterConfig<OrderItem, OrderItemDto>.NewConfig()
                .Map(dest => dest.ProductName, src => src.Product.Name);

            TypeAdapterConfig<OrderItemDto, OrderItem>.NewConfig().Ignore(dest => dest.Product); 

            TypeAdapterConfig<CreateOrderItemDto, OrderItem>.NewConfig()
                .Map(dest => dest.CreateAt, src => DateTime.UtcNow)
                .Map(dest => dest.UUID, src => Guid.CreateVersion7());

            TypeAdapterConfig<UpdateOrderItemDto, OrderItem>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Map(dest => dest.UpdateAt, src => DateTime.UtcNow);

            #endregion
        }
    }
}