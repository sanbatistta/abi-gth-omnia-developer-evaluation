namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.Dtos
{
    public class SaleDetailDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid BranchId { get; set; }
        public string BranchDescription { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
        public List<SaleItemDetailDto> Items { get; set; }
    }
}
