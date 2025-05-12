using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationInTesting.UI.Infrastructure;

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

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
        var screenshotDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Screenshots");
        Directory.CreateDirectory(screenshotDirectory);
            
        var fileName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(screenshotDirectory, fileName);
        screenshot.SaveAsFile(filePath);
        TestContext.AddTestAttachment(filePath);
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}