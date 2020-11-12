using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPOC.Page
{
    public class SideBarPage : Page
    {
        public SideBarPage(IWebDriver driver) : base(driver)
        {
        }

        private readonly By BackToMailboxBy = By.XPath("//*[@data-test-id='sidebar:compose']");
        public IWebElement BackToMailboxButton => Driver.FindElement(BackToMailboxBy);

        private readonly By FoldersAndLabelsBy = By.XPath("//span[text()='Folders & labels']");
        public IWebElement FoldersAndLabels => Driver.FindElement(FoldersAndLabelsBy);

        public SideBarPage VerifyUserIsLoggedIn()
        {
            Assert.IsTrue(Driver.FindElement(BackToMailboxBy).Displayed, "The user is not logged in");

            return this;
        }

        public FoldersAndLabelsPage ClickFoldersAndLabels()
        {
            FoldersAndLabels.Click();

            return new FoldersAndLabelsPage(Driver);
        }
    }
}
