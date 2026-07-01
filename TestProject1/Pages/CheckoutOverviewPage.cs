using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>Checkout step two — order overview (<c>/checkout-step-two.html</c>).</summary>
public class CheckoutOverviewPage : BasePage
{
    private readonly By _totalLabel = By.ClassName("summary_total_label");
    private readonly By _finishButton = By.Id("finish");

    public CheckoutOverviewPage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    public string GetTotalLabel() => GetText(_totalLabel);

    public CheckoutCompletePage Finish()
    {
        Click(_finishButton);
        return new CheckoutCompletePage(Driver, Wait);
    }
}
