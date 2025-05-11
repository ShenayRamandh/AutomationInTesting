using OpenQA.Selenium;

namespace AutomationInTesting.Infrastructure.Repository;

public class Repository
{
    private readonly IWebDriver _driver;
    public Reservation Reservation { get; }

    public Repository(IWebDriver driver)
    {
        _driver = driver;
        Reservation = new Reservation(driver);
    }

    public void NavigateToUrl(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }
}