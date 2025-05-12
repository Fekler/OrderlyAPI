using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.UseCases
{
    public interface IDashboard
    {
        Task<Response<SalesSummaryDto>> GetSalesSummary();
        Task<Response<int>> GetPendingOrdersCount();
        Task<Response<List<ActiveCustomerDto>>> GetMostActiveCustomers();
    }
}
