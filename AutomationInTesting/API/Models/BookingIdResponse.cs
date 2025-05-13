using System.Text.Json.Serialization;

namespace AutomationInTesting.API.Models;

public class BookingIdResponse
{
    [JsonPropertyName("bookingid")]
    public int BookingId { get; set; }
}