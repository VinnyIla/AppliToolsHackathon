using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HackathonProj.PageObjects
{
    class BasePage
    {

        protected IWebDriver Driver;
        public BasePage(String urlOfPage)
        {
            Driver = (IWebDriver)ScenarioContext.Current["WEBDRIVER"];
        }
    }
}
