using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Product Product { get; private set; } // Value Object
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

        public SaleItem(Guid id, Product product, int quantity, decimal unitPrice)
        {
            if (quantity < 1)
                throw new DomainException("Quantity must be at least 1.");

            if (quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");

            Id = id;
            Product = product ?? throw new DomainException("Product is required.");
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
