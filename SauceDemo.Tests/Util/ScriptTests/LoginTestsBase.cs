using System;
using OpenQA.Selenium;
using SauceDemo.Tests.Logging;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Tests.Drivers;
using Serilog;
using Xunit;

namespace SauceDemo.Tests.Util.ScriptTests
{
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

        // UC-1 credenciales vacías 
        [Theory]
        [InlineData("abcde", "abcde", "Epic sadface: Username is required")]
        [InlineData("test", "test", "Epic sadface: Username is required")]
        [InlineData("user", "pass", "Epic sadface: Username is required")]
        [InlineData("demo", "demo", "Epic sadface: Username is required")]
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

        // UC-2 solo username 
        [Theory]
        [InlineData("abcde", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("usuario", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("test_user", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("standard", "NotPassword", "Epic sadface: Password is required")]
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

        // UC-3 credenciales válidas
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

            //Page.WaitForTitle().Should().Be(expectedMsg);

            var title = Page.WaitForTitle();
            Assert.Equal(expectedMsg, title);

            Log.Information("UC3 end ({Browser}) OK", BrowserName);
        }
    }
}
