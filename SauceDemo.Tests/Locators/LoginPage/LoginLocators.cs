using OpenQA.Selenium;

namespace SauceDemo.Tests.Locators.LoginPage
{
    internal class LoginPageLocators
    {
        public static readonly By Username = By.XPath("//input[@id='user-name']");
        public static readonly By Password = By.XPath("//input[@id='password']");
        public static readonly By LoginButton = By.XPath("//input[@id='login-button']");
        public static readonly By ErrorMessage = By.XPath("//h3[@data-test='error']");
    }
}
