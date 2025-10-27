using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Tests.Locators
{
    internal class LoginPageLocators
    {
        // locators
        public static readonly By Username = By.XPath("//input[@id='user-name']");
        public static readonly By Password = By.XPath("//input[@id='password']");
        public static readonly By LoginButton = By.XPath("//input[@id='login-button']");
        public static readonly By ErrorMessage = By.XPath("//h3[@data-test='error']");
    }
}
