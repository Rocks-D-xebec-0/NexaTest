using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>The SauceDemo login screen (<c>/</c>).</summary>
public class LoginPage : BasePage
{
    private readonly By _username = By.Id("user-name");
    private readonly By _password = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");
    private readonly By _errorMessage = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    /// <summary>Logs in and returns the inventory page for a successful attempt.</summary>
    public InventoryPage Login(string username, string password)
    {
        SubmitCredentials(username, password);
        return new InventoryPage(Driver, Wait);
    }

    /// <summary>Submits credentials without navigating away (used for error cases).</summary>
    public void SubmitCredentials(string username, string password)
    {
        Type(_username, username);
        Type(_password, password);
        Click(_loginButton);
    }

    public string GetErrorMessage() => GetText(_errorMessage);

    public bool IsErrorDisplayed() => IsVisible(_errorMessage);

    public bool IsAtLoginPage() => IsVisible(_loginButton);
}
