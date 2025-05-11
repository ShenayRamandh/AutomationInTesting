using AutomationInTesting.Infrastructure;
using NUnit.Framework;

namespace AutomationInTesting.tests;

[TestFixture]
public class ReservationTests : TestBase
{
    [Test]
    public void Reservation_GiveInvalidFirstName_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName();
        Repository.Reservation.EnterLastName("James");
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("1234567890");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
    }
    
    [Test]
    public void Reservation_GiveInvalidLastName_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName();
        Repository.Reservation.EnterEmail("James@gmail.com");
        Repository.Reservation.EnterPhone("1234567890");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
    }
    
    [Test]
    public void Reservation_GiveInvalidEmailAddress_ShouldThrowException()
    {
        // Arrange
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
        Repository.Reservation.ClickReserveButton();
        
        // Act
        Repository.Reservation.EnterFirstName("James");
        Repository.Reservation.EnterLastName("Jeff");
        Repository.Reservation.EnterEmail();
        Repository.Reservation.EnterPhone("1234567890");
        Repository.Reservation.ClickReserveNowButton();
        
        // Assert
    }
    
    [Test]
    public void Reservation_GiveInvalidPhoneNumber_ShouldThrowException()
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
    }
}