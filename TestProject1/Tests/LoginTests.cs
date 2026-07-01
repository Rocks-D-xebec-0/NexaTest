using Allure.NUnit.Attributes;
using TestProject1.Config;

namespace TestProject1.Tests;

[TestFixture]
[AllureSuite("Authentication")]
public class LoginTests : BaseTest
{
    // Scenario 1
    [Test]
    [AllureName("Valid login lands on the inventory page")]
    public void ValidLogin_LandsOnInventoryPage()
    {
        var inventory = LoginPage.Login(AppConfig.Users.Standard, AppConfig.Users.Password);

        Assert.Multiple(() =>
        {
            Assert.That(inventory.IsLoaded(), Is.True, "Inventory container should be visible.");
            Assert.That(inventory.GetTitle(), Is.EqualTo("Products"));
            Assert.That(Driver.Url, Does.Contain("inventory.html"));
        });
    }

    // Scenario 2
    [Test]
    [AllureName("Locked-out user sees the locked-out error")]
    public void LockedOutUser_ShowsError()
    {
        var login = LoginPage;
        login.SubmitCredentials(AppConfig.Users.LockedOut, AppConfig.Users.Password);

        Assert.Multiple(() =>
        {
            Assert.That(login.IsErrorDisplayed(), Is.True);
            Assert.That(login.GetErrorMessage(), Does.Contain("locked out"));
        });
    }

    // Scenario 3
    [Test]
    [AllureName("Invalid credentials show the mismatch error")]
    public void InvalidCredentials_ShowsError()
    {
        var login = LoginPage;
        login.SubmitCredentials(AppConfig.Users.Standard, "wrong_password");

        Assert.Multiple(() =>
        {
            Assert.That(login.IsErrorDisplayed(), Is.True);
            Assert.That(login.GetErrorMessage(), Does.Contain("Username and password do not match"));
        });
    }
}
