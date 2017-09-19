using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace TechnicalAssignmentML
{
    [Binding]
    public class HomePageSteps
    {
        IWebDriver driver;
        WebDriverWait wait;

        [Given(@"I am on the Transavia homepage")]
        public void GivenIAmOnTheTransaviaHomepage()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            driver = new ChromeDriver(options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Url = "https://www.transavia.com/nl-NL/home/";

            //Don't actually close the cookie message. Site seems to reload it self on occasion
            IWebElement cookieAccept = wait.Until(ExpectedConditions
                .ElementToBeClickable(By.CssSelector(".cc-left>button.cookie-consent-close")));
        }

        [When(@"I select ""(.*)"" as my departure, I select ""(.*)"" as my departure")]
        public void WhenISelectAsMyDepartureISelectAsMyDeparture(string departure, string arrival)
        {
            IWebElement departureTextField = wait.Until(ExpectedConditions
                .ElementToBeClickable(By.CssSelector("#routeSelection_DepartureStation-input")));
            departureTextField.SendKeys(departure);

            IWebElement arrivalTextField = wait.Until(ExpectedConditions
                .ElementToBeClickable(By.CssSelector("#routeSelection_ArrivalStation-input")));
            arrivalTextField.Click();

            string xpathLocatorArrival = 
                "//div[@class='autocomplete-results'][not(@style)]//h6[text()[contains(.,'vanaf Amsterdam')]]/following-sibling::ol//li[text()[contains(.,'" 
                + arrival
                + "')]]";
            IWebElement autocompleteArrival = wait.Until(ExpectedConditions
                .ElementIsVisible(By.XPath(xpathLocatorArrival)));
            autocompleteArrival.Click();
        }

        [When(@"I select a departure date ""(.*)"" days in the future, I select the return date plus ""(.*)"" days")]
        public void WhenISelectADepartureDateDaysInTheFutureISelectTheReturnDatePlusDays(int departureInterval, int arrivalInterval)
        {
            IWebElement departureDateTextField = wait.Until(ExpectedConditions
                .ElementToBeClickable(By.CssSelector("#dateSelection_OutboundDate-datepicker.date-input")));
            departureDateTextField.Clear();

            IWebElement arrivalDateTextField = wait.Until(ExpectedConditions
                .ElementToBeClickable(By.CssSelector(".textfield #dateSelection_IsReturnFlight-datepicker.date-input")));
            arrivalDateTextField.Clear();

            DateTime TodayDate = new DateTime();
            TodayDate = DateTime.Now;
            departureDateTextField.SendKeys(TodayDate.AddDays(departureInterval).ToString("dd-MM-yy"));
            arrivalDateTextField.SendKeys(TodayDate.AddDays(departureInterval+arrivalInterval).ToString("dd-MM-yy"));

        }


        [When(@"I fly alone")]
        public void WhenIFlyAlone()
        {
            string passengersSite = driver.FindElement(By.CssSelector(".passengers-input-container div"))
                .GetAttribute("innerHTML");
            Assert.AreEqual("1 Volwassene", passengersSite,"Default value on the site is not correct");
        }
        
        [Then(@"I should be taken to the ""(.*)"" page")]
        public void ThenIShouldBeTakenToThePage(string pageName)
        {
            driver.FindElement(By.CssSelector(".desktop button[class*= button-primary]")).Click();
            Assert.AreEqual(pageName, driver.Title, driver.Title + " is not the correct page");
        }
        
        [Then(@"I should be able to select a flight")]
        public void ThenIShouldBeAbleToSelectAFlight()
        {
            wait.Until(ExpectedConditions
                .ElementIsVisible(By.CssSelector(".price-section button[name=next_button][type=submit]")));

            wait.Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.CssSelector("div.select")));

            IList<IWebElement> selectFlightButtons = driver.FindElements(By.CssSelector("div.select"));
            Console.WriteLine(selectFlightButtons.Count);
            Assert.IsTrue(selectFlightButtons.Count >= 2, "Amount of select buttons found to find return flights is not correct");

            driver.Quit();
        }
    }
}
