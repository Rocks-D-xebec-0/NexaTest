using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestProject1.Utils;

/// <summary>
/// Central "smart wait" helper. Every interaction goes through an explicit
/// <see cref="WebDriverWait"/> with a polling condition — no <c>Thread.Sleep</c>
/// and no reliance on implicit waits.
/// </summary>
public class WaitHelper
{
    private readonly WebDriverWait _wait;

    public WaitHelper(IWebDriver driver, int timeoutSeconds)
    {
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds))
        {
            PollingInterval = TimeSpan.FromMilliseconds(250)
        };
        _wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
    }

    /// <summary>Waits until the element exists and is displayed, then returns it.</summary>
    public IWebElement WaitUntilVisible(By locator) =>
        _wait.Until(d =>
        {
            var el = d.FindElement(locator);
            return el.Displayed ? el : null;
        })!;

    /// <summary>Waits until the element is displayed and enabled (i.e. clickable).</summary>
    public IWebElement WaitUntilClickable(By locator) =>
        _wait.Until(d =>
        {
            var el = d.FindElement(locator);
            return el is { Displayed: true, Enabled: true } ? el : null;
        })!;

    /// <summary>Waits until the current URL contains the given fragment.</summary>
    public bool WaitUntilUrlContains(string fragment) =>
        _wait.Until(d => d.Url.Contains(fragment, StringComparison.OrdinalIgnoreCase));
}
