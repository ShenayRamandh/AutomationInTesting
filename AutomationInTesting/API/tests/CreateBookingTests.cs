using System.Net;
using AutomationInTesting.API.Fakers;
using AutomationInTesting.API.Infrastructure;
using AutomationInTesting.API.Models;
using FluentAssertions;

namespace AutomationInTesting.API.tests;

[TestFixture]
[Category("API")]
public class CreateBookingTests : TestBase
{
    [Test]
    public async Task CreateBooking_GivenValidRequest_ShouldReturnBookingDetails()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseData = await DeserializeResponseAsync<BookingResponse>(response);

        responseData.Should().NotBeNull();
        responseData.BookingId.Should().BeGreaterThan(0);
        responseData.Booking.FirstName.Should().Be(requestBody.FirstName);
        responseData.Booking.LastName.Should().Be(requestBody.LastName);
        responseData.Booking.TotalPrice.Should().Be(requestBody.TotalPrice);
        responseData.Booking.DepositPaid.Should().Be(requestBody.DepositPaid);
        responseData.Booking.BookingDates.CheckIn = DateTime.SpecifyKind(responseData.Booking.BookingDates.CheckIn!.Value, DateTimeKind.Utc);
        responseData.Booking.BookingDates.CheckOut = DateTime.SpecifyKind(responseData.Booking.BookingDates.CheckOut!.Value, DateTimeKind.Utc);
        responseData.Booking.BookingDates.CheckIn?.ToUniversalTime().Date.Should().Be(requestBody.BookingDates.CheckIn?.ToUniversalTime().Date);
        responseData.Booking.BookingDates.CheckOut?.ToUniversalTime().Date.Should().Be(requestBody.BookingDates.CheckOut?.ToUniversalTime().Date);
        responseData.Booking.AdditionalNeeds.Should().Be(requestBody.AdditionalNeeds);
    }
    
    [Ignore("This test is suppose to fail as the firstName is an empty string but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidFirstName_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.FirstName = string.Empty;
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("firstname is required");
    }
    
    [Ignore("This test is suppose to fail as the lastName is an empty string but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidLastName_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.LastName = string.Empty;
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("lastname is required");
    }
    
    [Ignore("This test is suppose to fail as the totalPrice is an empty string but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidTotalPrice_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.TotalPrice = null;
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("totalprice is required");
    }
    
    [Ignore("This test is suppose to fail as the checkin is an empty string but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidCheckIn_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.BookingDates.CheckIn = null;
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("checkin is required");
    }
    
    [Ignore("This test is suppose to fail as the checkin is an empty string but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidCheckOut_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.BookingDates.CheckOut = null;
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("checkout is required");
    }
    
    [Ignore("This test is suppose to fail as the checkin date is after the checkout date but still creates the booking.")]
    [Test]
    public async Task CreateBooking_GivenInvalidCheckOutDate_ShouldThrowValidationError()
    {
        // Arrange
        var requestBody = new BookingRequestFaker().Generate();
        requestBody.BookingDates.CheckOut = requestBody.BookingDates.CheckIn!.Value.Date.AddDays(-1);
        
        // Act
        var response = await PostAsync("booking", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Contain("checkout is required");
    }
}