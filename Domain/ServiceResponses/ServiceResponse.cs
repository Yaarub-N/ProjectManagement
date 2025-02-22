

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.ServiceResponses;


public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }

    public ServiceResponse(bool success, string message = "") {
   
        Success = success;
        Message = message;
    }

    public ServiceResponse(T data, bool success, string message = "")
    {
        Data = data;
        Success = success;
        Message = message;
    }
}
