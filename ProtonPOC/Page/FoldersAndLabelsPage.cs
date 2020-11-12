using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProtonPOC.Page
{
    public class FoldersAndLabelsPage : Page
    {
        public FoldersAndLabelsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement AddFolderButton => Driver.FindElement(By.XPath("//button[text()='Add folder']"));

        public IWebElement AddLabelButton => Driver.FindElement(By.XPath("//button[text()='Add label']"));

        public FoldersAndLabelsPage VerifyFoldersAndLabelsIsDisplayed()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(AddFolderButton.Displayed, "Buton Add folder is not displayed");
                Assert.IsTrue(AddLabelButton.Displayed, "Buton Add label is not displayed");
            });

            return this;
        }
    }
}
