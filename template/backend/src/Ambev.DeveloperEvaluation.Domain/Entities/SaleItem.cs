using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal Discount
        {
            get
            {
                if (Quantity >= 10 && Quantity <= 20) return 0.20m;
                if (Quantity >= 4) return 0.10m;
                return 0.0m;
            }
        }

        public decimal Total => Quantity * UnitPrice * (1 - Discount);

        private SaleItem() { }

        public SaleItem(Guid id, Product product, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new DomainException("Quantity must be positive.");

            if (quantity > 20)
                throw new DomainException("Maximum 20 items per product.");

            if (unitPrice <= 0)
                throw new DomainException("Unit price must be positive.");

            Id = id;
            Product = product ?? throw new DomainException("Product is required.");
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
