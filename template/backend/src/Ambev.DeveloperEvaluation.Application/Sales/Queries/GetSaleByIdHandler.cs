using Ambev.DeveloperEvaluation.Application.Sales.Queries.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public class GetSaleByIdHandler
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaleDetailDto> Handle(Guid saleId)
        {
            var sale = await _repository.GetByIdAsync(saleId)
                ?? throw new ApplicationException("Sale not found.");

            return new SaleDetailDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                CustomerId = sale.Customer.Id,
                CustomerName = sale.Customer.Name,
                BranchId = sale.Branch.Id,
                BranchDescription = sale.Branch.Description,
                Total = sale.Total,
                Cancelled = sale.Cancelled,
                Items = sale.Items.Select(item => new SaleItemDetailDto
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    Total = item.Total
                }).ToList()
            };
        }
    }
}
