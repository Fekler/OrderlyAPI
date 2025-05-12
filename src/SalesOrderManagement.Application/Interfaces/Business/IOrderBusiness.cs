using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Interfaces.Business._bases;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.Business
{
    public interface IOrderBusiness : IBusinessBase<Order, CreateOrderDto, UpdateOrderDto, OrderDto>
    {
        Task<Response<IEnumerable<OrderDto>>> GetOrdersByUserId(Guid userId);
        Task<Response<IEnumerable<OrderDto>>> GetOrdersByStatus(string status);
        Task<Response<IEnumerable<OrderDto>>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
        Task<Response<OrderDto>> GetOrderWithOrdemItems(Guid uuid);
        Task<Response<IEnumerable<OrderDto>>> GetAllWithItemsAsync();

    }
}
