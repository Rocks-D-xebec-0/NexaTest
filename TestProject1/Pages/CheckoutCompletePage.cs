using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>Order confirmation (<c>/checkout-complete.html</c>).</summary>
public class CheckoutCompletePage : BasePage
{
    private readonly By _completeHeader = By.ClassName("complete-header");

    public CheckoutCompletePage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    public string GetConfirmationMessage() => GetText(_completeHeader);
}
