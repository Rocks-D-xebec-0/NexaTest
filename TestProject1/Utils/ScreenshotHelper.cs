using OpenQA.Selenium;

namespace TestProject1.Utils;

/// <summary>Captures the current viewport as PNG bytes for report attachments.</summary>
public static class ScreenshotHelper
{
    public static byte[] Capture(IWebDriver driver) =>
        ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
}
