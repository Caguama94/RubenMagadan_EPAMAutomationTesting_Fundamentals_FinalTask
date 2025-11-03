using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;

            /// <summary>
            /// Espera explícita de 10 segundos.
            /// </summary>
            Wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(250));
        }

        /// <summary>
        /// Método para encontrar un elemento visible en la página.
        /// </summary>
        protected IWebElement FindVisible(By locator)
        {
            return Wait.Until(d =>
            {
                var el = d.FindElement(locator);
                return el.Displayed ? el : null;
            });
        }
    }
}

