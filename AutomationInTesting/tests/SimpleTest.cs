using AutomationInTesting.Infrastructure;
using NUnit.Framework;

namespace AutomationInTesting.tests;

[TestFixture]
public class SimpleTest : TestBase
{
    [Test]
    public void ShouldLaunchAutomationTestingHomePage()
    {
        Repository.NavigateToUrl(BaseUrl);
        Repository.Reservation.ClickBookNowButton();
    }
}