using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationInTesting.Infrastructure.Repository;

public class Reservation
{
    private readonly IWebDriver _driver;

    public Reservation(IWebDriver driver)
    {
        _driver = driver;
    }

    public void ClickBookNowButton()
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
      
        // Wait for the element to be present
        var bookNowButton = wait.Until(driverInstance => driverInstance.FindElement(By.LinkText("Book now")));
      
        // Scroll the element into view
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", bookNowButton);

        // Perform a JS click to bypass click interception
        ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", bookNowButton);
    }
}