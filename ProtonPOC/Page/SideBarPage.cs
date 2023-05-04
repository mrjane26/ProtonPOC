using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProtonPOC.Specification;
using System;

namespace ProtonPOC.Page
{
    public class SideBarPage : Page
    {
        private readonly ProtonSpec proton;

        public SideBarPage(IWebDriver driver, ProtonSpec proton) : base(driver) => this.proton = proton;

        private readonly By UsernameBy = By.XPath("//*[text()='User.ProtonTest']");
        public IWebElement BackToMailboxButton => Driver.FindElement(UsernameBy);

        private readonly By FoldersBy = By.XPath("//span[text()='Folders']");
        public IWebElement Folders => Driver.FindElement(FoldersBy);

        private readonly By LabelsBy = By.XPath("//span[text()='Folders']");
        public IWebElement Labels => Driver.FindElement(LabelsBy);

        public SideBarPage VerifyUserIsLoggedIn()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(UsernameBy));

            Assert.IsTrue(Driver.FindElements(UsernameBy).Count > 0, "The user is not logged in");

            return this;
        }

        public FoldersAndLabelsPage ClickFolders()
        {
            Folders.Click();

            return new FoldersAndLabelsPage(Driver, proton);
        }

        public FoldersAndLabelsPage ClickLabels()
        {
            Labels.Click();

            return new FoldersAndLabelsPage(Driver, proton);
        }
    }
}
