using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TechnicalAssignmentML
{
    [TestClass]
    public class UnitTest1
    {   
        
        [TestMethod]
        public void TestSelenium()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            // go to website
            driver.Url = "https://techblog.polteq.com/testshop/";

            // open login page
            driver.FindElement(By.ClassName("login")).Click();

            // Enter username
            driver.FindElement(By.Id("email")).SendKeys("tester@test.com");

            // Enter password
            driver.FindElement(By.Id("passwd")).SendKeys("1qazxsw2");

            // Click the Log in button
            driver.FindElement(By.Id("SubmitLogin")).Click();

            // Verify if the logout link is displayed
            Assert.IsTrue(driver.FindElement(By.CssSelector("a.logout")).Displayed, "Logout link should be displayed");


            // Verify if the MY ACCOUNT text is show
            String validationString = driver.FindElement(By.CssSelector("h1.page-heading")).Text;

            Assert.AreEqual("MY ACCOUNT", validationString, "My account element was not found");

            driver.Quit();
        }
    }
}
