using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Locators.InventoryPage;
using OpenQA.Selenium;
namespace SauceDemo.Tests.Pages
{
    internal class InventoryPage : BasePage
    {
        public InventoryPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Esperar y validar el título.
        /// </summary>
        public string WaitForAppLogo()
        {
            var wait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(250));
            /// <summary>
            /// Espera hasta que el logo “Swag Labs” sea visible en la página Inventory.
            /// </summary>

            var element = wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(InventoryLocators.AppLogo);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            return element.Text;
        }
    }
}


