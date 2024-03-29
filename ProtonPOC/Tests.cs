using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProtonPOC.Page;
using ProtonPOC.Specification;
using System;

namespace ProtonPOC
{
    public class Tests
    {
        public IWebDriver Driver { get; set; }

        public ProtonSpec Proton = new ProtonSpec();

        [SetUp]
        public void DriverSetupChrome()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
        }

        [Test]
        public void WhenUserEntersCredentialsUserIsLoggedIn()
        {
            new LoginPage(Driver, Proton)
                .Navigate()
                .EnterUsername()
                .EnterPassword()
                .Login()
                .VerifyUserIsLoggedIn();
        }

        [Test]
        public void WhenUserIsLoggedInTheLandingPageIsFoldersAndLabels()
        {
            new LoginPage(Driver, Proton)
                .LoginUser()
                .ClickFolders()
                .VerifyFoldersAndLabelsIsDisplayed();
        }

        [Test]
        public void WhenUserAddsFolderVerifyFolderIsAdded()
        {
            new LoginPage(Driver, Proton)
                .LoginUser()
                .ClickFolders()
                .AddFolder()
                .VerifyFolderIsAdded()
                .RemoveFolder();
        }

        [Test]
        public void WhenUserAddsLabelVerifyLabelIsAdded()
        {
            new LoginPage(Driver, Proton)
                .LoginUser()
                .ClickLabels()
                .AddLabel()
                .VerifyLabelIsAdded()
                .RemoveLabel();
        }

        [TearDown]
        public void DisposeDriverAfterScenario()
        {
            Driver.Quit();
        }
    }
}