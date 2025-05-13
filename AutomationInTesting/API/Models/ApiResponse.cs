namespace AutomationInTesting.API.Models;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public HttpResponseMessage Response { get; set; }

    public ApiResponse(T? data, HttpResponseMessage response)
    {
        Data = data;
        Response = response;
    }
}