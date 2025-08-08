namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CancelSaleItemCommand
    {
        public Guid SaleId { get; }
        public Guid ItemId { get; }

        public CancelSaleItemCommand(Guid saleId, Guid itemId)
        {
            if (saleId == Guid.Empty)
                throw new ArgumentException("Sale ID is required.", nameof(saleId));

            if (itemId == Guid.Empty)
                throw new ArgumentException("Item ID is required.", nameof(itemId));

            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
