using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using System.Threading;
using System.Xml.Serialization;
using TestWithFramework.Pages;

namespace TestWithFramework
{
    public class Tests
    {
        private IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
           chromeOptions.AddArgument("--no-sandbox");
           chromeOptions.AddArgument("--headless");

            driver = new ChromeDriver(chromeOptions);
           // driver = new ChromeDriver();
        }
        [TearDown]
        public void Teardown()
        {
            driver.Close();
            //thanks, webdriver!
            Thread.Sleep(3000);
            driver.Quit();
        }

        [Test]
        public void Change_city()
        {
            var coursePage = new CoursesPage(driver);
            var city = "Киев";
            coursePage.ChangeCityTo(city);
            //извините
            Thread.Sleep(4000);
            var currcity = coursePage.GetCurrentCity().ToLower();
            Assert.IsTrue(currcity.Equals( city.ToLower()));
        }
        [Test]
        public void Course_entry_when_not_authorized()
        {
            var coursePage = new CoursesPage(driver);
            coursePage.ClickRegButton();
            Assert.IsTrue(coursePage.CheckLogingPageRedirection());
        }
        [Test]
        public void ChangeLanguage()
        {
            var journalPage = new JournalPage(driver);
            journalPage.ChangeLanguageToUkr();
            Assert.IsTrue(journalPage.CheckLanguageOfPageIsUkr());
        }
        //[Test]
        //не работает в хедлес режиме
        public void PagesChecker()
        {
            var journalPage = new JournalPage(driver);
            var category = "Grammar";
            journalPage.SelectCategory(category);
            Thread.Sleep(500);
            journalPage.GoToNextPage();
            Assert.IsTrue(journalPage.CheckIfNextPageIsOpened());
        }
        //[Test]
        //не работает в хедлес режиме
        public void SearchField()
        {
            var journalPage = new JournalPage(driver);
            journalPage.SearchButtonClick();
            var text = "B1";
            
            journalPage.EnterTextInSearchField(text);
            Assert.IsTrue(journalPage.CheckIfSearchPage());
        }
        [Test]
        public void ReadMoreCheck()
        {
            var journalPage = new JournalPage(driver);
            //за такое быстрое нажимание банят
            //Thread.Sleep(1000);
            journalPage.ReadMoreClick();
            Thread.Sleep(1000);
            Assert.IsTrue(journalPage.CheckIfNewArticleAppears());
        }
        [Test]
        public void WriteUsCheck()
        {
            var journalPage = new JournalPage(driver);
            journalPage.ClickWriteUs();
            Assert.IsTrue(journalPage.CheckIfLetterFormVisible());
        }
    }
}