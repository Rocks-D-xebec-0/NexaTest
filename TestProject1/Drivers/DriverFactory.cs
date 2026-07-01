using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Config;

namespace TestProject1.Drivers;

/// <summary>
/// Builds a configured <see cref="IWebDriver"/>. Selenium 4's built-in Selenium
/// Manager resolves the matching chromedriver automatically — no manual driver
/// binaries to maintain.
/// </summary>
public static class DriverFactory
{
    public static IWebDriver Create(TestSettings settings) =>
        settings.Browser.ToLowerInvariant() switch
        {
            "chrome" => CreateChrome(settings),
            _ => throw new NotSupportedException($"Browser '{settings.Browser}' is not supported.")
        };

    private static IWebDriver CreateChrome(TestSettings settings)
    {
        var options = new ChromeOptions();
        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
        }

        options.AddArgument("--window-size=1920,1080");
        options.AddArgument("--disable-notifications");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-dev-shm-usage");
        // SauceDemo triggers Chrome's password-breach popups; disable them so they
        // never steal focus from our elements.
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);

        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);
        // Deliberately zero: we rely exclusively on explicit smart waits and never
        // want implicit + explicit waits to compound.
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;

        if (!settings.Headless)
        {
            driver.Manage().Window.Maximize();
        }

        return driver;
    }
}
