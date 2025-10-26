using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using SauceDemo.Tests.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace SauceDemo.Tests.Tests.Tests
{
    public class LoginTests
    {
        // Factory local para crear el driver según el navegador
        private static IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver;

            switch (browser.ToLower())
            {
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var ffOptions = new FirefoxOptions();
                    // Tamaño ventana (Firefox no tiene --start-maximized estable)
                    ffOptions.AddArgument("--width=1920");
                    ffOptions.AddArgument("--height=1080");
                    driver = new FirefoxDriver(ffOptions);
                    break;

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--start-maximized");
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default: // chrome
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    driver = new ChromeDriver(chromeOptions);
                    break;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            return driver;
        }

        // UC-1 credenciales vacías 
        [Theory]
        [InlineData("chrome")]
        [InlineData("firefox")]
        [InlineData("edge")]
        public void UC1_EmptyCredentials_ShowsUsernameRequired(string browser)
        {
            IWebDriver? driver = null;

            try
            {
                driver = CreateDriver(browser);
                var page = new LoginPage(driver);
                page.GoTo();

                // Escribir y limpiar para asegurar campos vacíos
                page.TypeUsername("abcde"); 
                page.ClearUsername();
                page.TypePassword("abcde");
                page.ClearPassword();
                page.ClickLogin();

                var msg = page.ReadError();
                msg.Should().NotBeNull();
                msg!.Should().Contain("Username is required");
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Excepción en el test ({browser}): {ex}");
            }
            finally
            {
                driver?.Quit();
            }
        }

        // UC-2 solo username 
        [Theory]
        [InlineData("chrome")]
        [InlineData("firefox")]
        [InlineData("edge")]
        public void UC2_OnlyUsername_ShowsPasswordRequired(string browser)
        {
            IWebDriver? driver = null;

            try
            {
                driver = CreateDriver(browser);
                var page = new LoginPage(driver);
                page.GoTo();

                page.TypeUsername("abcde");
                page.TypePassword("abcde"); 
                page.ClearPassword();
                page.ClickLogin();

                var msg = page.ReadError();
                msg.Should().NotBeNull();
                msg!.Should().Contain("Password is required");
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Excepción en el test ({browser}): {ex}");
            }
            finally
            {
                driver?.Quit();
            }
        }

        // UC-3 credenciales válidas
        [Theory]
        [InlineData("chrome")]
        [InlineData("firefox")]
        [InlineData("edge")]
        public void UC3_ValidCredentials_ShowsDashboardTitle(string browser)
        {
            IWebDriver? driver = null;

            try
            {
                driver = CreateDriver(browser);
                var page = new LoginPage(driver);
                page.GoTo();

                page.TypeUsername("standard_user");
                page.TypePassword("secret_sauce");
                page.ClickLogin();

                page.WaitTitleIsSwagLabs().Should().BeTrue();
            }
            catch (Exception ex)
            {
                Assert.False(true, $"Excepción en el test ({browser}): {ex}");
            }
            finally
            {
                driver?.Quit();
            }
        }
    }
}
