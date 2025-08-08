namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CreateSaleCommand
    {
        public string SaleNumber { get; }
        public Guid CustomerId { get; }
        public string CustomerName { get; }
        public Guid BranchId { get; }
        public string BranchDescription { get; }
        public IReadOnlyList<CreateSaleItemDto> Items { get; }

        internal CreateSaleCommand(
            string saleNumber,
            Guid customerId,
            string customerName,
            Guid branchId,
            string branchDescription,
            IEnumerable<CreateSaleItemDto> items)
        {
            SaleNumber = saleNumber;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchDescription = branchDescription;
            Items = items.ToList().AsReadOnly();
        }

        public record CreateSaleItemDto(
            Guid ProductId,
            string ProductName,
            int Quantity,
            decimal UnitPrice);
    }
}
