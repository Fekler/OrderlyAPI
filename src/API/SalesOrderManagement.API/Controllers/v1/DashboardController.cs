using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.Interfaces.UseCases;

namespace SalesOrderManagement.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DashboardController(IDashboard dashboardUseCase) : ControllerBase
    {
        private readonly IDashboard _dashboardUseCase = dashboardUseCase;
        [HttpGet("sales-summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSalesSummary()
        {
            var response = await _dashboardUseCase.GetSalesSummary();
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("pending-orders-count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingOrdersCount()
        {
            var response = await _dashboardUseCase.GetPendingOrdersCount();
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("most-active-customers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMostActiveCustomers()
        {
            var response = await _dashboardUseCase.GetMostActiveCustomers();
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }
    }
}