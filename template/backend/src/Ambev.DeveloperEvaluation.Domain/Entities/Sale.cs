using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.Total);
        public bool Cancelled { get; private set; }

        public void Cancel() => Cancelled = true;

        public void AddItem(SaleItem item)
        {
            if (item.Quantity > 20)
                throw new DomainException("Cannot sell more than 20 identical items.");
            Items.Add(item);
        }
    }
}
