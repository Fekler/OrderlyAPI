using Mapster;
using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Application.Interfaces.UseCases;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using SharedKernel.Utils;
using System.Net;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.UseCases
{
    public class OrderProcessing : IOrderProcessing
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IOrderItemBusiness _orderItemBusiness;
        private readonly IUserBusiness _userBusiness;

        public OrderProcessing(IOrderBusiness orderBusiness, IProductBusiness productBusiness, IOrderItemBusiness orderItemBusiness, IUserBusiness userBusiness)
        {
            _orderBusiness = orderBusiness;
            _productBusiness = productBusiness;
            _orderItemBusiness = orderItemBusiness;
            _userBusiness = userBusiness;
        }

        public async Task<Response<Guid>> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var orderResult = await _orderBusiness.Add(createOrderDto);
                if (!orderResult.ApiReponse.Success)
                {
                    return new Response<Guid>().Failure(default, message: orderResult.ApiReponse.Message, statusCode: orderResult.StatusCode);
                }
                var orderUuid = orderResult.ApiReponse.Data;
                var orderEntityResult = await _orderBusiness.GetEntity(orderUuid);
                if (!orderEntityResult.ApiReponse.Success || orderEntityResult.ApiReponse.Data == null)
                {
                    return new Response<Guid>().Failure(default, message: "Erro ao recuperar a entidade do pedido.", statusCode: HttpStatusCode.InternalServerError);
                }
                var orderEntity = orderEntityResult.ApiReponse.Data;
                await _orderBusiness.Update(orderEntity.Adapt<UpdateOrderDto>());

                decimal totalOrderAmount = 0;
                var createdOrderItemsUuids = new List<Guid>();


                foreach (var itemDto in createOrderDto.OrderItems)
                {
                    var productResult = await _productBusiness.GetEntity(itemDto.ProductId);
                    if (!productResult.ApiReponse.Success || productResult.ApiReponse.Data == null)
                    {
                        return new Response<Guid>().Failure(default, message: $"Produto com UUID: {itemDto.ProductId} não encontrado.", statusCode: HttpStatusCode.NotFound);
                    }
                    var product = productResult.ApiReponse.Data;

                    if(!itemDto.OrderId.HasValue || itemDto.OrderId.Value != orderUuid)
                    {
                        itemDto.OrderId = orderUuid;
                    }
                    var createOrderItemResult = await _orderItemBusiness.Add(itemDto);
                    if (!createOrderItemResult.ApiReponse.Success)
                    {

                        return new Response<Guid>().Failure(default, message: $"Erro ao adicionar item ao pedido: {createOrderItemResult.ApiReponse.Message}", statusCode: createOrderItemResult.StatusCode);
                    }
                    var orderItemUuid = createOrderItemResult.ApiReponse.Data;
                    var orderItemEntityResult = await _orderItemBusiness.GetEntity(orderItemUuid);
                    if (!orderItemEntityResult.ApiReponse.Success || orderItemEntityResult.ApiReponse.Data == null)
                    {
                        return new Response<Guid>().Failure(default, message: "Erro ao recuperar a entidade do item do pedido.", statusCode: HttpStatusCode.InternalServerError);
                    }
                    var orderItemEntity = orderItemEntityResult.ApiReponse.Data;
                    orderItemEntity.OrderId = orderEntity.UUID;
                    orderItemEntity.UnitPrice = product.Price;
                    orderItemEntity.TotalPrice = orderItemEntity.UnitPrice * orderItemEntity.Quantity;
                    await _orderItemBusiness.Update(orderItemEntity.Adapt<UpdateOrderItemDto>());

                    totalOrderAmount += orderItemEntity.TotalPrice;
                    createdOrderItemsUuids.Add(orderItemUuid);
                }

                orderEntity.TotalAmount = totalOrderAmount;
                await _orderBusiness.Update(orderEntity.Adapt<UpdateOrderDto>());

                return new Response<Guid>().Sucess(orderUuid, message: "Pedido criado com sucesso.", statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return new Response<Guid>().Failure(default, message: $"Erro ao processar a criação do pedido: {ex.Message}", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetAllByLoggedUser(Guid userUuid)
        {

            try
            {
                var user = await _userBusiness.GetEntity(userUuid);
                if (!user.ApiReponse.Success || user.ApiReponse.Data == null)
                {
                    return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Usuário não encontrado.", statusCode: HttpStatusCode.NotFound);
                }

                switch (user.ApiReponse.Data.UserRole)
                {
                    case UserRole.Admin:
                        return await _orderBusiness.GetAll();
                    case UserRole.Seller:
                        return await _orderBusiness.GetAll();
                    case UserRole.Client:
                        return await _orderBusiness.GetOrdersByUserId(userUuid);
                    default:
                        return new Response<IEnumerable<OrderDto>>().Failure(default, message: "Função de usuário inválida.", statusCode: HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<bool>> ActionOrder(Guid orderUuid, Guid userUuid, OrderStatus orderStatus)
        {
            try
            {
                var orderResult = await _orderBusiness.GetEntity(orderUuid);
                if (!orderResult.ApiReponse.Success || orderResult.ApiReponse.Data == null)
                {
                    return new Response<bool>().Failure(false, message: "Pedido não encontrado.", statusCode: HttpStatusCode.NotFound);
                }
                var order = orderResult.ApiReponse.Data;
                var userResult = await _userBusiness.GetEntity(userUuid);
                if (!userResult.ApiReponse.Success || userResult.ApiReponse.Data == null)
                {
                    return new Response<bool>().Failure(false, message: "Usuário não encontrado.", statusCode: HttpStatusCode.NotFound);
                }
                var user = userResult.ApiReponse.Data;
                if (user.UserRole != UserRole.Admin && user.UserRole != UserRole.Seller)
                {
                    return new Response<bool>().Failure(false, message: "Apenas administradores e vendedores podem alterar o status do pedido.", statusCode: HttpStatusCode.Forbidden);
                }
                order.Status = orderStatus;
                order.ActionedByUserUuid = user.UUID;
                order.ActionedAt = DateTime.UtcNow;
                await _orderBusiness.Update(order.Adapt<UpdateOrderDto>());
                return new Response<bool>().Sucess(true, message: "Status do pedido atualizado com sucesso.", statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new Response<bool>().Failure(false, message: $"Erro ao processar a ação no pedido: {ex.Message}", statusCode: HttpStatusCode.InternalServerError);
            }
        }
    }
    
}