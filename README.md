# SauceDemo Selenium Suite

Automated UI tests for [SauceDemo](https://www.saucedemo.com) built with
**.NET 8 · NUnit · Selenium WebDriver 4 · Page Object Model · Allure**.

## Scenarios covered (7)

| # | Area           | Test                                            |
|---|----------------|-------------------------------------------------|
| 1 | Authentication | Valid login lands on the inventory page         |
| 2 | Authentication | Locked-out user sees the locked-out error       |
| 3 | Authentication | Invalid credentials show the mismatch error     |
| 4 | Cart           | Adding products updates the cart badge/contents |
| 5 | Checkout       | Full checkout happy path completes the order    |
| 6 | Inventory      | Sort by price low-to-high orders ascending      |
| 7 | Inventory      | Logout returns the user to the login page       |

## Design highlights

- **Page Object Model** — `Pages/` holds one class per page; locators are private
  `By` fields, tests speak only in intent-level actions.
- **Smart waits only** — `Utils/WaitHelper` wraps `WebDriverWait` with explicit
  polling conditions. Implicit waits are disabled and there is **no `Thread.Sleep`**.
- **Selenium Manager** — `Drivers/DriverFactory` lets Selenium 4 resolve the
  Chrome driver automatically; no committed driver binaries.
- **Config-driven** — `Config/appsettings.json` holds base URL, browser, headless
  flag, timeouts, and users. Any value is overridable via environment variables
  (e.g. `TestSettings__Headless=true`).
- **Allure reporting** — screenshots are captured on failure and attached to the
  report; results land in `bin/**/allure-results`.

## Project layout

```
TestProject1/
├─ Config/    TestSettings.cs, appsettings.json
├─ Drivers/   DriverFactory.cs
├─ Pages/     BasePage + Login/Inventory/Cart/Checkout page objects
├─ Tests/     BaseTest + Login/Cart/Checkout/Inventory fixtures
├─ Utils/     WaitHelper.cs, ScreenshotHelper.cs
└─ allureConfig.json
```

## Running

```bash
# All tests (headed Chrome by default)
dotnet test

# Headless (e.g. CI)
TestSettings__Headless=true dotnet test        # PowerShell: $env:TestSettings__Headless="true"

# A single area
dotnet test --filter "FullyQualifiedName~CheckoutTests"
```

## Viewing the Allure report

Requires the [Allure CLI](https://allurereport.org/docs/install/).

```bash
allure serve TestProject1/bin/Debug/net8.0/allure-results
```
