using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages
{
    public class LoginPage : BasePage
    {
        // locators
        private readonly By Username = By.XPath("//input[@id='user-name']");
        private readonly By Password = By.XPath("//input[@id='password']");
        private readonly By LoginButton = By.XPath("//input[@id='login-button']");
        private readonly By ErrorMessage = By.XPath("//h3[@data-test='error']");

        //Constructor: recibe el driver y lo pasa a la clase base
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        //Methods
        //Ir a la pagina principal
        public void GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        //Escribir el usuario
        public void TypeUsername(string? value)
        {
            var el = FindVisible(Username);//Metodo del BasePage
            el.Clear();
            if (!string.IsNullOrEmpty(value))
                el.SendKeys(value);
        }

        //Escribir la contraseña
        public void TypePassword(string? value)
        {
            var el = FindVisible(Password);
            el.Clear();
            if (!string.IsNullOrEmpty(value))
                el.SendKeys(value);
        }

        //Hacer click en el boton de login
        public void ClickLogin()
        {
            FindVisible(LoginButton).Click();
        }

        // 🔹 Obtener mensaje de error (si existe)
        public string? ReadError()
        {
            try
            {
                return FindVisible(ErrorMessage).Text;
            }
            catch
            {
                return null; // si no hay error visible
            }
        }

        // 🔹 Esperar y validar el título del dashboard ("Swag Labs")
        public bool WaitTitleIsSwagLabs()
        {
            var wait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(250));
            return wait.Until(d => d.Title == "Swag Labs");
        }
    }
}
