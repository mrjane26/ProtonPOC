using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProtonPOC.Specification;
using System;
using System.Configuration;

namespace ProtonPOC.Page
{
    public class LoginPage : Page
    {
        private readonly string Username = ConfigurationManager.AppSettings.Get("Username");
        private readonly string Password = ConfigurationManager.AppSettings.Get("Password");
        private readonly string BaseUrl = ConfigurationManager.AppSettings.Get("UITestBaseURL");

        private readonly ProtonSpec proton;

        public LoginPage(IWebDriver driver, ProtonSpec proton) : base(driver) => this.proton = proton;

        private readonly By usernameBy = By.Id("login");
        public IWebElement User => Driver.FindElement(usernameBy);

        private readonly By passwordBy = By.Id("password");
        public IWebElement Pass => Driver.FindElement(passwordBy);

        public IWebElement SignIn => Driver.FindElement(By.XPath("//button[@type='submit']"));

        public LoginPage Navigate()
        {
            Driver.Navigate().GoToUrl(BaseUrl);

            return this;
        }

        public LoginPage EnterUsername()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(usernameBy).Displayed);

            User.Click();
            User.SendKeys(Username);

            return this;
        }

        public LoginPage EnterPassword()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(passwordBy).Displayed);

            Pass.Click();
            Pass.SendKeys(Password);

            return this;
        }

        public SideBarPage Login()
        {
            SignIn.Click();

            return new SideBarPage(Driver, proton);
        }

        public SideBarPage LoginUser()
        {
            Navigate();
            EnterUsername();
            EnterPassword();
            Login();

            return new SideBarPage(Driver, proton);
        }
    }
}
