using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using TestPipeline.WebDrivers;

namespace TestPipeline.Helper
{
    public class DriverHelper
    {
      //  public static RemoteWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        WebDriverSetup WebDriverSetup => new WebDriverSetup();

        public IWebDriver GetWebDriver()
        {
            return WebDriverSetup.GetWebDriver();
        }

    }
}

