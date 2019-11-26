using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;
using HackathonProj.Helpers;
using Applitools.Selenium;
using Applitools;
using System.Drawing;

namespace HackathonProj.SpecFlow
{
    [Binding]
    public class Hooks
    {

        private EyesRunner runner;
        private Eyes eyes;


        [BeforeScenario]
        public void BeforeScenario()
        {
            //Initialize the Runner for your test.
            //runner = new ClassicRunner();

            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            //eyes = new Eyes(runner);


            //eyes.SetLogHandler(new StdoutLogHandler(true));

            IWebDriver driver = new ChromeDriver();

            //eyes.Open(driver, "Demo App", "Hackathon");

            driver.Url = ConfigurationManager.AppSettings["URL"].ToString();

            driver.Manage().Window.Maximize();

            ScenarioContext.Current["WEBDRIVER"] = driver;

           
        }
                
        private void TakeErrorScreenShot()
        {
            IWebDriver driver = (IWebDriver)ScenarioContext.Current["WEBDRIVER"];
            driver.TakeScreenshot("Error");

        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = null;
            try
            {
                driver = (IWebDriver)ScenarioContext.Current["WEBDRIVER"];
                driver.Close();
                driver.Quit();
                driver = null;

            }
            catch (Exception e)
            {
                Console.WriteLine($"Selenium Web Driver not found to dispose. {e.Message}");
            }

            finally
            {


                ScenarioContext.Current["WEBDRIVER"] = null;

                Console.WriteLine("After Scenario - Test Completed");
            }


            //Close Eyes
            try
            {
                EyesRunner runner = (EyesRunner)ScenarioContext.Current["EYESRUNNER"];
                Eyes eyes = (Eyes)ScenarioContext.Current["EYES"];

                eyes.CloseAsync();

                // If the test was aborted before eyes.close was called, ends the test as aborted.
                eyes.AbortIfNotClosed();

                //Wait and collect all test results
                TestResultsSummary allTestResults = runner.GetAllTestResults();

            }catch(Exception e)
            {

            }



        }

    }
}
