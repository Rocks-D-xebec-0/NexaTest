using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject1.Utils;

namespace TestProject1.Pages;

/// <summary>The products listing shown after login (<c>/inventory.html</c>).</summary>
public class InventoryPage : BasePage
{
    private readonly By _inventoryContainer = By.Id("inventory_container");
    private readonly By _title = By.ClassName("title");
    private readonly By _cartBadge = By.ClassName("shopping_cart_badge");
    private readonly By _cartLink = By.ClassName("shopping_cart_link");
    private readonly By _sortDropdown = By.ClassName("product_sort_container");
    private readonly By _itemPrices = By.ClassName("inventory_item_price");
    private readonly By _burgerMenu = By.Id("react-burger-menu-btn");
    private readonly By _logoutLink = By.Id("logout_sidebar_link");

    public InventoryPage(IWebDriver driver, WaitHelper wait) : base(driver, wait) { }

    public bool IsLoaded() => IsVisible(_inventoryContainer);

    public string GetTitle() => GetText(_title);

    /// <param name="productName">The data-test slug, e.g. <c>sauce-labs-backpack</c>.</param>
    public void AddProductToCart(string productName)
    {
        Click(By.CssSelector($"[data-test='add-to-cart-{productName}']"));
        // Wait for the button to toggle to "Remove" — proof the add registered in
        // React state before the next action reads the cart badge.
        Wait.WaitUntilVisible(By.CssSelector($"[data-test='remove-{productName}']"));
    }

    /// <summary>Cart badge count, or 0 when the badge is absent.</summary>
    public int GetCartItemCount() =>
        IsVisible(_cartBadge) ? int.Parse(GetText(_cartBadge)) : 0;

    public CartPage OpenCart()
    {
        Click(_cartLink);
        return new CartPage(Driver, Wait);
    }

    public void SortBy(string visibleText)
    {
        var dropdown = new SelectElement(Wait.WaitUntilVisible(_sortDropdown));
        dropdown.SelectByText(visibleText);
    }

    public IReadOnlyList<decimal> GetProductPrices()
    {
        Wait.WaitUntilVisible(_itemPrices);
        return Driver.FindElements(_itemPrices)
            .Select(e => decimal.Parse(e.Text.Replace("$", string.Empty), CultureInfo.InvariantCulture))
            .ToList();
    }

    public LoginPage Logout()
    {
        Click(_burgerMenu);
        Click(_logoutLink);
        return new LoginPage(Driver, Wait);
    }
}
