namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.Dtos
{
    public class SaleListDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string BranchDescription { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
        public int ItemCount { get; set; }
    }
}
