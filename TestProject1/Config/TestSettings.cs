using Microsoft.Extensions.Configuration;

namespace TestProject1.Config;

/// <summary>Runtime settings bound from <c>Config/appsettings.json</c>.</summary>
public class TestSettings
{
    public string BaseUrl { get; set; } = "https://www.saucedemo.com";
    public string Browser { get; set; } = "chrome";
    public bool Headless { get; set; }
    public int ExplicitWaitSeconds { get; set; } = 15;
    public int PageLoadTimeoutSeconds { get; set; } = 30;
}

/// <summary>Test user credentials (kept out of the test code itself).</summary>
public class UserSettings
{
    public string Standard { get; set; } = "standard_user";
    public string LockedOut { get; set; } = "locked_out_user";
    public string Password { get; set; } = "secret_sauce";
}

/// <summary>
/// Single entry point for configuration. Loaded once, environment variables win
/// so CI can override (e.g. <c>TestSettings__Headless=true</c>).
/// </summary>
public static class AppConfig
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(Path.Combine("Config", "appsettings.json"), optional: false, reloadOnChange: false)
        .AddEnvironmentVariables()
        .Build();

    public static TestSettings TestSettings { get; } =
        Configuration.GetSection("TestSettings").Get<TestSettings>() ?? new TestSettings();

    public static UserSettings Users { get; } =
        Configuration.GetSection("Users").Get<UserSettings>() ?? new UserSettings();
}
