namespace CO4029_BE.API.Contracts.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    
    public static ApiResponse<T> Ok(T data, string message = "Thành công")
        => new() { Success = true, Data = data, Message = message, ErrorCode = null };
    
    public static ApiResponse<object?> Fail(string message, string? errorCode = null) 
        => new ApiResponse<object?>
            { Success = false, Data = null, Message = message, ErrorCode = errorCode };
}