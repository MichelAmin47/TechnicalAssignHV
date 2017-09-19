using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TechnicalAssignmentML.steps
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        protected IWebDriver driver;

        [Given(@"I am on the Polteq WebShop homepage")]
        public void GivenIAmOnThePolteqWebShopHomepage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://techblog.polteq.com/testshop/";
        }
        
        [Given(@"I click the login link")]
        public void GivenIClickTheLoginLink()
        {
            driver.FindElement(By.ClassName("login")).Click();
        }
        
        [When(@"I login")]
        public void WhenILogin()
        {
            driver.FindElement(By.Id("email")).SendKeys("tester@test.com");
            driver.FindElement(By.Id("passwd")).SendKeys("1qazxsw2");
            driver.FindElement(By.Id("SubmitLogin")).Click();
        }
        
        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            Assert.IsTrue(driver.FindElement(By.CssSelector("a.logout")).Displayed, "Logout link should be displayed");
            String validationString = driver.FindElement(By.CssSelector("h1.page-heading")).Text;
            Assert.AreEqual("MY ACCOUNT", validationString, "My account element was not found");
            driver.Quit();
        }
    }
}
