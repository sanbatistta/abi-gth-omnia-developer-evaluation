namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CancelSaleCommand
    {
        public Guid SaleId { get; }

        public CancelSaleCommand(Guid saleId)
        {
            if (saleId == Guid.Empty)
                throw new ArgumentException("Sale ID is required.", nameof(saleId));

            SaleId = saleId;
        }
    }
}
