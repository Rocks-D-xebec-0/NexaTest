using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>
/// Base for all page objects. Provides wait-backed primitives so no page ever
/// touches raw <c>FindElement</c>/<c>Click</c> without an explicit wait.
/// </summary>
public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WaitHelper Wait;

    protected BasePage(IWebDriver driver, WaitHelper wait)
    {
        Driver = driver;
        Wait = wait;
    }

    protected void Click(By locator) => Wait.WaitUntilClickable(locator).Click();

    protected void Type(By locator, string text)
    {
        var element = Wait.WaitUntilVisible(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected string GetText(By locator) => Wait.WaitUntilVisible(locator).Text;

    protected bool IsVisible(By locator)
    {
        try
        {
            return Wait.WaitUntilVisible(locator).Displayed;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}
