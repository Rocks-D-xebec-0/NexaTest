using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>Checkout step one — customer information (<c>/checkout-step-one.html</c>).</summary>
public class CheckoutInformationPage : BasePage
{
    private readonly By _firstName = By.Id("first-name");
    private readonly By _lastName = By.Id("last-name");
    private readonly By _postalCode = By.Id("postal-code");
    private readonly By _continueButton = By.Id("continue");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");

    public CheckoutInformationPage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    public CheckoutOverviewPage EnterInformation(string firstName, string lastName, string postalCode)
    {
        Type(_firstName, firstName);
        Type(_lastName, lastName);
        Type(_postalCode, postalCode);
        Click(_continueButton);
        return new CheckoutOverviewPage(Driver, Wait);
    }

    /// <summary>Clicks Continue without filling fields — used for validation tests.</summary>
    public void ContinueExpectingError() => Click(_continueButton);

    public string GetErrorMessage() => GetText(_errorMessage);
}
