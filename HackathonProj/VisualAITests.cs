using Applitools;
using Applitools.Selenium;
using HackathonProj.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Drawing;
using SeleniumExtras.PageObjects;
using HackathonProj.Helpers;

namespace HackathonProj
{
    [TestFixture]
    public class VisualAITests
    {

       
            private EyesRunner runner;
            private Eyes eyes1, eyes2;

            private IWebDriver driver;

            [SetUp]
            public void BeforeEach()
            {

                var batchInfo = new Applitools.BatchInfo("LoginBatch");
                
                //Initialize the Runner for your test.
                runner = new ClassicRunner();

                // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
                eyes1 = new Eyes(runner);
                eyes2 = new Eyes(runner);
                eyes2.Batch = batchInfo;

                // Use Chrome browser
                driver = new ChromeDriver();

                driver.Url = ConfigurationManager.AppSettings["URL"].ToString();

                driver.Manage().Window.Maximize();

                eyes1.Open(driver, "Demo App", $"Hackathon");
                eyes2.Open(driver, "Demo App", $"Hackathon");
        }

           

            [Test]
            public void VisualTests()
            {
                //*******************
                // #1 TEST
                // Visual checkpoint #1 - Check the login page.
                eyes1.CheckWindow("LoginPage");

                //*******************
                //#2 TEST
                // This will create a test with two test steps.
                driver.ClickElement(By.Id("log-in"), "Login button");

                // Visual checkpoint #2
                eyes2.CheckWindow("LoginPage-NoUsername-NoPassword");

                driver.FindElement(By.Id("username")).SendKeys("Hackathon");
                driver.ClickElement(By.Id("log-in"), "Login button");

                // Visual checkpoint #3
                eyes2.CheckWindow("LoginPage-NoPassword");

                driver.FindElement(By.Id("username")).Clear();
                driver.FindElement(By.Id("password")).SendKeys("password");
                driver.ClickElement(By.Id("log-in"), "Login button");

                // Visual checkpoint #4
                eyes2.CheckWindow("LoginPage-NoUsername");

                //*******************
                //#3 TEST
                driver.FindElement(By.Id("username")).SendKeys("Hackathon");
                driver.ClickElement(By.Id("log-in"), "Login button");

                driver.ClickElement(By.CssSelector("#transactionsTable #amount"), "Recent Trasactions table - Amount column heading");

                // Visual checkpoint #4
                eyes1.CheckWindow("RecentTrans-AmountSort");

                //*******************
                //#4 TEST
                driver.ClickElement(By.CssSelector("#showExpensesChart"), "Compare Expenses link");
                driver.ElementVisible(By.CssSelector("Canvas#canvas"), "Canvas chart");

                // Visual checkpoint #5
                eyes1.CheckWindow("CompareExpense-CanvasChart");

                driver.ClickElement(By.CssSelector("#addDataset"), "Show data for next year link");
                driver.ElementVisible(By.CssSelector("Canvas#canvas"), "Canvas chart");

                // Visual checkpoint #6
                eyes1.CheckWindow("CompareExpense-ChartWith2019Data");

                //*******************
                //#5 TEST
                driver.Url = $"{ConfigurationManager.AppSettings["URL"].ToString()}?showAd=true";
                driver.SendKeysElement(By.Id("username"), "Hackathon", "Username Text field");
                driver.SendKeysElement(By.Id("password"), "Password", "Password Text field");
                driver.ClickElement(By.Id("log-in"), "Login button");

                // Visual checkpoint #7
                eyes1.CheckWindow("CompareAd");

                // End the test.
                eyes1.CloseAsync();
                eyes2.CloseAsync();

        }

            [TearDown]
            public void AfterEach()
            {
                // Close the browser.
                driver.Quit();

                // If the test was aborted before eyes.close was called, ends the test as aborted.
                eyes1.AbortIfNotClosed();
                eyes2.AbortIfNotClosed();

            //Wait and collect all test results
            //TestResultsSummary allTestResults = runner.GetAllTestResults();
        }
        }
}
