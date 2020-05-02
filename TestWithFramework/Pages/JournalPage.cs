using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using TestWithFramework.Pages;
using System.Threading;
using OpenQA.Selenium;

namespace TestWithFramework.Pages
{
    class JournalPage
    {
        private string language_switcher = @".language-switcher a";
        private string category_selector = @".categories li";
        private string next_page = @".green-pagination li a";
        private string search_button = @".search .btn";
        private string search_form = @".form input";
        private string articles = @".journal-page > div";
        private string read_more_button = @"read-more-button";
        private string write_us_button=@".letter";
        private string letter_form = @"letter_form";

        private IWebDriver driver;
        private int number_of_el;

        public JournalPage(IWebDriver _driver)
        {
            driver = _driver;
            driver.Navigate().GoToUrl("https://greenforest.com.ua/journal");
        }
        public void ChangeLanguageToUkr()
        {
            var languageSwitcher = driver.FindElement(By.CssSelector(language_switcher)) ;
            if (languageSwitcher.Text.ToLower().Equals("ukr"))
            {
                languageSwitcher.Click();
            }
        }
        ~JournalPage()
        {
           // driver.Close();
        }
        public bool CheckLanguageOfPageIsUkr()
        {
            return driver.Url.Contains("/ua");
        }
        public void SelectCategory(string p0)
        {
            var categories = driver.FindElements(By.CssSelector(category_selector));
            foreach (var category in categories)
            {
                if (category.Text.Trim().ToLower().Equals(p0.ToLower()))
                {
                    category.Click();
                    break;
                }
            }
        }
        public void GoToNextPage()
        {
            //Thread.Sleep(500);
            driver.FindElement(By.CssSelector(next_page)).Click();
        }
        public bool CheckIfNextPageIsOpened()
        {
            //новостная лента полный кошмар
            return driver.Url.Contains("page/");
        }
        public void SearchButtonClick()
        {
            driver.FindElement(By.CssSelector(search_button)).Click();
            //Thread.Sleep(1000);
        }
        public void EnterTextInSearchField(string p0)
        {
            var field = driver.FindElement(By.CssSelector(search_form));
            field.Click();
            field.SendKeys(p0);
            field.SendKeys(Keys.Enter);
        }
        public bool CheckIfSearchPage()
        {
            return driver.Url.Contains("search?q=");
        }
        public void ReadMoreClick()
        {
            //Thread.Sleep(3000);
            number_of_el = driver.FindElements(By.CssSelector(articles)).Count;
            driver.FindElement(By.ClassName(read_more_button)).Click();

        }
        public bool CheckIfNewArticleAppears()
        {
            Thread.Sleep(1000);
            var current_num_of_el = driver.FindElements(By.CssSelector(articles)).Count;
            return current_num_of_el > number_of_el;
            
        }
        public void ClickWriteUs()
        {
            driver.FindElement(By.CssSelector(write_us_button)).Click();
        }
        public bool CheckIfLetterFormVisible()
        {
            var letterForm = driver.FindElement(By.Id(letter_form));
            return letterForm.Displayed;
        }
    }
}
