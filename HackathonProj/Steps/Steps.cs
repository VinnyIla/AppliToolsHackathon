using Applitools;
using Applitools.Selenium;
using HackathonProj.Helpers;
using HackathonProj.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Drawing;
using TechTalk.SpecFlow;


namespace HackathonProj.Steps
{
    [Binding]
    public class Steps
    {
        PageManager pageManager = new PageManager();        

        private EyesRunner runner;
        private Eyes eyes;

        private IWebDriver driver = (IWebDriver)ScenarioContext.Current["WEBDRIVER"];


        [Given(@"Test")]
        public void GivenTest()
        {
            //Initialize the Runner for your test.
            runner = new ClassicRunner();

            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes(runner);

            // Use Chrome browser
            driver = new ChromeDriver();

            // Start the test by setting AUT's name, window or the page name that's being tested, viewport width and height
            eyes.Open(driver, "Demo App", "Smoke Test", new Size(800, 600));

            // Navigate the browser to the "ACME" demo app. To see visual bugs after the first run, use the commented line below instead.
            driver.Url = "https://demo.applitools.com/";
            //driver.Url = "https://demo.applitools.com/index_v2.html";

            // Visual checkpoint #1 - Check the login page.
            eyes.CheckWindow("Login Page");

            // This will create a test with two test steps.
            driver.FindElement(By.Id("log-in")).Click();

            // Visual checkpoint #2 - Check the app page.
            eyes.CheckWindow("App Window");

            // End the test.
            eyes.CloseAsync();
        }



        [Given(@"Home Screen Displayed")]
        [Then(@"Home Screen Displayed")]
        public void GivenHomeScreenDisplayed()
        {
            driver.ElementVisible(By.CssSelector("div form"), "Home Screen");

        }

        [Then(@"Verify Login Page UI elements")]
        public void ThenVerifyLoginPageUIElements()
        {
            pageManager.allPage.VerifyLoginPageUIElements();
        }


        [Then(@"Verify Login functionality")]
        public void ThenVerifyLoginFunctionality()
        {
            pageManager.allPage.VerifyLoginFunction();
        }

        [Then(@"Login")]
        public void ThenLogin()
        {
            pageManager.allPage.Login();
        }

        [Then(@"In recent transaction, Sort Amounts column and verify")]
        public void ThenInRecentTransactionSortAmountsColumnAndVerify()
        {
            pageManager.allPage.SortAmountsInRecentTransAndVerify();
        }

        [Then(@"Click Compare Expenses and Verify Chart")]
        public void ThenClickCompareExpensesAndVerifyChart()
        {
            pageManager.allPage.VerifyCompareExpensesChart();
        }

        [Then(@"Go to the url with ad true")]
        public void ThenGoToTheUrlWithAdTrue()
        {
            string url = ConfigurationManager.AppSettings["URL"].ToString();
            driver.Navigate().GoToUrl($"{url}?showAd=true");

            GivenHomeScreenDisplayed();
        }

        [Then(@"Verify (.*) adv gifs")]
        public void ThenVerifyAdvGifs(int adNum)
        {
            pageManager.allPage.VerifyAdvGifs();
        }

        // APPLITOOLs 
        [Given(@"Home Screen Displayed Initialize Eyes")]
        public void GivenHomeScreenDisplayedInitializeEyes()
        {
            driver.ElementVisible(By.CssSelector("div form"), "Home Screen");
        }

        [Then(@"Take Eyes Screenshot ""(.*)""")]
        public void ThenTakeEyesScreenshot(string tag)
        {
            eyes.CheckWindow(tag);

        }

        

    }
}
