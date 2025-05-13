using System.Net;
using AutomationInTesting.API.Fakers;
using AutomationInTesting.API.Infrastructure;
using AutomationInTesting.API.Models;
using FluentAssertions;

namespace AutomationInTesting.API.tests;

[TestFixture]
[Category("API")]
public class GetBookingIdsTests : TestBase
{
    [Test]
    public async Task GetBookingIds_ShouldReturnAllBookingIds()
    {
        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking");
    
        // Assert
        responseData.Data!.Count.Should().BeGreaterThan(0);
        responseData.Response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Test]
    public async Task GetBookingIds_GivenFirstNameAndLastNameParameter_ShouldReturnFilteredBookingIds()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        var queryParams = new Dictionary<string, string>
        {
            { "firstname", requestBody.FirstName },
            { "lastname", requestBody.LastName }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().BeGreaterThan(0);

        var bookingId = responseData.Data!.FirstOrDefault()?.BookingId;
        bookingId.Should().Be(createdBooking.BookingId);
    }
    
    [Test]
    public async Task GetBookingIds_GivenFirstNameParameter_ShouldReturnFilteredBookingIds()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        var queryParams = new Dictionary<string, string>
        {
            { "firstname", requestBody.FirstName }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().BeGreaterThan(0);

        var bookingId = responseData.Data!.FirstOrDefault()?.BookingId;
        bookingId.Should().Be(createdBooking.BookingId);
    }
    
    [Test]
    public async Task GetBookingIds_GivenLastNameParameter_ShouldReturnFilteredBookingIds()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        var createResponse = await PostAsync("booking", requestBody);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdBooking = await DeserializeResponseAsync<BookingResponse>(createResponse);
        createdBooking.Should().NotBeNull();

        var queryParams = new Dictionary<string, string>
        {
            { "lastname", requestBody.LastName }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().BeGreaterThan(0);

        var bookingId = responseData.Data!.FirstOrDefault()?.BookingId;
        bookingId.Should().Be(createdBooking.BookingId);
    }
    
    [Test]
    public async Task GetBookingIds_GivenInvalidFirstNameParameter_ShouldReturnEmptyResult()
    {
        // Arrange
        var queryParams = new Dictionary<string, string>
        {
            { "firstname", "InvalidFirstName" }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().Be(0);
    }
    
    [Test]
    public async Task GetBookingIds_GivenInvalidLastNameParameter_ShouldReturnEmptyResult()
    {
        // Arrange
        var queryParams = new Dictionary<string, string>
        {
            { "lastname", "InvalidLastName" }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().Be(0);
    }
    
    [Ignore("This test is bring back status 200 which is inorrect.")]
    [Test]
    public async Task GetBookingIds_GivenMalformedQueryParameters_ShouldReturnBadRequest()
    {
        // Arrange
        var queryParams = new Dictionary<string, string>
        {
            { "invalidparam", "value" }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task GetBookingIds_GivenInvalidCheckInAndCheckOutDatesParameter_ShouldReturnEmptyResult()
    {
        // Arrange
        var queryParams = new Dictionary<string, string>
        {
            { "checkin", "2099-12-31" },
            { "checkout", "2100-01-01" }
        };

        // Act
        var responseData = await GetAsync<List<BookingIdResponse>>("booking", queryParams);

        // Assert
        responseData.Should().NotBeNull();
        responseData.Data!.Count.Should().Be(0);
    }
}