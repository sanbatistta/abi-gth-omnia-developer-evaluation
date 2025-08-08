using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class UpdateSaleCommandHandler : ICommandHandler<UpdateSaleCommand, Result>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSaleCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(UpdateSaleCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                return Result.Failure("Command cannot be null.");

            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var sale = await _repository.GetByIdAsync(command.SaleId);
                if (sale == null)
                    return Result.Failure($"Sale with ID {command.SaleId} not found.");

                if (sale.Cancelled)
                    return Result.Failure("Cannot update a cancelled sale.");

                sale.ClearItems();

                foreach (var item in command.Items)
                {
                    sale.AddItem(new SaleItem(
                        Guid.NewGuid(),
                        new Product(item.ProductId, item.ProductName),
                        item.Quantity,
                        item.UnitPrice
                    ));
                }

                await _repository.UpdateAsync(sale);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);


                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result.Failure($"Failed to update sale: {ex.Message}");
            }
        }
    }
}
