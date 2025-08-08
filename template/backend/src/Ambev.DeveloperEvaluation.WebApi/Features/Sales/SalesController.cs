using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly CreateSaleCommandHandler _createSaleHandler;
        private readonly UpdateSaleCommandHandler _updateSaleHandler;
        private readonly CancelSaleCommandHandler _cancelSaleHandler;
        private readonly CancelSaleItemCommandHandler _cancelSaleItemHandler;
        private readonly GetSaleByIdHandler _getSaleByIdHandler;
        private readonly ListSalesHandler _listSalesHandler;

        public SalesController(
            CreateSaleCommandHandler createSaleHandler,
            UpdateSaleCommandHandler updateSaleHandler,
            CancelSaleCommandHandler cancelSaleHandler,
            CancelSaleItemCommandHandler cancelSaleItemHandler,
            GetSaleByIdHandler getSaleByIdHandler,
            ListSalesHandler listSalesHandler)
        {
            _createSaleHandler = createSaleHandler;
            _updateSaleHandler = updateSaleHandler;
            _cancelSaleHandler = cancelSaleHandler;
            _cancelSaleItemHandler = cancelSaleItemHandler;
            _getSaleByIdHandler = getSaleByIdHandler;
            _listSalesHandler = listSalesHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            var validationResult = CreateSaleCommandValidator.Validate(request);
            if (validationResult.IsFailure)
                return BadRequest(ApiResponse.Error(validationResult.Error));

            var result = await _createSaleHandler.Handle(validationResult.Value);
            
            if (result.IsFailure)
                return BadRequest(ApiResponse.Error(result.Error));

            return CreatedAtAction(
                nameof(GetSaleById), 
                new { id = result.Value }, 
                ApiResponseWithData.Success(result.Value, "Sale created successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            try
            {
                var result = await _getSaleByIdHandler.Handle(id);
                return Ok(ApiResponseWithData.Success(result));
            }
            catch (ApplicationException ex)
            {
                return NotFound(ApiResponse.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Error($"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var result = await _listSalesHandler.Handle();
                return Ok(ApiResponseWithData.Success(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse.Error($"Internal server error: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
        {
            var validationResult = UpdateSaleCommandValidator.Validate(id, request);
            if (validationResult.IsFailure)
                return BadRequest(ApiResponse.Error(validationResult.Error));

            var result = await _updateSaleHandler.Handle(validationResult.Value);
            
            if (result.IsFailure)
                return BadRequest(ApiResponse.Error(result.Error));

            return Ok(ApiResponse.CreateSuccess("Sale updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var command = new CancelSaleCommand(id);
            var result = await _cancelSaleHandler.Handle(command);
            
            if (result.IsFailure)
                return BadRequest(ApiResponse.Error(result.Error));

            return Ok(ApiResponse.CreateSuccess("Sale cancelled successfully"));
        }

        [HttpDelete("{saleId}/items/{itemId}")]
        public async Task<IActionResult> CancelSaleItem(Guid saleId, Guid itemId)
        {
            var command = new CancelSaleItemCommand(saleId, itemId);
            var result = await _cancelSaleItemHandler.Handle(command);
            
            if (result.IsFailure)
                return BadRequest(ApiResponse.Error(result.Error));

            return Ok(ApiResponse.CreateSuccess("Sale item cancelled successfully"));
        }
    }
}
