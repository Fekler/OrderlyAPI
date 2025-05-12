using Mapster;
using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Application.Interfaces.UseCases;
using SharedKernel.Utils;
using System.Net;

namespace SalesOrderManagement.Application.Dtos.Dashboard
{
    public class OrderProcessing : IOrderProcessing
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IOrderItemBusiness _orderItemBusiness;

        public OrderProcessing(IOrderBusiness orderBusiness, IProductBusiness productBusiness, IOrderItemBusiness orderItemBusiness)
        {
            _orderBusiness = orderBusiness;
            _productBusiness = productBusiness;
            _orderItemBusiness = orderItemBusiness;
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
    }
}