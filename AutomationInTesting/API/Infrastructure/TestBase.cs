using System.Text;
using System.Text.Json;

namespace AutomationInTesting.API.Infrastructure;

public class TestBase
{
    private readonly HttpClient _httpClient;
    protected const string Username = "admin";
    protected const string Password = "password123";

    protected TestBase()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://restful-booker.herokuapp.com/")
        };
    }

    protected static StringContent CreateJsonContent(object data)
    {
        var json = JsonSerializer.Serialize(data);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    protected async Task<HttpResponseMessage> PostAsync(string endpoint, object requestBody)
    {
        var content = CreateJsonContent(requestBody);
        return await _httpClient.PostAsync(endpoint, content);
    }

    protected async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseBody);
    }
}