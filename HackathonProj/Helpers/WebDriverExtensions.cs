using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Reflection;
using SeleniumExtras.PageObjects;
using System.Text;
using System.Globalization;
using HackathonProj.PageObjects;

namespace HackathonProj.Helpers
{
    public static class WebDriverExtensions
    {        
        
        public static void ClickElement(this IWebDriver driver, By by, string message, int timeoutInSecs = 30)
        {

            var element = driver.ElementVisible(by, message, timeoutInSecs);
            ClickElement(element, message, timeoutInSecs);

        }

        public static void ClickElement(this IWebElement element, IWebDriver driver, string message, int timeoutInSecs = 30, bool isThrowError = true)
        {
            try
            {
                new Actions(driver).MoveToElement(element).Perform();
            }
            catch (Exception) { }
            try
            {

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                var finalElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                
                ClickElement(finalElement, message, timeoutInSecs);

            }
            catch (Exception ex)
            {
                if (isThrowError)
                    throw new Exception($"{message} is not clickable, after waiting for {timeoutInSecs} seconds. {ex.Message}", ex);

            }


        }
        public static void ClickElement(IWebElement element, string message, int timeoutInSecs = 30)
        {
            try
            {
                element.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to click element {message}. {ex.Message}", ex);

            }
        }

        public static void SendKeysElement(this IWebDriver driver, By by, string inputText, string message, int timeoutInSecs = 30)
        {

            var element = driver.ElementVisible(by, message, timeoutInSecs);
            SendKeysElement(element, inputText, message);

        }

        public static void SendKeysElement(this IWebElement element, string inputText, string message)
        {
            try
            {
                element.Click(); }
            catch (Exception) { }

            try
            {
                element.Clear();
                element.SendKeys(inputText);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while input text '{inputText}' to field - {message}. {ex.Message} ", ex);
            }
        }


        public static void ClearElement(this IWebElement element, string message, bool isThrowError = true)
        {
            try
            {
                element.Clear();
            }
            catch (Exception ex)
            {
                if (isThrowError)
                    throw new Exception($"Error while clearing text in - {message}. {ex.Message}  ", ex);
            }
        }            
        

        public static IWebElement[] ElementsExists(this IWebDriver driver, By by, string message, int timeoutInSecs = 30, bool isThrowError = true)
        {

            IWebElement[] finalElements = null;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                finalElements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by)).ToArray();


            }
            catch (Exception ex)
            {
                if (isThrowError)
                    throw new Exception($"{message} not exists or no matching selector, after waiting for {timeoutInSecs} seconds. Message : {ex.Message} ", ex);
            }

            return finalElements;

        }
        
        public static IWebElement ElementVisible(this IWebDriver driver, By by, string message, int timeoutInSecs = 30, bool isThrowError = true)
        {


            IWebElement finalElement = null;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSecs));
                wait.PollingInterval = TimeSpan.FromSeconds(1);
                finalElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                new Actions(driver).MoveToElement(finalElement).Perform();

            }
            catch (Exception ex)
            {
                if (isThrowError)
                    throw new Exception($"{message} is not visible or no matching selector, after waiting for {timeoutInSecs} seconds. Message : {ex.Message} ", ex);
            }

            return finalElement;

        }
        
        public static void TakeScreenshot(this IWebDriver _driver, string filename = null)
        {

            if (_driver == null)
            {
                Console.WriteLine("Cannot take screenshot, driver is null");
                return;
            }

            if (String.IsNullOrEmpty(filename))
                filename = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            else
                filename = filename + "_" + GetCurrentDateTimeAsString();

            var tempFileName = Path.Combine(Directory.GetCurrentDirectory(), filename) + ".jpg";
            tempFileName = tempFileName.Replace(" ", "_");

            try
            {
                var takesScreenshot = _driver as ITakesScreenshot;
                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    screenshot.SaveAsFile(tempFileName, ScreenshotImageFormat.Jpeg);

                    Console.WriteLine($"file:///{tempFileName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not save image : {0}, {1}", filename, e.Message);
            }


        }

        public static string GetCurrentDateTimeAsString()
        {
            string formatted = DateTime.Now.ToString("ddMMyyHHmmss", CultureInfo.InvariantCulture);
            return formatted;

        }


    }
}
