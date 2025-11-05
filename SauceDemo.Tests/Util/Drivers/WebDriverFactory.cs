using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SauceDemo.Tests.Util.Drivers
{
    /// <summary>
    /// Clase de fábrica responsable de crear instancias de WebDriver configuradas para diferentes navegadores.
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Crea y configura una instancia de WebDriver para el navegador especificado.
        /// Navegadores compatibles: Chrome, Firefox y Edge.
        /// </summary>
        /// <param name="browser">Nombre del navegador (por ejemplo: "chrome", "firefox" o "edge").</param>
        /// <returns>Una instancia de <see cref="IWebDriver"/> configurada.</returns>

        public static IWebDriver CreateDriver(string browser)
        {
            IWebDriver driver;

            switch (browser.ToLower())
            {
                case "firefox":
                    var ffOptions = new FirefoxOptions();
                    ffOptions.AddArgument("--width=1920");
                    ffOptions.AddArgument("--height=1080");
                    driver = new FirefoxDriver(ffOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--start-maximized");
                    driver = new EdgeDriver(edgeOptions);
                    break;

                default:
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
