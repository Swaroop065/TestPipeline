using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Text;
using TestPipeline.Constants;
using WebDriverManager.DriverConfigs.Impl;

namespace TestPipeline.WebDrivers
{
    public class WebDriverSetup
    {
        public object path;
        public object browserversion;
        public ChromeOptions chromeoptions = new ChromeOptions();
        public static FirefoxOptions firefoxoptions = new FirefoxOptions();
        public Uri url = new Uri("http://localhost:4444/wd/hub");
        private static FirefoxDriverService firefoxDriverService;

        public WebDriverSetup()
        {
            chromeoptions.AddArgument("--start-maximized");
            chromeoptions.AddArgument("--no-sandbox");
            chromeoptions.AddArgument("--verbose");
            chromeoptions.AddArgument("--disable-gpu");
            chromeoptions.AddArgument("--disable-extensions");
            chromeoptions.AddArgument("--disable-dev-shm-usage");
            chromeoptions.AddArgument("disable-infobars");
            var testPipeline = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline);
            if (!testPipeline.Equals(EnvironmentVariableValues.Local, StringComparison.InvariantCultureIgnoreCase))
            {
                chromeoptions.AddArguments("--headless", "--window-size=1920,1200");
            }
            firefoxoptions.AddArgument("--headless");
            if (Environment.GetEnvironmentVariable("Test_Environment").ToLower() == "local")
            {
                chromeoptions.AcceptInsecureCertificates = true;
                firefoxoptions.AcceptInsecureCertificates = true;

            }
        }

        public IWebDriver GetWebDriver()
        {
            CodePagesEncodingProvider.Instance.GetEncoding(437);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.Test_Browser)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.Test_Browser, EnvironmentVariableValues.Chrome);
            }

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.WebDriver)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.WebDriver, EnvironmentVariableValues.Remote);
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WebDriver")))
            {
                if (Environment.GetEnvironmentVariable("WebDriver").ToLower() == "remote")
                {
                    switch (Environment.GetEnvironmentVariable("Test_Browser").ToLower())
                    {

                        case "chrome":
                            {
                                return new RemoteWebDriver(url, chromeoptions);
                            }
                        case "firefox":
                            {
                                return new RemoteWebDriver(url, firefoxoptions);
                            }
                        case "":
                            {
                                return new RemoteWebDriver(url, chromeoptions);
                            }
                        case string browser: throw new NotSupportedException($"{browser} is not a supported browser");
                        default: throw new NotSupportedException("not supported browser: <null>");
                    }
                }
            }
            else
            {
                switch (Environment.GetEnvironmentVariable("Test_Browser").ToLower())
                {

                    case "chrome":
                        {
                            var chromeconfig = new ChromeConfig();
                            var version = chromeconfig.GetMatchingBrowserVersion();
                            var envChromeWebDriver = Environment.GetEnvironmentVariable("ChromeWebDriver");
                            if (string.IsNullOrEmpty(envChromeWebDriver))
                            {
                                new WebDriverManager.DriverManager().SetUpDriver(chromeconfig, version);
                                var driver = new ChromeDriver(chromeoptions);
                                driver.Manage().Cookies.DeleteAllCookies();
                                return driver;
                            }
                            else if (File.Exists(Path.Combine(envChromeWebDriver, "chromedriver.exe")))
                            {
                                chromeoptions.AddArguments("--headless", "--window-size=1920,1200");
                                var driverPath = Path.Combine(envChromeWebDriver);
                                ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService(driverPath);
                                defaultService.HideCommandPromptWindow = true;
                                var driver = new ChromeDriver(defaultService, chromeoptions);
                                driver.Manage().Cookies.DeleteAllCookies();
                                return driver;
                            }
                            else
                                throw new DriverServiceNotFoundException("Driver not installed: <null>");
                        }
                    case "firefox":
                        {
                            var firefoxconfig = new FirefoxConfig();
                            new WebDriverManager.DriverManager().SetUpDriver(firefoxconfig);
                            var envFirefoxWebDriver = Environment.GetEnvironmentVariable("GeckoWebDriver");
                            firefoxDriverService = FirefoxDriverService.CreateDefaultService();

                            if (string.IsNullOrEmpty(envFirefoxWebDriver))
                            {
                                firefoxDriverService.Host = "::1";
                                FirefoxDriver firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxoptions);
                                firefoxDriver.Manage().Window.Maximize();
                                firefoxDriver.Manage().Cookies.DeleteAllCookies();

                                return firefoxDriver;
                            }
                            else if (File.Exists(Path.Combine(envFirefoxWebDriver, "geckodriver.exe")))
                            {
                                var driverPath = Path.Combine(envFirefoxWebDriver);
                                firefoxDriverService = FirefoxDriverService.CreateDefaultService(driverPath);
                                firefoxDriverService.Host = "::1";
                                FirefoxDriver firefoxDriver = new FirefoxDriver(firefoxDriverService, firefoxoptions);
                                firefoxDriver.Manage().Cookies.DeleteAllCookies();
                                return firefoxDriver;
                            }
                            else
                                throw new DriverServiceNotFoundException("Driver not installed: <null>");
                        }
                    case "":
                        {
                            var chromeconfig = new ChromeConfig();
                            var version = chromeconfig.GetMatchingBrowserVersion();
                            var driverPath = Path.Combine(Directory.GetCurrentDirectory());
                            var envChromeWebDriver = Environment.GetEnvironmentVariable("ChromeWebDriver");
                            if (string.IsNullOrEmpty(envChromeWebDriver))
                            {
                                new WebDriverManager.DriverManager().SetUpDriver(chromeconfig, version);
                                var driver = new ChromeDriver(chromeoptions);
                                driver.Manage().Cookies.DeleteAllCookies();
                                return driver;
                            }
                            else if (File.Exists(Path.Combine(envChromeWebDriver, "chromedriver.exe")))
                            {
                                ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService(driverPath);
                                chromeoptions.AddArguments("--headless", "--window-size=1920,1200");
                                defaultService.HideCommandPromptWindow = true;
                                var driver = new ChromeDriver(defaultService, chromeoptions);
                                driver.Manage().Cookies.DeleteAllCookies();
                                return driver;
                            }
                            else
                                throw new DriverServiceNotFoundException("Driver not installed: <null>");
                        }
                    case string browser: throw new NotSupportedException($"{browser} is not a supported browser");
                    default: throw new NotSupportedException("not supported browser: <null>");
                }
            }
            throw new Exception("Web driver environment variable not set");
        }

    }
}

