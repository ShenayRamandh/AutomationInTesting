using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace AutomationInTesting.Infrastructure;

public class TestBase : IDisposable
{
    private readonly IWebDriver _driver;
    protected readonly string BaseUrl = "https://automationintesting.online/";
    protected readonly Repository.Repository Repository;

    protected TestBase()
    {
        _driver = new EdgeDriver();
        _driver.Manage().Window.Maximize();
        
        Repository = new Repository.Repository(_driver);
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}