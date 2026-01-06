using OpenQA.Selenium;
using SauceDemo.Tests.Helpers.Logging;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Util.Drivers;
using Serilog;

namespace SauceDemo.Tests.Util.Tests;


public abstract class LoginTestsBase : IDisposable
{
    protected IWebDriver Driver { get; }
    protected LoginPage Page { get; }
    protected abstract string BrowserName { get; }

    protected LoginTestsBase()
    {

        LoggerConfig.Init();
        Log.Information("Creating driver for {Browser}", BrowserName);

        Driver = WebDriverFactory.CreateDriver(BrowserName);
        Page = new LoginPage(Driver);
    }

    public void Dispose()
    {
        try
        {
            Log.Information("Disposing driver for {Browser}", BrowserName);
            Driver?.Quit();
            Log.Information("Driver disposed for {Browser}", BrowserName);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error disposing driver for {Browser}", BrowserName);
            throw;
        }


    }

    /// <summary>
    /// UC-1 credenciales vacías.
    /// Verifica que se muestre el mensaje “Epic sadface: Username and password do not match any user in this service”
    /// cuando los campos de usuario y contraseña quedan vacíos.
    /// </summary>
    [Theory]
    [InlineData("abcde", "abcde", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("test", "test", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("user", "pass", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("demo", "demo", "Epic sadface: Username and password do not match any user in this service")]
    public void UC1_EmptyCredentials_ShowsUsernameRequired(string username, string password, string expectedMsg)
    {
        Log.Information("UC1 start ({Browser}) with user='{User}' pass='{Pass}'", BrowserName, username, password);

        Page.GoTo();
        Page.TypeUsername(username);
        Page.ClearUsername();
        Page.TypePassword(password);
        Page.ClearPassword();
        Page.ClickLogin();

        var msg = Page.ReadError();
        Assert.Equal(expectedMsg, msg);

        Log.Information("UC1 end ({Browser}) OK", BrowserName);
    }

    /// <summary>
    /// UC-2 solo username.
    /// Verifica que se muestre el mensaje “Epic sadface: Username and password do not match any user in this service”
    /// cuando solo se introduce el nombre de usuario.
    /// </summary>
    [Theory]
    [InlineData("abcde", "NotPassword", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("usuario", "NotPassword", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("test_user", "NotPassword", "Epic sadface: Username and password do not match any user in this service")]
    [InlineData("standard", "NotPassword", "Epic sadface: Username and password do not match any user in this service")]
    public void UC2_OnlyUsername_ShowsPasswordRequired(string username, string password, string expectedMsg)
    {
        Log.Information("UC2 start ({Browser}) with user='{User}' pass='{Pass}'", BrowserName, username, password);

        Page.GoTo();

        Page.TypeUsername(username);
        Page.TypePassword(password);
        Page.ClearPassword();
        Page.ClickLogin();

        var msg = Page.ReadError();
        Assert.Equal(expectedMsg, msg);

        Log.Information("UC2 end ({Browser}) OK", BrowserName);
    }


    /// <summary>
    /// UC-3 credenciales válidas.
    /// Verifica que, al iniciar sesión con credenciales correctas, se muestre el título “Swag Labs”.
    /// </summary>
    [Theory]
    [InlineData("standard_user", "secret_sauce", "Swag Labs")]
    [InlineData("problem_user", "secret_sauce", "Swag Labs")]
    [InlineData("performance_glitch_user", "secret_sauce", "Swag Labs")]
    [InlineData("visual_user", "secret_sauce", "Swag Labs")]
    public void UC3_ValidCredentials_ShowsDashboardTitle(string username, string password, string expectedMsg)
    {
        Log.Information("UC3 start ({Browser}) with user='{User}'", BrowserName, username);

        Page.GoTo();

        Page.TypeUsername(username);
        Page.TypePassword(password);
        Page.ClickLogin();

        var inventory=new InventoryPage(Driver);
        var logoText = inventory.WaitForAppLogo();

        Assert.Equal(expectedMsg, logoText);

        Log.Information("UC3 end ({Browser}) OK", BrowserName);
    }
}
