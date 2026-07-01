using Allure.NUnit.Attributes;
using TestProject1.Config;

namespace TestProject1.Tests;

[TestFixture]
[AllureSuite("Cart")]
public class CartTests : BaseTest
{
    // Scenario 4
    [Test]
    [AllureName("Adding products updates the cart badge and cart contents")]
    public void AddProductsToCart_UpdatesBadgeAndContents()
    {
        var inventory = LoginPage.Login(AppConfig.Users.Standard, AppConfig.Users.Password);
        inventory.AddProductToCart("sauce-labs-backpack");
        inventory.AddProductToCart("sauce-labs-bike-light");

        Assert.That(inventory.GetCartItemCount(), Is.EqualTo(2), "Cart badge should show 2 items.");

        var cart = inventory.OpenCart();
        Assert.Multiple(() =>
        {
            Assert.That(cart.GetItemCount(), Is.EqualTo(2));
            Assert.That(cart.GetItemNames(), Does.Contain("Sauce Labs Backpack"));
            Assert.That(cart.GetItemNames(), Does.Contain("Sauce Labs Bike Light"));
        });
    }
}
