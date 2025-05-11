using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationInTesting.Infrastructure.Repository;

public class Reservation(IWebDriver driver)
{
    public void ClickBookNowButton()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var bookNowButton = wait.Until(driverInstance =>
        {
            try
            {
                var element = driverInstance.FindElement(By.LinkText("Book now"));
                return element.Displayed && element.Enabled ? element : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", bookNowButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", bookNowButton);
    }
    
    public void ClickReserveButton()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var reserveNowButton = wait.Until(driverInstance =>
        {
            try
            {
                var element = driverInstance.FindElement(By.Id("doReservation"));
                return element is { Displayed: true, Enabled: true } ? element : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        if (reserveNowButton != null)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", reserveNowButton);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", reserveNowButton);
        }
        else
        {
            throw new Exception("Reserve button is not available within the given wait time.");
        }
    }
    
    public void ClickReserveNowButton()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var reserveNowButton = wait.Until(driverInstance =>
        {
            try
            {
                var element = driverInstance.FindElements(By.CssSelector("button.btn.btn-primary.w-100.mb-3"))
                    .FirstOrDefault(e => e.Text.Equals("Reserve Now", StringComparison.OrdinalIgnoreCase));

                return element is { Displayed: true, Enabled: true } ? element : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        if (reserveNowButton == null)
        {
            throw new Exception("Reserve Now button is not available within the given wait time.");
        }
    
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", reserveNowButton);
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", reserveNowButton);
    }
    
    public void EnterFirstName(string? text = null)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var firstnameField = wait.Until(driverInstance =>
        {
            var element = driverInstance.FindElement(By.Name("firstname"));
            return element is { Displayed: true, Enabled: true } ? element : null;
        });

        firstnameField.Clear();

        if (!string.IsNullOrEmpty(text))
        {
            firstnameField.SendKeys(text);
        }
    }
    
    public void EnterLastName(string? text = null)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    
        var lastnameField = wait.Until(driverInstance =>
        {
            var element = driverInstance.FindElement(By.Name("lastname"));
            return element is { Displayed: true, Enabled: true } ? element : null;
        });
    
        lastnameField.Clear();
    
        if (!string.IsNullOrEmpty(text))
        {
            lastnameField.SendKeys(text);
        }
    }
    
    public void EnterEmail(string? text = null)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    
        var emailField = wait.Until(driverInstance =>
        {
            var element = driverInstance.FindElement(By.Name("email"));
            return element is { Displayed: true, Enabled: true } ? element : null;
        });
    
        emailField.Clear();
    
        if (!string.IsNullOrEmpty(text))
        {
            emailField.SendKeys(text);
        }
    }
    
    public void EnterPhone(string? text = null)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var phoneField = wait.Until(driverInstance =>
        {
            var element = driverInstance.FindElement(By.Name("phone"));
            return element is { Displayed: true, Enabled: true } ? element : null;
        });

        phoneField.Clear();

        if (!string.IsNullOrEmpty(text))
        {
            phoneField.SendKeys(text);
        }
    }
    
    public IList<string> GetErrorMessages()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        var errorContainer = wait.Until(driverInstance =>
        {
            try
            {
                var element = driverInstance.FindElement(By.CssSelector("div.alert.alert-danger"));
                return element.Displayed ? element : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });

        if (errorContainer == null)
        {
            throw new Exception("Error container not found on the webpage.");
        }

        var errorMessages = errorContainer
            .FindElements(By.TagName("li"))
            .Select(e => e.Text)
            .ToList();

        return errorMessages;
    }

}