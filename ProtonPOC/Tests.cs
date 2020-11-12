using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProtonPOC.Page;
using System;

namespace ProtonPOC
{
    [Parallelizable]
    [TestFixture]
    public class Tests
    {
        public IWebDriver Driver { get; set; }

        [SetUp]
        public void DriverSetupChrome()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [Test]
        public void WhenUserEntersCredentialsUserIsLoggedIn()
        {
            new LoginPage(Driver)
                .Navigate()
                .EnterUsername()
                .EnterPassword()
                .Login()
                .VerifyUserIsLoggedIn();
        }

        [Test]
        public void WhenUserIsLoggedInTheLandingPageIsFoldersAndLabels()
        {
            new LoginPage(Driver)
                .LoginUser()
                .ClickFoldersAndLabels()
                .VerifyFoldersAndLabelsIsDisplayed();
        }


        [TearDown]
        public void DisposeDriverAfterScenario()
        {
            Driver.Quit();
        }
    }
}