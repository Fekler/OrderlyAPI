using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Interfaces.Business._bases;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.Business
{
    public interface IOrderItemBusiness : IBusinessBase<OrderItem, CreateOrderItemDto, UpdateOrderItemDto, OrderItemDto>
    {
        Task<Response<IEnumerable<OrderItemDto>>> GetOrderItemsByOrderId(Guid orderId);

    }
}