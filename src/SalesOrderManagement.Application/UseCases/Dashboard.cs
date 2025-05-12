using Microsoft.Extensions.Logging;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Application.Interfaces.UseCases;
using SalesOrderManagement.Domain.Entities._bases;
using SharedKernel.Utils;
using System.Net;
using static SalesOrderManagement.Domain.Entities._bases.Enums;

namespace SalesOrderManagement.Application.UseCases
{
    public class Dashboard(IOrderBusiness orderBusiness, IOrderItemBusiness orderItemBusiness, 
                                 ILogger<Dashboard> logger, IProductBusiness productBusiness, IUserBusiness userBusiness) : IDashboard
    {
        private readonly IOrderBusiness _orderBusiness = orderBusiness;
        private readonly IOrderItemBusiness _orderItemBusiness = orderItemBusiness;
        private readonly ILogger<Dashboard> _logger = logger;
        private readonly IProductBusiness _productBusiness = productBusiness;
        private readonly IUserBusiness _userBusiness = userBusiness; 

        public async Task<Response<DashboardReportDto>> GetDashboardReport()
        {
            try
            {
                var allOrdersResponse = await _orderBusiness.GetAllWithItemsAsync();
                if (!allOrdersResponse.ApiReponse.Success || allOrdersResponse.ApiReponse.Data == null)
                {
                    _logger.LogError("Erro ao obter todos os pedidos para o dashboard.");
                    return new Response<DashboardReportDto>().Failure(default, message: "Erro ao obter dados de pedidos.", statusCode: HttpStatusCode.InternalServerError);
                }
                var allOrders = allOrdersResponse.ApiReponse.Data.ToList();

                // Resumo de Vendas
                var totalOrders = allOrders.Count;
                var totalRevenue = allOrders.Sum(o => o.TotalAmount);
                var totalProductsSold = allOrders.Sum(o => o.OrderItems?.Sum(oi => oi.Quantity) ?? 0);

                var salesSummary = new SalesSummaryDto
                {
                    TotalOrders = totalOrders,
                    TotalRevenue = totalRevenue,
                    TotalProductsSold = totalProductsSold
                };

                // Pedidos Pendentes
                var pendingOrdersCount = allOrders.Count(o => o.Status == Enums.OrderStatus.Pending);

                // Clientes Mais Ativos (Exemplo básico - pode precisar de mais informações do cliente)
                var customerOrderCounts = allOrders
                    .GroupBy(o => o.CreateByUserUuid)
                    .Select(g => new { CustomerId = g.Key, OrderCount = g.Count() })
                    .OrderByDescending(c => c.OrderCount)
                    .Take(5); // Pegar os 5 clientes mais ativos

                var activeCustomers = new List<ActiveCustomerDto>();
                // Assuming you have a way to get customer names by their UUID (e.g., from a customer business)
                foreach (var customerData in customerOrderCounts)
                {
                    // This is a placeholder - replace with your actual logic to fetch customer names
                    var customerName = await _userBusiness.GetEntity(customerData.CustomerId);

                    activeCustomers.Add(new ActiveCustomerDto
                    {
                        CustomerId = customerData.CustomerId,
                        OrderCount = customerData.OrderCount,
                        CustomerName = customerName.ApiReponse.Data.FullName
                    });
                }

                var report = new DashboardReportDto
                {
                    SalesSummary = salesSummary,
                    PendingOrdersCount = pendingOrdersCount,
                    MostActiveCustomers = activeCustomers
                };

                return new Response<DashboardReportDto>().Sucess(report, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o relatório do dashboard.");
                return new Response<DashboardReportDto>().Failure(default, message: "Erro ao gerar o relatório do dashboard.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<SalesSummaryDto>> GetSalesSummary()
        {
            try
            {
                var allOrdersResponse = await _orderBusiness.GetAllWithItemsAsync();
                if (!allOrdersResponse.ApiReponse.Success || allOrdersResponse.ApiReponse.Data == null)
                {
                    _logger.LogError("Erro ao obter todos os pedidos para o resumo de vendas.");
                    return new Response<SalesSummaryDto>().Failure(default, message: "Erro ao obter dados de pedidos.", statusCode: HttpStatusCode.InternalServerError);
                }
                var allOrders = allOrdersResponse.ApiReponse.Data;

                var salesSummary = new SalesSummaryDto
                {
                    TotalOrders = allOrders.Count(),
                    TotalRevenue = allOrders.Sum(o => o.TotalAmount),
                    TotalProductsSold = allOrders.Sum(o => o.OrderItems?.Sum(oi => oi.Quantity) ?? 0)
                };

                return new Response<SalesSummaryDto>().Sucess(salesSummary, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar o resumo de vendas.");
                return new Response<SalesSummaryDto>().Failure(default, message: "Erro ao gerar o resumo de vendas.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<int>> GetPendingOrdersCount()
        {
            try
            {
                var allOrdersResponse = await _orderBusiness.GetAllWithItemsAsync();
                if (!allOrdersResponse.ApiReponse.Success || allOrdersResponse.ApiReponse.Data == null)
                {
                    _logger.LogError("Erro ao obter todos os pedidos para contar os pendentes.");
                    return new Response<int>().Failure(default, message: "Erro ao obter dados de pedidos.", statusCode: HttpStatusCode.InternalServerError);
                }
                var allOrders = allOrdersResponse.ApiReponse.Data;

                var pendingOrdersCount = allOrders.Count(o => o.Status == OrderStatus.Pending);

                return new Response<int>().Sucess(pendingOrdersCount, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a contagem de pedidos pendentes.");
                return new Response<int>().Failure(default, message: "Erro ao obter a contagem de pedidos pendentes.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<List<ActiveCustomerDto>>> GetMostActiveCustomers()
        {
            try
            {
                var allOrdersResponse = await _orderBusiness.GetAllWithItemsAsync();
                if (!allOrdersResponse.ApiReponse.Success || allOrdersResponse.ApiReponse.Data == null)
                {
                    _logger.LogError("Erro ao obter todos os pedidos para os clientes mais ativos.");
                    return new Response<List<ActiveCustomerDto>>().Failure(default, message: "Erro ao obter dados de pedidos.", statusCode: HttpStatusCode.InternalServerError);
                }
                var allOrders = allOrdersResponse.ApiReponse.Data;

                var customerOrderCounts = allOrders
                    .GroupBy(o => o.CreateByUserUuid)
                    .Select(g => new { CustomerId = g.Key, OrderCount = g.Count() })
                    .OrderByDescending(c => c.OrderCount)
                    .Take(5);

                var activeCustomers = new List<ActiveCustomerDto>();
                foreach (var customerData in customerOrderCounts)
                {
                    var userResponse = await _userBusiness.GetEntity(customerData.CustomerId);
                    if (userResponse.ApiReponse.Success && userResponse.ApiReponse.Data != null && userResponse.ApiReponse.Data.UserRole == UserRole.Client)
                    {
                        activeCustomers.Add(new ActiveCustomerDto
                        {
                            CustomerId = customerData.CustomerId,
                            OrderCount = customerData.OrderCount,
                            CustomerName = userResponse.ApiReponse.Data.FullName
                        });
                    }
                }

                return new Response<List<ActiveCustomerDto>>().Sucess(activeCustomers, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os clientes mais ativos.");
                return new Response<List<ActiveCustomerDto>>().Failure(default, message: "Erro ao obter os clientes mais ativos.", statusCode: HttpStatusCode.InternalServerError);
            }
        }
    }
}
