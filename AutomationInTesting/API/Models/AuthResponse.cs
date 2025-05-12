using System.Text.Json.Serialization;

namespace AutomationInTesting.API.Models;

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}