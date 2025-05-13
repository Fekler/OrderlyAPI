using Microsoft.Extensions.Logging;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using SharedKernel.Utils;
using System.Net;
using Mapster;
using SalesOrderManagement.Domain.Errors;

namespace SalesOrderManagement.Application.Business
{
    public class OrderItemBusiness(IOrderItemRepository orderItemRepository, IProductRepository productRepository, ILogger<OrderItemBusiness> logger) : IOrderItemBusiness
    {
        private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
        private readonly IProductRepository _productRepository = productRepository; 
        private readonly ILogger<OrderItemBusiness> _logger = logger;

        public async Task<Response<Guid>> Add(CreateOrderItemDto createOrderItemDto)
        {
            try
            {
                var product = await _productRepository.Get(createOrderItemDto.ProductId);
                if (product == null)
                {
                    return new Response<Guid>().Failure(default, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                var orderItem = createOrderItemDto.Adapt<OrderItem>();
                orderItem.UnitPrice = product.Price; 
                orderItem.CalculateTotalPrice();
                orderItem.Validate();

                await _orderItemRepository.Add(orderItem);
                _logger.LogInformation($"Item de pedido criado com UUID: {orderItem.UUID} para o produto: {product.Name}");
                return new Response<Guid>().Sucess(orderItem.UUID, message: Const.CREATE_SUCCESS, statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novo item de pedido.");
                return new Response<Guid>().Failure(default, message: "Erro ao adicionar novo item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _orderItemRepository.Delete(id);
                if (deleted)
                {
                    _logger.LogInformation($"Item de pedido com ID: {id} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar item de pedido com ID: {id}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar item de pedido com ID: {id}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(Guid guid)
        {
            try
            {
                var orderItemToDelete = await _orderItemRepository.Get(guid);
                if (orderItemToDelete == null)
                {
                    return new Response<bool>().Failure(false, message: Error.ORDER_ITEM_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                var result = await _orderItemRepository.Delete(orderItemToDelete.UUID);
                if (result)
                {
                    _logger.LogInformation($"Item de pedido com UUID: {guid} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar item de pedido com UUID: {guid}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar item de pedido com UUID: {guid}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderItemDto>> Get(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.Get(id);
                if (orderItem == null)
                {
                    return new Response<OrderItemDto>().Failure(default, message: Error.ORDER_ITEM_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var orderItemDto = orderItem.Adapt<OrderItemDto>();
                return new Response<OrderItemDto>().Sucess(orderItemDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter item de pedido com ID: {id}.");
                return new Response<OrderItemDto>().Failure(default, message: "Erro ao obter item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderItemDto>>> GetAll()
        {
            try
            {
                var orderItems = await _orderItemRepository.GetAllWithProductAsync();
                var orderItemDtos = orderItems.Adapt<IEnumerable<OrderItemDto>>();
                return new Response<IEnumerable<OrderItemDto>>().Sucess(orderItemDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os itens de pedido.");
                return new Response<IEnumerable<OrderItemDto>>().Failure(default, message: "Erro ao obter todos os itens de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderItemDto>> GetDto(Guid guid)
        {
            try
            {
                var orderItem = await _orderItemRepository.Get(guid);
                if (orderItem == null)
                {
                    return new Response<OrderItemDto>().Failure(default, message: Error.ORDER_ITEM_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var orderItemDto = orderItem.Adapt<OrderItemDto>();
                return new Response<OrderItemDto>().Sucess(orderItemDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter item de pedido com UUID: {guid} como DTO.");
                return new Response<OrderItemDto>().Failure(default, message: "Erro ao obter item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<OrderItem>> GetEntity(Guid guid)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetOrderItemWithProduct(guid);
                if (orderItem == null)
                {
                    return new Response<OrderItem>().Failure(default, message: Error.ORDER_ITEM_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                return new Response<OrderItem>().Sucess(orderItem, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter item de pedido com UUID: {guid} como entidade.");
                return new Response<OrderItem>().Failure(default, message: "Erro ao obter item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Update(UpdateOrderItemDto updateOrderItemDto)
        {
            try
            {
                var existingOrderItem = await _orderItemRepository.GetOrderItemWithProduct(updateOrderItemDto.UUID);
                if (existingOrderItem == null)
                {
                    return new Response<bool>().Failure(false, message: Error.ORDER_ITEM_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                if (existingOrderItem.Product == null)
                {
                    return new Response<bool>().Failure(false, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                updateOrderItemDto.Adapt(existingOrderItem);
                existingOrderItem.UnitPrice = existingOrderItem.Product.Price; // Atualizar o preço se o produto mudar? Considere a lógica de precificação.
                existingOrderItem.TotalPrice = existingOrderItem.UnitPrice * existingOrderItem.Quantity;
                existingOrderItem.UpdateAt = DateTime.UtcNow;

                var result = await _orderItemRepository.Update(existingOrderItem);
                if (result)
                {
                    _logger.LogInformation($"Item de pedido com UUID: {updateOrderItemDto.UUID} atualizado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.UPDATE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao atualizar item de pedido com UUID: {updateOrderItemDto.UUID}.");
                    return new Response<bool>().Failure(false, message: "Falha ao atualizar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar atualizar item de pedido com UUID: {updateOrderItemDto.UUID}.");
                return new Response<bool>().Failure(false, message: "Erro ao atualizar item de pedido.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderItemDto>>> GetOrderItemsByOrderId(Guid orderId)
        {
            try
            {
                var orderItems = await _orderItemRepository.GetOrderItemsByOrderId(orderId);
                var orderItemDtos = orderItems.Adapt<IEnumerable<OrderItemDto>>();
                return new Response<IEnumerable<OrderItemDto>>().Sucess(orderItemDtos, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter itens de pedido para o pedido com UUID: {orderId}.");
                return new Response<IEnumerable<OrderItemDto>>().Failure(default, message: "Erro ao obter itens de pedido por OrderId.", statusCode: HttpStatusCode.InternalServerError);
            }
        }
    }
}