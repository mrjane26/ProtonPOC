using NUnit.Framework;
using OpenQA.Selenium;
using ProtonPOC.Specification;

namespace ProtonPOC.Page
{
    public class SideBarPage : Page
    {
        private readonly ProtonSpec proton;

        public SideBarPage(IWebDriver driver, ProtonSpec proton) : base(driver) => this.proton = proton;

        private readonly By BackToMailboxBy = By.XPath("//a[text()='Back to Mailbox']");
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

            return new FoldersAndLabelsPage(Driver, proton);
        }
    }
}
