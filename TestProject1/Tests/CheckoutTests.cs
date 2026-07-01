using Allure.NUnit.Attributes;
using TestProject1.Config;

namespace TestProject1.Tests;

[TestFixture]
[AllureSuite("Checkout")]
public class CheckoutTests : BaseTest
{
    // Scenario 5
    [Test]
    [AllureName("Full checkout happy path completes the order")]
    public void CheckoutHappyPath_CompletesOrder()
    {
        var inventory = LoginPage.Login(AppConfig.Users.Standard, AppConfig.Users.Password);
        inventory.AddProductToCart("sauce-labs-backpack");

        var confirmation = inventory
            .OpenCart()
            .Checkout()
            .EnterInformation("Jane", "Doe", "10001")
            .Finish();

        Assert.That(confirmation.GetConfirmationMessage(), Is.EqualTo("Thank you for your order!"));
    }
}
