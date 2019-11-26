using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using System.Threading;
using HackathonProj.Helpers;

namespace HackathonProj.PageObjects
{
    class AllPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "#username")]
        private IWebElement usernameTxt { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#password")]
        private IWebElement passwordTxt { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button#log-in")]
        private IWebElement loginBtn { get; set; }

        StringBuilder error;
        StringBuilder success;

        public AllPage(String title = "") : base(title)
        {
            PageFactory.InitElements(Driver, this);
        }

        internal void VerifyLoginPageUIElements()
        {
            error = new StringBuilder();
            success = new StringBuilder();

            //Header Text
            var headerEle = Driver.ElementVisible(By.CssSelector("h4.auth-header"), "Home Screen - Header");
            AddMessage(headerEle.Text.Contains("Login"), "Header contains text 'Login' ", "Header does not contain text 'Login'; ");


            //Username PreIcon
            var unPreIcon = Driver.ElementVisible(By.CssSelector("form .form-group:first-of-type div.pre-icon"), null, 5, false);
            AddMessage(unPreIcon != null, "Pre Icon present for Username", "PreIcon not present for Username; ");

            //Password PreIcon
            var pwdPreIcon = Driver.ElementVisible(By.CssSelector("form .form-group:last-of-type div.pre-icon"), null, 5, false);
            AddMessage(unPreIcon != null, "Pre Icon present for Password", "PreIcon not present for Password; ");

            //Username PlaceHolde Text
            var unBGRNDTxt = Driver.ElementVisible(By.CssSelector("#username"), "Username text field");
            AddMessage(unBGRNDTxt.GetAttribute("placeholder").Equals("Enter your username"), "Username Placeholder text is 'Enter your username'", "Username Placeholder text is not 'Enter your username'; ");

            //Password PlaceHolde Text
            var pwdBGRNDTxt = Driver.ElementVisible(By.CssSelector("#password"), "Password text field");
            AddMessage(pwdBGRNDTxt.GetAttribute("placeholder").Equals("Enter your password"), "Password Placeholder text is 'Enter your password'", "Password Placeholder text is not 'Enter your password'; ");

            //Remember Me Margin
            var rememberMe = Driver.ElementVisible(By.CssSelector("form input.form-check-input"), "Remember me check field");
            AddMessage(!rememberMe.GetCssValue("margin-right").Contains("50"), "Remember me check has no margin-right value ", "Remember me check has a margin-right value of 50; ");

            //Linkedin icon
            var linkedInIcon = Driver.ElementVisible(By.CssSelector("img[src*='linkedin']"), null, 5, false);
            AddMessage(linkedInIcon != null, "Linked in social icon present", "Linkedin social icon not present");

            FinalCheck();

        }

        internal void Login()
        {
            usernameTxt.SendKeysElement("Hackathon", "Username text field");
            passwordTxt.SendKeysElement("Password123", "Password text field");
            loginBtn.ClickElement(Driver, "Login button");
            Driver.ElementVisible(By.CssSelector(".logged-user-w.avatar-inline"), "User successfully logged in avatar", 5, false);

        }
        
        private void CheckAlert(string title, string alertText)
        {
            var alertEle = Driver.ElementVisible(By.CssSelector("[id*='random_id'].alert-warning"), "Alert", 10, false);
            AddMessage(alertEle != null && !alertEle.GetCssValue("display").Equals("block"), $"{title} - Alert present", $"{title} - Alert Not present; ");

            if (alertEle != null)
                AddMessage(alertEle.Text.Contains(alertText), $"{title} - Alert message contains text '{alertText}'", $"{title} -  Alert message does not contains text '{alertText}'; ");

        }

        internal void VerifyLoginFunction()
        {
            error = new StringBuilder();
            success = new StringBuilder();

            //2a. No username and password
            loginBtn.ClickElement(Driver, "Login button");
            CheckAlert("2a.Login with empty username and Password", "Both Username and Password must be present");

            Thread.Sleep(500);
            //2b. Only Username input
            usernameTxt.SendKeysElement("Hackathon", "Username text field");
            loginBtn.ClickElement(Driver, "Login button");
            CheckAlert("2b.Login with username and empty Password", "Password must be present");

            Thread.Sleep(500);
            //2c. Only Password input
            usernameTxt.ClearElement("Username text field");
            passwordTxt.SendKeysElement("Password123", "Password text field");
            loginBtn.ClickElement(Driver, "Login button");
            CheckAlert("2c.Login with empty username", "Username must be present");

            Thread.Sleep(500);
            //2d. Both Username and password
            usernameTxt.SendKeysElement("Hackathon", "Username text field");
            passwordTxt.SendKeysElement("Password123", "Password text field");
            loginBtn.ClickElement(Driver, "Login button");
            var loggedInAvatarEle = Driver.ElementVisible(By.CssSelector(".logged-user-w.avatar-inline"), null, 5, false);
            AddMessage(loggedInAvatarEle != null, "2d.Username and Password with Value - User loggedin", "Username and Password with value - User Not loggedin");

            FinalCheck();
        }
        
        private void CheckArray(int rowNum, string actual, string expected)
        {
            AddMessage(actual.Contains(expected), $"Amounts in row {rowNum} correct : {actual}", $"Amounts in row {rowNum} not correct. Expected : {expected} Actual : {actual}; ");
        }
        internal void SortAmountsInRecentTransAndVerify()
        {
            error = new StringBuilder();
            success = new StringBuilder();

            //Click Amounts column
            Driver.ClickElement(By.CssSelector("#transactionsTable #amount"), "Recent Trasactions table - Amount column heading");

            var allAmountsColumn = Driver.ElementsExists(By.CssSelector("#transactionsTable tbody tr td:nth-of-type(5)"), "Recent Trasactions - Amounts column, all rows");

            string[] amtsArr =  { "- 320.00", "- 244.00", "+ 17.99", "+ 340.00", "+ 952.23", "+ 1,250.00" };

            CheckArray(1, allAmountsColumn[0].Text, amtsArr[0]);
            CheckArray(2, allAmountsColumn[1].Text, amtsArr[1]);
            CheckArray(3, allAmountsColumn[2].Text, amtsArr[2]);
            CheckArray(4, allAmountsColumn[3].Text, amtsArr[3]);
            CheckArray(5, allAmountsColumn[4].Text, amtsArr[4]);
            CheckArray(6, allAmountsColumn[5].Text, amtsArr[5]);

            FinalCheck();
        }

        internal void VerifyCompareExpensesChart()
        {
            //Click Compare Expenses
            Driver.ClickElement(By.CssSelector("#showExpensesChart"), "Compare Expenses link");

            Driver.ElementVisible(By.CssSelector("Canvas#canvas"), "Canvas chart");

            throw new Exception("Cannot automate / there is no one easy way to automate the canvas chart test. Looking up online, it seems we will need additional toolsets like OpenCV or with  java scripting tweaking. I don't have experience using those tools ");

        }

        internal void VerifyAdvGifs()
        {

            error = new StringBuilder();
            success = new StringBuilder();

            var gif1 = Driver.ElementVisible(By.CssSelector("#flashSale img"), "Adv 1 Gif", 5, false);
            AddMessage(gif1 != null && !gif1.GetCssValue("display").Equals("block"), "GIF1 present", "GIF1 not present");

            var gif2 = Driver.ElementVisible(By.CssSelector("#flashSale2 img"), "Adv 2 Gif", 5, false);
            AddMessage(gif2 != null && !gif2.GetCssValue("display").Equals("block"), "GIF2 present", "GIF2 not present");

            FinalCheck();
        }

        private void AddMessage(bool condn, string successMsg, string errMsg)
        {
            if (condn)
                success.AppendLine(successMsg);
            else
                error.AppendLine(errMsg);
        }

        private void FinalCheck()
        {
            if (string.IsNullOrEmpty(error.ToString()))
                Console.WriteLine(success.ToString());
            else
                throw new Exception(error.ToString());
        }
    }
}