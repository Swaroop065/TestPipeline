using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TestPipeline.Constants;
using TestPipeline.Utilities;

namespace TestPipeline.StepDefinitions
{
    [Binding]
    public class LaunchUrlStep
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly SeleniumUtilities _seleniumUtilities;

        public LaunchUrlStep(ScenarioContext scenarioContext, SeleniumUtilities seleniumUtilities)
        {
            _scenarioContext = scenarioContext;
            _seleniumUtilities = seleniumUtilities;
            
        }

        [Given(@"User open a browser")]
        public void GivenUserOpenABrowser()
        {
            //   string url = _scenarioContext.Get<string>(ScenarioContextKeys.CustomerSiteUrl);
            string url = _scenarioContext.Get<string>(ScenarioContextKeys.CustomerSiteUrl);
            _seleniumUtilities.SetPageUrl(url);

        }

        [When(@"user enter a url")]
        public void WhenUserEnterAUrl()
        {
            throw new PendingStepException();
        }

        [Then(@"user enetered url should be displayed")]
        public void ThenUserEneteredUrlShouldBeDisplayed()
        {
            throw new PendingStepException();
        }
    }
}
