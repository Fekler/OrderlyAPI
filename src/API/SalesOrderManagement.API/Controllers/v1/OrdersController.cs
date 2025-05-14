using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.Dtos.Entities.Order;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Application.UseCases;
using SalesOrderManagement.Domain.Errors;
using System.Security.Claims;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrdersController(IOrderBusiness orderBusiness, OrderProcessing orderProcessing) : ControllerBase
    {
        private readonly IOrderBusiness _orderBusiness = orderBusiness;
        private readonly OrderProcessing _orderProcessing = orderProcessing;

        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var createdByUserUuid))
            {
                return Unauthorized(Error.UNAUTHORIZED);
            }
            createOrderRequest.CreateByUserUuid = createdByUserUuid;
            var response = await _orderProcessing.CreateOrder(createOrderRequest);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("{uuid}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(Guid uuid)
        {
            var response = await _orderBusiness.GetOrderWithOrdemItems(uuid);

            return StatusCode((int)response.StatusCode, response.ApiReponse);

        }

        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetOrdersByLoggedUserId()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(Error.UNAUTHORIZED);
            }
            var response = await _orderBusiness.GetOrdersByUserId(userId);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> GetOrdersByStatus(string status)
        {
            var response = await _orderBusiness.GetOrdersByStatus(status);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }


        [HttpGet("date-range")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetOrdersByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("A data de início deve ser anterior à data de fim.");
            }
            var response = await _orderBusiness.GetOrdersByDateRange(startDate, endDate);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("GetAllDetailed")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> GetAllOrdersWithItems()
        {
            var response = await _orderBusiness.GetAllWithItemsAsync();
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpPut("{uuid}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> UpdateOrder(Guid uuid, [FromBody] UpdateOrderDto updateOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (uuid != updateOrderDto.UUID)
            {
                return BadRequest("O UUID na rota não corresponde ao UUID no corpo da requisição.");
            }

            var response = await _orderBusiness.Update(updateOrderDto);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpDelete("{uuid}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> DeleteOrder(Guid uuid)
        {
            var response = await _orderBusiness.Delete(uuid);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }


        [HttpPost("manager-order")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> ManagerOrder(Guid orderUuid, OrderStatus status)
        {


            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userUuid))
            {
                return Unauthorized(Error.UNAUTHORIZED);
            }
            
            var response = await _orderProcessing.ActionOrder(orderUuid,userUuid, status);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }
    }
}