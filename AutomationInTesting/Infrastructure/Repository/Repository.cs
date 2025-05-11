using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationInTesting.Infrastructure.Repository;

public class Repository(IWebDriver driver)
{
    public void NavigateToUrl(string url)
    {
        driver.Navigate().GoToUrl(url);
    }
    
    public void ClickBookNowButton()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
  
        // Wait for the element to be present
        var bookNowButton = wait.Until(driverInstance => driverInstance.FindElement(By.LinkText("Book now")));
  
        // Scroll the element into view
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", bookNowButton);

        // Perform a JS click to bypass click interception
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bookNowButton);
    }

}