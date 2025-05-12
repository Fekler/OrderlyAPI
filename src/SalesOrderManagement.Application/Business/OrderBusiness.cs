using Mapster;
using Microsoft.Extensions.Logging;
using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using SharedKernel.Utils;
using System.Net;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Application.Interfaces.Business;

namespace SalesOrderManagement.Application.Business
{
    public class OrderBusiness(IOrderRepository orderRepository, ILogger<OrderBusiness> logger) : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly ILogger<OrderBusiness> _logger = logger;

        public async Task<Response<Guid>> Add(CreateOrderDto createOrderDto)
        {
            try
            {
                var order = createOrderDto.Adapt<Order>();
                order.OrderNumber = GenerateOrderNumber();
                order.UUID = Guid.NewGuid();
                order.TotalAmount = 0; 

                await _orderRepository.Add(order);
                _logger.LogInformation($"Pedido criado com UUID: {order.UUID} e número: {order.OrderNumber}");
                return new Response<Guid>().Sucess(order.UUID, message: Const.CREATE_SUCCESS, statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novo pedido.");
                return new Response<Guid>().Failure(default, message: "Erro ao adicionar novo pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _orderRepository.Delete(id);
                if (deleted)
                {
                    _logger.LogInformation($"Pedido com ID: {id} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar pedido com ID: {id}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar pedido com ID: {id}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(Guid guid)
        {
            try
            {
                var orderToDelete = await _orderRepository.Get(guid);
                if (orderToDelete == null)
                {
                    return new Response<bool>().Failure(false, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                var result = await _orderRepository.Delete(orderToDelete.UUID);
                if (result)
                {
                    _logger.LogInformation($"Pedido com UUID: {guid} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar pedido com UUID: {guid}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar pedido com UUID: {guid}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderDto>> Get(int id)
        {
            try
            {
                var order = await _orderRepository.Get(id);
                if (order == null)
                {
                    return new Response<OrderDto>().Failure(default, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var orderDto = order.Adapt<OrderDto>();
                return new Response<OrderDto>().Sucess(orderDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter pedido com ID: {id}.");
                return new Response<OrderDto>().Failure(default, message: "Erro ao obter pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetAll()
        {
            try
            {
                var orders = await _orderRepository.GetAllWithItemsAsync();
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
                return new Response<IEnumerable<OrderDto>>().Sucess(orderDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os pedidos.");
                return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Erro ao obter todos os pedidos.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderDto>> GetDto(Guid guid)
        {
            try
            {
                var order = await _orderRepository.Get(guid);
                if (order == null)
                {
                    return new Response<OrderDto>().Failure(default, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var orderDto = order.Adapt<OrderDto>();
                return new Response<OrderDto>().Sucess(orderDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter pedido com UUID: {guid} como DTO.");
                return new Response<OrderDto>().Failure(default, message: "Erro ao obter pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<Order>> GetEntity(Guid guid)
        {
            try
            {
                var order = await _orderRepository.Get(guid);
                if (order == null)
                {
                    return new Response<Order>().Failure(default, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                return new Response<Order>().Sucess(order, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter pedido com UUID: {guid} como entidade.");
                return new Response<Order>().Failure(default, message: "Erro ao obter pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Update(UpdateOrderDto updateOrderDto)
        {
            try
            {
                var existingOrder = await _orderRepository.Get(updateOrderDto.UUID);
                if (existingOrder == null)
                {
                    return new Response<bool>().Failure(false, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                updateOrderDto.Adapt(existingOrder);
                existingOrder.UpdateAt = DateTime.UtcNow;

                var result = await _orderRepository.Update(existingOrder);
                if (result)
                {
                    _logger.LogInformation($"Pedido com UUID: {updateOrderDto.UUID} atualizado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.UPDATE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao atualizar pedido com UUID: {updateOrderDto.UUID}.");
                    return new Response<bool>().Failure(false, message: "Falha ao atualizar pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar atualizar pedido com UUID: {updateOrderDto.UUID}.");
                return new Response<bool>().Failure(false, message: "Erro ao atualizar pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetOrdersByUserId(Guid userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserId(userId);
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
                return new Response<IEnumerable<OrderDto>>().Sucess(orderDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter pedidos do usuário com UUID: {userId}.");
                return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Erro ao obter pedidos do usuário.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            try
            {
                if (!Enum.TryParse<Enums.OrderStatus>(status, true, out var orderStatus))
                {
                    return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Status de pedido inválido.", statusCode: HttpStatusCode.BadRequest);
                }
                var orders = await _orderRepository.GetOrdersByStatus(orderStatus);
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
                return new Response<IEnumerable<OrderDto>>().Sucess(orderDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter pedidos com status: {status}.");
                return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Erro ao obter pedidos por status.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByDateRange( startDate , endDate);
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
                return new Response<IEnumerable<OrderDto>>().Sucess(orderDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter pedidos no período de {startDate} a {endDate}.");
                return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Erro ao obter pedidos por período.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderDto>> GetOrderWithOrdemItems(Guid uuid)
        {
            try
            {
                var order = await _orderRepository.GetOrderWithOrdemItems(uuid);
                if (order == null)
                {
                    return new Response<OrderDto>().Failure(default, message: Error.ORDER_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var orderDto = order.Adapt<OrderDto>();
                return new Response<OrderDto>().Sucess(orderDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter pedido com UUID: {uuid} e seus itens.");
                return new Response<OrderDto>().Failure(default, message: "Erro ao obter pedido com itens.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetAllWithItemsAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllWithItemsAsync();
                var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
                return new Response<IEnumerable<OrderDto>>().Sucess(orderDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os pedidos com seus itens.");
                return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Erro ao obter todos os pedidos com itens.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        private static string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.Now:yyyyMMddHHmmss}-{Guid.CreateVersion7().ToString()[..4].ToUpper()}";
        }
    }
}