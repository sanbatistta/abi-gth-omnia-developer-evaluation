namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse CreateSuccess(string message = "Operation completed successfully")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResponse Error(string message)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message
            };
        }
    }

    public class ApiResponseWithData<T> : ApiResponse
    {
        public T? Data { get; set; }

        public static ApiResponseWithData<T> CreateSuccess(T data, string message = "Operation completed successfully")
        {
            return new ApiResponseWithData<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static new ApiResponseWithData<T> Error(string message)
        {
            return new ApiResponseWithData<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }

    public static class ApiResponseWithData
    {
        public static ApiResponseWithData<T> Success<T>(T data, string message = "Operation completed successfully")
        {
            return ApiResponseWithData<T>.CreateSuccess(data, message);
        }
    }
}
