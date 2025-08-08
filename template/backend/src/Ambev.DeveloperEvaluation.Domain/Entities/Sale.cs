using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        private readonly List<SaleItem> _items = new();

        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; } = string.Empty;
        public DateTime Date { get; private set; }
        public Customer Customer { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
        public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();
        public bool Cancelled { get; private set; }

        public decimal Total => _items.Sum(i => i.Total);

        private Sale() { }

        public Sale(
            Guid id,
            string saleNumber,
            DateTime date,
            Customer customer,
            Branch branch)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new DomainException("Sale number is required.");

            Id = id;
            SaleNumber = saleNumber;
            Date = date;
            Customer = customer ?? throw new DomainException("Customer is required.");
            Branch = branch ?? throw new DomainException("Branch is required.");
            Cancelled = false;
        }

        public void AddItem(SaleItem item)
        {
            if (item == null)
                throw new DomainException("Sale item is required.");

            if (item.Quantity > 20)
                throw new DomainException("Maximum 20 items per product.");

            if (Cancelled)
                throw new DomainException("Cannot modify cancelled sale.");

            _items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            if (Cancelled)
                throw new DomainException("Cannot modify cancelled sale.");

            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new DomainException("Item not found.");

            _items.Remove(item);
        }

        public void ClearItems()
        {
            if (Cancelled)
                throw new DomainException("Cannot modify cancelled sale.");

            _items.Clear();
        }

        public void Cancel()
        {
            if (Cancelled)
                throw new DomainException("Sale already cancelled.");

            Cancelled = true;
        }
    }
}
