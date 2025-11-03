using OpenQA.Selenium;
using SauceDemo.Tests.Locators.LoginPage;

namespace SauceDemo.Tests.Pages
{
    public class LoginPage : BasePage
    {



        /// <summary>
        /// Constructor: recibe el driver y lo pasa a la clase base.
        /// </summary>
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Ir a la página principal.
        /// </summary>
        public void GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        /// <summary>
        /// Escribir el usuario.
        /// </summary>
        public void TypeUsername(string? value)
        {
            var element = FindVisible(LoginPageLocators.Username);//Metodo del BasePage
            element.Clear();
            if (!string.IsNullOrEmpty(value))
                element.SendKeys(value);
        }

        /// <summary>
        /// Escribir la contraseña.
        /// </summary>
        public void TypePassword(string? value)
        {
            var element = FindVisible(LoginPageLocators.Password);
            element.Clear();
            if (!string.IsNullOrEmpty(value))
                element.SendKeys(value);
        }

        /// <summary>
        /// Limpiar inputs del campo de usuario.
        /// </summary>
        public void ClearUsername()
        {
            var element = FindVisible(LoginPageLocators.Username);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        /// <summary>
        /// Limpiar inputs del campo de contraseña.
        /// </summary>
        public void ClearPassword()
        {
            var element = FindVisible(LoginPageLocators.Password);
            element.Click();
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        /// <summary>
        /// Hacer clic en el botón de inicio de sesión.
        /// </summary>
        public void ClickLogin()
        {
            FindVisible(LoginPageLocators.LoginButton).Click();
        }

        /// <summary>
        /// Obtener el mensaje de error (si existe).
        /// </summary>
        public string? ReadError()
        {
            try
            {
                return FindVisible(LoginPageLocators.ErrorMessage).Text;
            }
            catch
            {
                return null; // si no hay error visible
            }
        }
    }
}
