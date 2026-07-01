# SauceDemo Selenium Suite — Design

**Date:** 2026-07-01
**Stack:** .NET 8 · NUnit · Selenium WebDriver 4 · Page Object Model · Allure

## Goal

Automate 7 core scenarios of https://www.saucedemo.com using .NET + Selenium,
following best practices: Page Object Model, explicit "smart" waits (no
`Thread.Sleep`), config-driven setup, and rich Allure reporting.

## Scenarios (core 7)

1. **Valid login** — `standard_user` logs in and lands on the inventory page.
2. **Locked-out user** — `locked_out_user` sees the correct error message.
3. **Invalid credentials** — wrong password shows the correct error message.
4. **Add to cart** — adding products updates the cart badge count correctly.
5. **Checkout happy path** — complete purchase shows "Thank you for your order!".
6. **Sort products** — sort price low→high; verify ascending price order.
7. **Logout** — logout via the burger menu returns to the login page.

## Architecture

Extends the existing `TestProject1` NUnit project.

```
TestProject1/
├─ Config/       TestSettings.cs, appsettings.json (baseUrl, browser, headless, timeouts, users)
├─ Drivers/      DriverFactory.cs (Selenium Manager, Chrome, headless toggle)
├─ Pages/        BasePage, LoginPage, InventoryPage, CartPage,
│                CheckoutInformationPage, CheckoutOverviewPage, CheckoutCompletePage
├─ Tests/        BaseTest, LoginTests, CartTests, CheckoutTests, InventoryTests
├─ Utils/        WaitHelper.cs, ScreenshotHelper.cs
└─ allureConfig.json
```

## Key decisions

- **Page Object Model** — each page is a class exposing intent-level actions
  (e.g. `LoginPage.Login(user, pass)`). Locators are private `By` fields.
- **Smart waits** — a central `WaitHelper` wraps `WebDriverWait` with conditions
  (element visible / clickable / URL contains / text present). Implicit waits are
  **disabled** so wait strategies are never mixed. No `Thread.Sleep` anywhere.
- **DriverFactory** — Selenium 4 built-in Selenium Manager auto-resolves the
  Chrome driver. Headless is config-toggled (`headless: false` default).
- **BaseTest** — `[SetUp]` builds the driver and navigates to `baseUrl`;
  `[TearDown]` captures a screenshot on failure, attaches it to Allure, then
  quits the driver. Driver is held per-instance for parallel safety.
- **Allure** — `Allure.NUnit` adapter with steps, screenshots-on-failure, and
  environment info. `allure serve bin/.../allure-results` renders the report.
- **Config-driven** — no hardcoded URLs/credentials in tests; `appsettings.json`
  with optional environment-variable overrides.

## Packages to add

- `Selenium.WebDriver` (4.x)
- `Allure.NUnit`
- `Microsoft.Extensions.Configuration.Json` + `.Binder`

## Non-goals (YAGNI)

- Cross-browser matrix (Chrome only for now).
- Parallel execution tuning beyond per-instance driver isolation.
- CI pipeline config (structure is CI-ready but no pipeline file yet).
