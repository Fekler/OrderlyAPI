using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.UseCases
{
    public interface IOrderProcessing
    {
        Task<Response<Guid>> CreateOrder(CreateOrderDto createOrderDto);
    }
}
