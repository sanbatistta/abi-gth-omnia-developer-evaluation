using Ambev.DeveloperEvaluation.Application.Sales.Queries.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public class ListSalesHandler
    {
        private readonly ISaleRepository _repository;

        public ListSalesHandler(ISaleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<SaleListDto>> Handle()
        {
            var sales = await _repository.GetAllAsync();

            return sales.Select(sale => new SaleListDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                CustomerName = sale.Customer.Name,
                BranchDescription = sale.Branch.Description,
                Total = sale.Total,
                Cancelled = sale.Cancelled,
                ItemCount = sale.Items.Count
            }).ToList();
        }
    }
}
