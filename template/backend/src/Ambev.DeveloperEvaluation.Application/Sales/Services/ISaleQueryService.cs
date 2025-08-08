using Ambev.DeveloperEvaluation.Application.Sales.Queries.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Sales.Services
{
    public interface ISaleQueryService
    {
        Task<PagedResult<SaleListDto>> GetSalesAsync(SaleFilter filter, PagingOptions paging);
        Task<List<SaleListDto>> GetByCustomerIdAsync(Guid customerId, int limit = 50);
        Task<List<SaleListDto>> GetByBranchIdAsync(Guid branchId, int limit = 50);
        Task<List<SaleListDto>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, int limit = 100);
        Task<SalesStatistics> GetSalesStatisticsAsync(DateTime startDate, DateTime endDate);
    }

    public class SaleFilter
    {
        public Guid? CustomerId { get; set; }
        public Guid? BranchId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Cancelled { get; set; }
        public string SaleNumber { get; set; }
    }

    public class PagingOptions
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; } = "Date";
        public bool SortDescending { get; set; } = true;

        public int Skip => (Page - 1) * PageSize;
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => Page < TotalPages;
        public bool HasPreviousPage => Page > 1;
    }

    public class SalesStatistics
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int TotalItemsSold { get; set; }
        public Dictionary<Guid, int> TopProducts { get; set; } = new();
        public Dictionary<Guid, decimal> RevenueByBranch { get; set; } = new();
    }
}
