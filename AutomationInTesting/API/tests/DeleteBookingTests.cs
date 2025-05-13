using System.Net;
using AutomationInTesting.API.Fakers;
using AutomationInTesting.API.Infrastructure;
using AutomationInTesting.API.Models;
using FluentAssertions;

namespace AutomationInTesting.API.tests;

public class DeleteBookingTests : TestBase
{
    [Test]
    public async Task DeleteBooking_GivenValidBookingIdAndToken_ShouldDeleteBooking()
    {
        // Arrange
        var tokenRequestBody = new
        {
            username = Username,
            password = Password
        };

        var tokenResponse = await PostAsync("auth", tokenRequestBody);
        tokenResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokenData = await DeserializeResponseAsync<AuthResponse>(tokenResponse);
        tokenData.Should().NotBeNull();
        tokenData!.Token.Should().NotBeNullOrEmpty();

        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        // Act
        var deleteResponse = await DeleteAsync($"booking/{createdBooking.BookingId}", tokenData.Token);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Test]
    public async Task DeleteBooking_GivenInvalidBookingId_ShouldReturnNotFound()
    {
        // Arrange
        var tokenRequestBody = new
        {
            username = Username,
            password = Password
        };

        var tokenResponse = await PostAsync("auth", tokenRequestBody);
        tokenResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var tokenData = await DeserializeResponseAsync<AuthResponse>(tokenResponse);
        tokenData.Should().NotBeNull();
        tokenData.Token.Should().NotBeNullOrEmpty();

        // Act
        var deleteResponse = await DeleteAsync("booking/9999", tokenData.Token);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
    }

    [Test]
    public async Task DeleteBooking_GivenMissingToken_ShouldReturnForbidden()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        // Act
        var deleteResponse = await DeleteAsync($"booking/{createdBooking.BookingId}", null);

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Test]
    public async Task DeleteBooking_GivenInvalidToken_ShouldReturnForbidden()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        // Act
        var deleteResponse = await DeleteAsync($"booking/{createdBooking.BookingId}", "invalid-token");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}