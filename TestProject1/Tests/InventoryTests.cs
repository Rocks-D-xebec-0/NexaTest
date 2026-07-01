using Allure.NUnit.Attributes;
using TestProject1.Config;

namespace TestProject1.Tests;

[TestFixture]
[AllureSuite("Inventory")]
public class InventoryTests : BaseTest
{
    // Scenario 6
    [Test]
    [AllureName("Sorting by price low-to-high orders products ascending")]
    public void SortByPriceLowToHigh_OrdersAscending()
    {
        var inventory = LoginPage.Login(AppConfig.Users.Standard, AppConfig.Users.Password);
        inventory.SortBy("Price (low to high)");

        Assert.That(inventory.GetProductPrices(), Is.Ordered.Ascending);
    }

    // Scenario 7
    [Test]
    [AllureName("Logout returns the user to the login page")]
    public void Logout_ReturnsToLoginPage()
    {
        var inventory = LoginPage.Login(AppConfig.Users.Standard, AppConfig.Users.Password);
        var login = inventory.Logout();

        Assert.Multiple(() =>
        {
            Assert.That(login.IsAtLoginPage(), Is.True, "Login button should be visible again.");
            Assert.That(Driver.Url, Does.Not.Contain("inventory.html"));
        });
    }
}
