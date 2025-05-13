using System.Text.Json.Serialization;

namespace AutomationInTesting.API.Models;

public class BookingResponse
{
    [JsonPropertyName("bookingid")]
    public int BookingId { get; set; }

    [JsonPropertyName("booking")]
    public BookingRequest Booking { get; set; } = new();
}