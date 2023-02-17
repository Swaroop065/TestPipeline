using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPipeline.Constants;

namespace TestPipeline.Utilities
{
    public class SeleniumUtilities
    {
        private readonly ScenarioContext _scenarioContext;
        public WebDriverWait _wait;
        public IJavaScriptExecutor js;
        public IWebDriver _driver;
        private static readonly string _currentDirectory = @"$Agent.TempDirectory";
        private static string _testDataLocation = "";
        public SeleniumUtilities(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = _scenarioContext.Get<IWebDriver>(ScenarioContextKeys.WebDriver);
          //  _wait = _driver.GetWebDriverWait(30, 5);
            js = (IJavaScriptExecutor)_driver;

        }
        public void SetPageUrl(string url)
        {
            Thread.Sleep(200);
            _driver.Url = url;
        }
    }
}
