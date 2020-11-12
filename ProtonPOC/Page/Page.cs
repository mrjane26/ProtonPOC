using OpenQA.Selenium;

namespace ProtonPOC.Page
{
    public class Page
    {
        internal IWebDriver Driver;

        public Page(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}
