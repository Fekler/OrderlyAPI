using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.Dtos.Entities.OrderItem;
using SalesOrderManagement.Application.Interfaces.Business;
using System;
using System.Threading.Tasks;

namespace SalesOrderManagement.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrderItemsController(IOrderItemBusiness orderItemBusiness) : ControllerBase
    {
        private readonly IOrderItemBusiness _orderItemBusiness = orderItemBusiness;

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> CreateOrderItem(CreateOrderItemDto createOrderItemDto)
        {
            var response = await _orderItemBusiness.Add(createOrderItemDto);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var response = await _orderItemBusiness.Get(id);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("{guid:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOrderItemByGuid(Guid guid)
        {
            var response = await _orderItemBusiness.GetDto(guid);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("orders/{orderId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOrderItemsByOrderId(Guid orderId)
        {
            var response = await _orderItemBusiness.GetOrderItemsByOrderId(orderId);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpPut("{guid:guid}")]
        [Authorize] 
        public async Task<IActionResult> UpdateOrderItem(Guid guid, UpdateOrderItemDto updateOrderItemDto)
        {
            if (guid != updateOrderItemDto.UUID)
            {
                return BadRequest();
            }
            var response = await _orderItemBusiness.Update(updateOrderItemDto);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpDelete("{id:int}")]
        [Authorize] 
        public async Task<IActionResult> DeleteOrderItemById(int id)
        {
            var response = await _orderItemBusiness.Delete(id);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpDelete("{guid:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrderItemByGuid(Guid guid)
        {
            var response = await _orderItemBusiness.Delete(guid);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }
    }
}