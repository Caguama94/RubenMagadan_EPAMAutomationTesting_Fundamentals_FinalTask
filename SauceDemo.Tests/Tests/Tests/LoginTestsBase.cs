using System;
using FluentAssertions;
using OpenQA.Selenium;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Tests.Drivers;
using Xunit;
using Xunit.Sdk;

namespace SauceDemo.Tests.Tests.Tests
{
    public abstract class LoginTestsBase : IDisposable
    {
        protected IWebDriver Driver { get; }
        protected LoginPage Page { get; }
        protected abstract string BrowserName { get; }

        protected LoginTestsBase()
        {
            Driver = WebDriverFactory.CreateDriver(BrowserName);
            Page = new LoginPage(Driver);
        }

        public void Dispose()
        {
            Driver?.Quit();
        }

        // UC-1 credenciales vacías 
        [Theory]
        [InlineData("abcde", "abcde", "Epic sadface: Username is required")]
        [InlineData("test", "test", "Epic sadface: Username is required")]
        [InlineData("user", "pass", "Epic sadface: Username is required")]
        [InlineData("demo", "demo", "Epic sadface: Username is required")]
        public void UC1_EmptyCredentials_ShowsUsernameRequired(string username, string password, string expectedMsg)
        {

            Page.GoTo();
            Page.TypeUsername(username);
            Page.ClearUsername();
            Page.TypePassword(password);
            Page.ClearPassword();
            Page.ClickLogin();

            var msg = Page.ReadError();
            Assert.Equal(expectedMsg, msg);
        }

        // UC-2 solo username 
        [Theory]
        [InlineData("abcde", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("usuario", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("test_user", "NotPassword", "Epic sadface: Password is required")]
        [InlineData("standard", "NotPassword", "Epic sadface: Password is required")]
        public void UC2_OnlyUsername_ShowsPasswordRequired(string username, string password, string expectedMsg)
        {
            Page.GoTo();

            Page.TypeUsername(username);
            Page.TypePassword(password);
            Page.ClearPassword();
            Page.ClickLogin();

            var msg = Page.ReadError();
            Assert.Equal(expectedMsg, msg);
        }

        // UC-3 credenciales válidas
        [Theory]
        [InlineData("standard_user", "secret_sauce", "Swag Labs")]
        [InlineData("problem_user", "secret_sauce", "Swag Labs")]
        [InlineData("performance_glitch_user", "secret_sauce", "Swag Labs")]
        [InlineData("visual_user", "secret_sauce", "Swag Labs")]
        public void UC3_ValidCredentials_ShowsDashboardTitle(string username, string password, string expectedMsg)
        {
            Page.GoTo();

            Page.TypeUsername(username);
            Page.TypePassword(password);
            Page.ClickLogin();

            //Page.WaitForTitle().Should().Be(expectedMsg);

            var title = Page.WaitForTitle();
            Assert.Equal(expectedMsg, title);
        }
    }
}
