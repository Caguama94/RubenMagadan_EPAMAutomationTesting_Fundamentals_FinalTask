using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemo.Tests.Tests.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver;

            switch (browser.ToLower())
            {
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var ffOptions = new FirefoxOptions();
                    // Firefox a veces no respeta --start-maximized
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
    }
}
