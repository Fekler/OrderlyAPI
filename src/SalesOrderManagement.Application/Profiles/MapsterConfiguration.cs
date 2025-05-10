using Mapster;
using SalesOrderManagement.Application.Dtos.Entities.User;
using SalesOrderManagement.Domain.Entities;


namespace SalesOrderManagement.Application.Profiles
{
    public class MapsterConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<CreateUserDto, User>.NewConfig()
                .Map(dest => dest.FullName, src => src.FullName.Trim()) 
                .Map(dest => dest.IsActive, src => true)
                .Map(dest => dest.CreateAt, src => DateTime.Now); 

            TypeAdapterConfig<UpdateUserDto, User>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CreateAt)
                .Map(dest => dest.UpdateAt, src => DateTime.Now);

            // .Map(dest => dest.UUID, src => src.UUID); // Já mapeado por padrão
        }
    }
}