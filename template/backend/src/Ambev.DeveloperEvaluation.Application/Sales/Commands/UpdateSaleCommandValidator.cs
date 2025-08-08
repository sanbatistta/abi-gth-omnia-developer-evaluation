using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public static class UpdateSaleCommandValidator
    {
        public static Result<UpdateSaleCommand> Validate(Guid saleId, UpdateSaleRequest request)
        {
            if (saleId == Guid.Empty)
                return Result.Failure<UpdateSaleCommand>("Sale ID is required.");

            if (request == null)
                return Result.Failure<UpdateSaleCommand>("Invalid request.");

            if (request.Items == null || !request.Items.Any())
                return Result.Failure<UpdateSaleCommand>("At least one item is required.");

            var validatedItems = new List<CreateSaleCommand.CreateSaleItemDto>();

            foreach (var item in request.Items)
            {
                if (item.ProductId == Guid.Empty)
                    return Result.Failure<UpdateSaleCommand>("Product ID is required.");

                if (string.IsNullOrWhiteSpace(item.ProductName))
                    return Result.Failure<UpdateSaleCommand>("Product name is required.");

                if (item.Quantity <= 0)
                    return Result.Failure<UpdateSaleCommand>("Quantity must be positive.");

                if (item.Quantity > 20)
                    return Result.Failure<UpdateSaleCommand>("Maximum 20 items per product.");

                if (item.UnitPrice <= 0)
                    return Result.Failure<UpdateSaleCommand>("Unit price must be positive.");

                validatedItems.Add(new CreateSaleCommand.CreateSaleItemDto(
                    item.ProductId,
                    item.ProductName.Trim(),
                    item.Quantity,
                    item.UnitPrice
                ));
            }

            var command = new UpdateSaleCommand(saleId, validatedItems);
            return Result.Success(command);
        }
    }
}
