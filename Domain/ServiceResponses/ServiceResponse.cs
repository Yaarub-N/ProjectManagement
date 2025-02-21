﻿

namespace Domain.ServiceResponses;


public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }

    public ServiceResponse() { }

    public ServiceResponse(T data, bool success, string message = "")
    {
        Data = data;
        Success = success;
        Message = message;
    }
}
