using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using TestWithFramework.Pages;
using System.Threading;

namespace TestWithFramework.Pages
{
    class CoursesPage
    {
        private string reg_button = @"reg-button";
        private string change_city_popup = ".cities-popup .inner ul li";
        private string change_city_button = ".cities-switch a";
        private string login_page_adress = @"my.greenforest";


        private IWebDriver driver;
        public CoursesPage(IWebDriver _driver)
        {
            driver = _driver;
            driver.Navigate().GoToUrl("https://greenforest.com.ua/courses/");
        }
        public CoursesPage ChangeCityTo(string p0)
        {
            var cities = driver.FindElements(By.CssSelector(change_city_popup));
            foreach (var city in cities)
            {
                if (city.Text.Trim().ToLower().Equals(p0.ToLower()))
                {
                    city.Click();
                    break;
                }
            }
            return this;
        }
        public CoursesPage ClickRegButton()
        {
            //actualy page is piece of trash - you can just scroll through city selection dialog)))
            driver.FindElement(By.ClassName(reg_button)).Click();
            return this;
        }
        public string GetCurrentCity()
        {
            var currCity = driver.FindElement(By.CssSelector(change_city_button)).Text;
            return currCity;
        }
        public void SwitchToNewWindow()
        {
            var windows = driver.WindowHandles[1];
            driver.SwitchTo().Window(windows);
        }
        public bool CheckLogingPageRedirection()
        {
            SwitchToNewWindow();
            var res = driver.Url.Contains(login_page_adress);
            return res;
        }


    }
}
