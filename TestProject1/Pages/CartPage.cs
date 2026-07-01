using OpenQA.Selenium;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>The shopping cart (<c>/cart.html</c>).</summary>
public class CartPage : BasePage
{
    private readonly By _cartItems = By.ClassName("cart_item");
    private readonly By _itemNames = By.ClassName("inventory_item_name");
    private readonly By _checkoutButton = By.Id("checkout");

    public CartPage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    public int GetItemCount() => Driver.FindElements(_cartItems).Count;

    public IReadOnlyList<string> GetItemNames() =>
        Driver.FindElements(_itemNames).Select(e => e.Text).ToList();

    public CheckoutInformationPage Checkout()
    {
        Click(_checkoutButton);
        return new CheckoutInformationPage(Driver, Wait);
    }
}
