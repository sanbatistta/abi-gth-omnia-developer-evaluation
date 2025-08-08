using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CreateSaleCommandHandler : ICommandHandler<CreateSaleCommand, Result<Guid>>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result<Guid>> Handle(CreateSaleCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                return Result.Failure<Guid>("Command cannot be null.");

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var sale = new Sale(
                    Guid.NewGuid(),
                    command.SaleNumber,
                    DateTime.UtcNow,
                    new Customer(command.CustomerId, command.CustomerName),
                    new Branch(command.BranchId, command.BranchDescription)
                );

                foreach (var item in command.Items)
                {
                    sale.AddItem(new SaleItem(
                        Guid.NewGuid(),
                        new Product(item.ProductId, item.ProductName),
                        item.Quantity,
                        item.UnitPrice
                    ));
                }

                await _repository.AddAsync(sale);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);


                return Result.Success(sale.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result.Failure<Guid>($"Failed to create sale: {ex.Message}");
            }
        }
    }
}
