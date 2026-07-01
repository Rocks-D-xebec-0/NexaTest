using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TestProject1.Config;
using TestProject1.Drivers;
using TestProject1.Pages;
using TestProject1.Utils;

namespace TestProject1.Tests;

/// <summary>
/// Shared lifecycle for every test: fresh driver per test, navigate to base URL,
/// screenshot-on-failure attached to the Allure report, guaranteed teardown.
/// </summary>
[AllureNUnit]
public abstract class BaseTest
{
    protected IWebDriver Driver = null!;
    protected WaitHelper Wait = null!;
    protected TestSettings Settings = null!;

    /// <summary>Convenience factory for the entry page.</summary>
    protected LoginPage LoginPage => new(Driver, Wait);

    [SetUp]
    public void SetUp()
    {
        Settings = AppConfig.TestSettings;
        Driver = DriverFactory.Create(Settings);
        Wait = new WaitHelper(Driver, Settings.ExplicitWaitSeconds);
        Driver.Navigate().GoToUrl(Settings.BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                AllureApi.AddAttachment(
                    $"failure-{TestContext.CurrentContext.Test.Name}",
                    "image/png",
                    ScreenshotHelper.Capture(Driver),
                    ".png");
            }
        }
        finally
        {
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
