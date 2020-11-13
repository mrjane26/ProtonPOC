using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ProtonPOC.Helpers;
using ProtonPOC.Specification;
using System;

namespace ProtonPOC.Page
{
    public class FoldersAndLabelsPage : Page
    {
        private readonly ProtonSpec proton;

        public FoldersAndLabelsPage(IWebDriver driver, ProtonSpec proton) : base(driver) => this.proton = proton;

        public IWebElement AddFolderButton => Driver.FindElement(By.XPath("//button[text()='Add folder']"));

        public IWebElement AddLabelButton => Driver.FindElement(By.XPath("//button[text()='Add label']"));

        private readonly By AccountNameBy = By.Id("accountName");
        public IWebElement AccountName => Driver.FindElement(AccountNameBy);

        public IWebElement Save => Driver.FindElement(By.XPath("//button[@type='submit']"));

        private By AddedFolderOrLabelBy(string name) => By.XPath($"//span[text()='{name}']");

        public IWebElement ActionsDropDown => Driver.FindElement(By.XPath("//button[@data-test-id='dropdown:open']"));

        private readonly By DeleteBy = By.XPath("//button[@data-test-id='folders/labels:item-delete']");
        public IWebElement DeleteAction => Driver.FindElement(DeleteBy);

        private readonly By DeletePopUpBy = By.XPath("//button[@type='submit' and contains(text(), 'Delete')]");
        public IWebElement DeletePopUp => Driver.FindElement(DeletePopUpBy);

        public FoldersAndLabelsPage VerifyFoldersAndLabelsIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(AddFolderButton.Displayed, "Buton Add folder is not displayed");
                Assert.IsTrue(AddLabelButton.Displayed, "Buton Add label is not displayed");
            });

            return this;
        }

        public FoldersAndLabelsPage AddFolder()
        {
            proton.FolderName = HelperMethods.RandomString(10);
            AddFolderButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(AccountNameBy).Displayed);

            AccountName.Click();
            AccountName.SendKeys(proton.FolderName);
            Save.Click();

            return this;
        }

        public FoldersAndLabelsPage VerifyFolderIsAdded()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(AddedFolderOrLabelBy(proton.FolderName)));

            Assert.IsTrue(Driver.FindElement(AddedFolderOrLabelBy(proton.FolderName)).Displayed, $"Folder with name [{proton.FolderName}] was not added");

            return this;
        }

        public FoldersAndLabelsPage AddLabel()
        {
            proton.LabelName = HelperMethods.RandomString(10);
            AddLabelButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(AccountNameBy).Displayed);

            AccountName.Click();
            AccountName.SendKeys(proton.LabelName);
            Save.Click();

            return this;
        }

        public FoldersAndLabelsPage VerifyLabelIsAdded()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(AddedFolderOrLabelBy(proton.LabelName)));

            Assert.IsTrue(Driver.FindElement(AddedFolderOrLabelBy(proton.LabelName)).Displayed, $"Folder with name [{proton.LabelName}] was not added");

            return this;
        }

        public FoldersAndLabelsPage RemoveFolderOrLabel()
        {
            ActionsDropDown.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(DeleteBy));
            DeleteAction.Click();

            wait.Until(c => c.FindElement(DeletePopUpBy));
            DeletePopUp.Click();

            return this;
        }
    }
}
