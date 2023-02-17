using BoDi;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPipeline.Configuration;
using TestPipeline.Constants;
using TestPipeline.Helper;
using TestPipeline.Utilities;

namespace TestPipeline.Hooks
{
    [Binding]
    public class BeforeBindingHooks
    {
        private DriverHelper _driverHelper;
        public IWebDriver Driver;
        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private SeleniumUtilities _seleniumUtilities;
        public IConfiguration Configuration { get; }
        public BeforeBindingHooks( IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            try
            {

                CheckAndOverwriteEnvironmentVariables();
                var configurationHelper = BindConfig();
                SetInitialConfiguration(configurationHelper);
              /*  _driverHelper = new DriverHelper();
                Driver = _driverHelper.GetWebDriver();
                _scenarioContext.Set(Driver, ScenarioContextKeys.WebDriver);
                _objectContainer.RegisterInstanceAs(new SeleniumUtilities(_scenarioContext));
                _seleniumUtilities = new SeleniumUtilities(_scenarioContext);
              */ 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SetInitialConfiguration(ConfigurationHelper configurationHelper)
        {
            if (!configurationHelper.ExternalConnections.AdminSiteUrl.EndsWith("/"))
            {
               var s= configurationHelper.ExternalConnections.AdminSiteUrl += "/";
            }
            
            _scenarioContext.Set(configurationHelper.ExternalConnections.CustomerSiteUrl, ScenarioContextKeys.CustomerSiteUrl);
            
        }

        private static void CheckAndOverwriteEnvironmentVariables()
        {
          /*  if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.TestPipeline, EnvironmentVariableValues.);
            }*/

            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment)))
            {
                Environment.SetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment, EnvironmentVariableValues.Admin);
            }
        }

        private ConfigurationHelper BindConfig()
        {
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.TestEnvironment);
            string appsettingsPath = environment == null ? "appsettings.json" : $"appsettings.{environment}.json";
            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(appsettingsPath).Build();
            ConfigurationHelper configurationHelper = new ConfigurationHelper(config);
            _objectContainer.RegisterInstanceAs(config);
            _objectContainer.RegisterInstanceAs(configurationHelper);
            return configurationHelper;
        }

       
        /* catch (Exception ex)
         {
             Console.WriteLine(ex.Message);
            // return null;
         }*/
    }
    }
