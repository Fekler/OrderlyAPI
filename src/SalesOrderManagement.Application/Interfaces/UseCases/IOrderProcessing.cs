using SalesOrderManagement.Application.Dtos.Entities.Order;
using SharedKernel.Utils;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.Interfaces.UseCases
{
    public interface IOrderProcessing
    {
        Task<Response<Guid>> CreateOrder(CreateOrderDto createOrderDto);
        Task<Response<IEnumerable<OrderDto>>> GetAllByLoggedUser(Guid userUuid);
        Task<Response<bool>> ActionOrder(Guid orderUuid, Guid userUuid, OrderStatus orderStatus);
    }
}
