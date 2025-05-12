using AutomationInTesting.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace AutomationInTesting.tests;

[TestFixture]
public class ReservationTests : TestBase
{
    [Test]
    public void Reservation_GiveNullFirstName_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName();
        Repository.Reservation.EnterLastName("James");
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "size must be between 3 and 18",
            "Firstname should not be blank"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [TestCase("j")]
    [TestCase("jjjjjjjjjjjjjjjjjjj")]
    public void Reservation_GiveInvalidFirstName_ShouldThrowException(string firstName)
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName(firstName);
        Repository.Reservation.EnterLastName("James");
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "size must be between 3 and 18"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [Test]
    public void Reservation_GiveNullLastName_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName();
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "Lastname should not be blank",
            "size must be between 3 and 30"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [TestCase("j")]
    [TestCase("qwertyuiopasdfghjklzxcvbnmqwert")]
    public void Reservation_GiveInvalidLastName_ShouldThrowException(string lastName)
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName(lastName);
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "size must be between 3 and 30"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [Test]
    public void Reservation_GiveNullEmailAddress_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName("Jeff");
        Repository.Reservation.EnterEmail();
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "must not be empty"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [Ignore("To fix flakey tests")]
    [TestCase("James.Jeff@")]
    [TestCase("James.Jeff@Gmail")]
    [TestCase("James.Jeff@Gmail.")]
    public void Reservation_GiveInvalidEmailAddress_ShouldThrowException(string emailAddress)
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName("Jeff");
        Repository.Reservation.EnterEmail(emailAddress);
        Repository.Reservation.EnterPhone("12345678901");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "must be a well-formed email address"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [Test]
    public void Reservation_GiveNullPhoneNumber_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName("Jeff");
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone();
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "must not be empty",
            "size must be between 11 and 21"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
    
    [TestCase("1234567890")]
    [TestCase("1234567890123456789012")]
    public void Reservation_GiveInvalidPhoneNumber_ShouldThrowException(string phoneNumber)
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName("Jeff");
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone(phoneNumber);
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
        var errorMessage = Repository.Reservation.GetErrorMessages();
        var expectedMessages = new[]
        {
            "size must be between 11 and 21"
        };

        errorMessage.Should().BeEquivalentTo(expectedMessages, "The error messages should exactly match the expected values.");
    }
}