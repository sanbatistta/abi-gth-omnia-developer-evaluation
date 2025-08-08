using static Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSaleCommand;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class UpdateSaleCommand
    {
        public Guid SaleId { get; }
        public IReadOnlyList<CreateSaleItemDto> Items { get; }

        public UpdateSaleCommand(Guid saleId, IEnumerable<CreateSaleItemDto> items)
        {
            if (saleId == Guid.Empty)
                throw new ArgumentException("Sale ID is required.", nameof(saleId));

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var itemsList = items.ToList();
            if (itemsList.Count == 0)
                throw new ArgumentException("Sale must have at least one item.", nameof(items));

            SaleId = saleId;
            Items = itemsList.AsReadOnly();
        }
    }
}
