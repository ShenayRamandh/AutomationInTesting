using System.Text;
using System.Text.Json;
using AutomationInTesting.API.Models;

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

    // Come back and refactor this method to use the new JsonSerializerOptions
    protected async Task<HttpResponseMessage> PostAsync(string endpoint, object requestBody)
    {
        var content = CreateJsonContent(requestBody);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));
        return await _httpClient.PostAsync(endpoint, content);
    }
    
    protected async Task<ApiResponse<T>> GetAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null)
    {
        var url = endpoint;

        if (queryParams != null && queryParams.Count != 0)
        {
            var queryString = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
            url = $"{endpoint}?{queryString}";
        }

        var response = await _httpClient.GetAsync(url);
        var data = await DeserializeResponseAsync<T>(response);

        return new ApiResponse<T>(data, response);
    }

    protected async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseBody);
    }
}