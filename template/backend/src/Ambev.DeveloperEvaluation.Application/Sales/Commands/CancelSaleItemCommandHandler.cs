using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CancelSaleItemCommandHandler : ICommandHandler<CancelSaleItemCommand, Result>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSaleItemCommandHandler(ISaleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Result> Handle(CancelSaleItemCommand command, CancellationToken cancellationToken = default)
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
                    return Result.Failure("Cannot cancel items from a cancelled sale.");

                sale.RemoveItem(command.ItemId);

                await _repository.UpdateAsync(sale);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);


                return Result.Success();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result.Failure($"Failed to cancel sale item: {ex.Message}");
            }
        }
    }
}
