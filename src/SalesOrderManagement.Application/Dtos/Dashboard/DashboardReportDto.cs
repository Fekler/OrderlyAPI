public class DashboardReportDto
{
    public SalesSummaryDto SalesSummary { get; set; }
    public int PendingOrdersCount { get; set; }
    public List<ActiveCustomerDto> MostActiveCustomers { get; set; }
}
