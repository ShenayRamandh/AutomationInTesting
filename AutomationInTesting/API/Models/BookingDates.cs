using System.Text.Json.Serialization;

namespace AutomationInTesting.API.Models;

public class BookingDates
{
    [JsonPropertyName("checkin")]
    public DateTime? CheckIn { get; set; }

    [JsonPropertyName("checkout")]
    public DateTime? CheckOut { get; set; }
}