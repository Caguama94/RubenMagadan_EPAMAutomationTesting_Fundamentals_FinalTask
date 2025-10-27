using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Tests.Locators;

namespace SauceDemo.Tests.Pages
{
    public class LoginPage : BasePage
    {
        


        //Constructor: recibe el driver y lo pasa a la clase base
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        //Metodos
        //Ir a la pagina principal
        public void GoTo()
        {
            Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        //Escribir el usuario
        public void TypeUsername(string? value)
        {
            var el = FindVisible(LoginPageLocators.Username);//Metodo del BasePage
            el.Clear();
            if (!string.IsNullOrEmpty(value))
                el.SendKeys(value);
        }

        //Escribir la contraseña
        public void TypePassword(string? value)
        {
            var el = FindVisible(LoginPageLocators.Password);
            el.Clear();
            if (!string.IsNullOrEmpty(value))
                el.SendKeys(value);
        }

        //Limpiar imputs
        public void ClearUsername()
        {
            var el = FindVisible(LoginPageLocators.Username);
            el.Click();
            el.SendKeys(Keys.Control + "a");
            el.SendKeys(Keys.Delete);
        }

        public void ClearPassword()
        {
            var el = FindVisible(LoginPageLocators.Password);
            el.Click();
            el.SendKeys(Keys.Control + "a");
            el.SendKeys(Keys.Delete);
        }

        public void ClickLogin()
        {
            FindVisible(LoginPageLocators.LoginButton).Click();
        }

        //Obtener mensaje de error (si existe)
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

        //Esperar y validar el título del dashboard
        public string WaitForTitle()
        {
            var wait = new WebDriverWait(new SystemClock(), Driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(250));

            // Espera hasta que el título deje de estar vacío
            wait.Until(d => !string.IsNullOrEmpty(d.Title));

            return Driver.Title;
        }
    }
}
