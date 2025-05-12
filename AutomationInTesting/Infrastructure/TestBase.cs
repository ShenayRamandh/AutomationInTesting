using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationInTesting.Infrastructure;

public class TestBase : IDisposable
{
    private readonly IWebDriver _driver;
    protected readonly string BaseUrl = "https://automationintesting.online/";
    protected readonly Repository.Repository Repository;

    protected TestBase()
    {
        var options = new ChromeOptions();
        
        if (Environment.GetEnvironmentVariable("CI") != null)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            
            var userDataDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            options.AddArgument($"--user-data-dir={userDataDir}");
        }

        _driver = new ChromeDriver(options);
        _driver.Manage().Window.Maximize();
        
        Repository = new Repository.Repository(_driver);
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}