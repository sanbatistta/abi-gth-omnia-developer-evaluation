using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public static class CreateSaleCommandValidator
    {
        public static Result<CreateSaleCommand> Validate(CreateSaleRequest request)
        {
            if (request == null)
                return Result.Failure<CreateSaleCommand>("Invalid request.");

            if (string.IsNullOrWhiteSpace(request.SaleNumber))
                return Result.Failure<CreateSaleCommand>("Sale number is required.");

            if (request.CustomerId == Guid.Empty)
                return Result.Failure<CreateSaleCommand>("Customer ID is required.");

            if (string.IsNullOrWhiteSpace(request.CustomerName))
                return Result.Failure<CreateSaleCommand>("Customer name is required.");

            if (request.BranchId == Guid.Empty)
                return Result.Failure<CreateSaleCommand>("Branch ID is required.");

            if (string.IsNullOrWhiteSpace(request.BranchDescription))
                return Result.Failure<CreateSaleCommand>("Branch description is required.");

            if (request.Items == null || !request.Items.Any())
                return Result.Failure<CreateSaleCommand>("At least one item is required.");

            var validatedItems = new List<CreateSaleCommand.CreateSaleItemDto>();

            foreach (var item in request.Items)
            {
                if (item.ProductId == Guid.Empty)
                    return Result.Failure<CreateSaleCommand>("Product ID is required.");

                if (string.IsNullOrWhiteSpace(item.ProductName))
                    return Result.Failure<CreateSaleCommand>("Product name is required.");

                if (item.Quantity <= 0)
                    return Result.Failure<CreateSaleCommand>("Quantity must be positive.");

                if (item.Quantity > 20)
                    return Result.Failure<CreateSaleCommand>("Maximum 20 items per product.");

                if (item.UnitPrice <= 0)
                    return Result.Failure<CreateSaleCommand>("Unit price must be positive.");

                validatedItems.Add(new CreateSaleCommand.CreateSaleItemDto(
                    item.ProductId,
                    item.ProductName.Trim(),
                    item.Quantity,
                    item.UnitPrice
                ));
            }

            var command = new CreateSaleCommand(
                request.SaleNumber.Trim(),
                request.CustomerId,
                request.CustomerName.Trim(),
                request.BranchId,
                request.BranchDescription.Trim(),
                validatedItems
            );

            return Result.Success(command);
        }
    }
}
