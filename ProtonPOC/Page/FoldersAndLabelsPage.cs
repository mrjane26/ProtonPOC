using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

        public IWebElement AddFolderButton => Driver.FindElement(By.XPath("//button[@title='Create a new folder']"));

        public IWebElement AddLabelButton => Driver.FindElement(By.XPath("//button[@title='Create a new label']"));

        private readonly By FolderNameBy = By.Id("folder");
        public IWebElement FolderName => Driver.FindElement(FolderNameBy);

        private readonly By FoldersNameBy = By.XPath("//button[@title='Folders']");
        public IWebElement FoldersName => Driver.FindElement(FoldersNameBy);

        private readonly By LabelsNameBy = By.XPath("//button[@title='Labels']");
        public IWebElement LabelsName => Driver.FindElement(LabelsNameBy);

        public IWebElement Save => Driver.FindElement(By.XPath("//button[@data-testid='label-modal:save']"));

        private By AddedFolderBy(string name) => By.XPath($"//div[text()='{name}']");

        private By AddedLabelBy(string name) => By.XPath($"//span[text()='{name}']");

        private readonly By FolderActionsBy = By.XPath("//button[@data-testid='dropdown-button' and @title='Folder options']");
        public IWebElement FolderActions => Driver.FindElement(FolderActionsBy);

        private readonly By LabelActionsBy = By.XPath("//button[@data-testid='dropdown-button' and @title='Label options']");
        public IWebElement LabelActions => Driver.FindElement(LabelActionsBy);

        private readonly By DeleteFolderBy = By.XPath("//button[text()='Delete folder']");
        public IWebElement DeleteFolderAction => Driver.FindElement(DeleteFolderBy);

        private readonly By DeleteLabelBy = By.XPath("//button[text()='Delete label']");
        public IWebElement DeleteLabelAction => Driver.FindElement(DeleteLabelBy);

        private readonly By DeletePopUpBy = By.XPath("//div[@class='modal-two']//button[contains(text(), 'Delete')]");
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
            wait.Until(c => c.FindElement(FolderNameBy).Displayed);

            FolderName.Click();
            FolderName.SendKeys(proton.FolderName);
            Save.Click();

            return this;
        }

        public FoldersAndLabelsPage VerifyFolderIsAdded()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(FoldersNameBy));

            if (Driver.FindElements(AddedLabelBy(proton.FolderName)).Count == 0)
            {
                js.ExecuteScript("arguments[0].click();", FoldersName);
            }

            wait.Until(c => c.FindElement(AddedFolderBy(proton.FolderName)));

            Assert.IsTrue(Driver.FindElement(AddedFolderBy(proton.FolderName)).Displayed, $"Folder with name [{proton.FolderName}] was not added");

            return this;
        }

        public FoldersAndLabelsPage AddLabel()
        {
            proton.LabelName = HelperMethods.RandomString(10);
            AddLabelButton.Click();

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(FolderNameBy).Displayed);

            FolderName.Click();
            FolderName.SendKeys(proton.LabelName);
            Save.Click();

            return this;
        }

        public FoldersAndLabelsPage VerifyLabelIsAdded()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(c => c.FindElement(LabelsNameBy));

            if (Driver.FindElements(AddedLabelBy(proton.LabelName)).Count == 0)
            {
                js.ExecuteScript("arguments[0].click();", LabelsName);
            }

            wait.Until(c => c.FindElement(AddedLabelBy(proton.LabelName)));

            Assert.IsTrue(Driver.FindElement(AddedLabelBy(proton.LabelName)).Displayed, $"Folder with name [{proton.LabelName}] was not added");

            return this;
        }

        public FoldersAndLabelsPage RemoveFolder()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            Actions action = new Actions(Driver);

            action.MoveToElement(FolderActions).Perform();
            wait.Until(c => c.FindElement(FolderActionsBy)).Click();

            wait.Until(c => c.FindElement(DeleteFolderBy)).Click();

            wait.Until(c => c.FindElement(DeletePopUpBy)).Click();

            return this;
        }

        public FoldersAndLabelsPage RemoveLabel()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            Actions action = new Actions(Driver);

            action.MoveToElement(LabelActions).Perform();
            wait.Until(c => c.FindElement(LabelActionsBy)).Click();

            wait.Until(c => c.FindElement(DeleteLabelBy)).Click();

            wait.Until(c => c.FindElement(DeletePopUpBy)).Click();

            return this;
        }
    }
}
