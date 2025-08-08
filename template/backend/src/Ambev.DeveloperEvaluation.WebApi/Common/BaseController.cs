using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(T result)
        {
            if (result == null)
                return NotFound(ApiResponse.Error("Resource not found"));

            return Ok(ApiResponseWithData.Success(result));
        }

        protected IActionResult HandleException(Exception ex)
        {
            return StatusCode(500, ApiResponse.Error($"Internal server error: {ex.Message}"));
        }
    }
}
