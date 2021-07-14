using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Drawing;

namespace vl_web_tests
{
    public class Manager
    {
        protected IWebDriver driver;
        protected string baseURL;

        private static ThreadLocal<Manager> manager = new ThreadLocal<Manager>();

        private Manager()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.vl.ru";
            driver.Manage().Window.Size = new Size(1440, 900);
        }

        public static Manager GetInstance()
        {
            if (!manager.IsValueCreated)
            {
                manager.Value = new Manager();
            }
            return manager.Value;
        }

        ~Manager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
            }
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public Manager OpenPage(string url)
        {
            url = baseURL + url;
            if (driver.Url != url)
            {
                driver.Navigate().GoToUrl(url);
            }
            return this;
        }

        public Manager TypeSearchQuery(string query)
        {
            driver.FindElement(By.Name("search")).Click();
            driver.FindElement(By.Name("search")).Clear();
            driver.FindElement(By.Name("search")).SendKeys(query);
            return this;
        }

        public Manager PressSearchButton()
        {
            driver.FindElement(By.CssSelector("button.j_searchSubmit")).Click();
            return this;
        }

        public Manager SendEnter()
        {
            driver.FindElement(By.Name("search")).SendKeys(Keys.Enter);
            return this;
        }

        public Manager SearchBy(string query)
        {
            TypeSearchQuery(query);
            PressSearchButton();
            WaitForElementPresent(By.CssSelector("section.companies"));
            return this;
        }

        public List<string> GetCompanies()
        {
            List<string> companies = new List<string>();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.company"));
            foreach (IWebElement element in elements)
            {
                companies.Add(element.FindElement(By.CssSelector("header.company__header a")).Text);
            }
            return companies;
        }

        public List<string> GetAddresses()
        {
            List<string> addresses = new List<string>();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("div.company"));
            foreach (IWebElement element in elements)
            {
                addresses.Add(element.FindElement(By.CssSelector("div.address-row")).GetAttribute("innerText").Trim());
            }
            return addresses;
        }

        public string GetSectionTitle()
        {
            return driver.FindElement(By.CssSelector("h1.item-title")).Text;
        }

        public bool WaitForElementPresent(By by, int sec = 3)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(sec)).Until(drv => drv.FindElement(by));
            } 
            catch(WebDriverTimeoutException exception)
            {
                return false;
            }
            return true;
        }

        public Manager WaitForElementVisible(By by, int sec = 3)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(sec)).Until(drv => drv.FindElement(by).Displayed);
            return this;
        }

        public Manager ClickOnElement(IWebElement element)
        {
            element.Click();
            return this;
        }

        public Manager HoverOnElement(IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Perform();
            return this;
        }

        public Manager ChooseMainSection(string section)
        {
                ClickOnElement(driver.FindElement(By.CssSelector("a.j_toggleForums")))
                .WaitForElementVisible(By.CssSelector("menu.forums-menu"))
                .ClickOnElement(driver.FindElement(By.CssSelector("menu.forums-menu")).FindElement(By.LinkText(section)))
                .WaitForElementPresent(By.CssSelector("section.companies"));
            return this;
        }

        public Manager ChooseMinorSection(string mainSection, string minorSection)
        {
            ClickOnElement(driver.FindElement(By.CssSelector("a.j_toggleForums")))
           .WaitForElementVisible(By.CssSelector("menu.forums-menu"))
           .HoverOnElement(driver.FindElement(By.CssSelector("menu.forums-menu")).FindElement(By.LinkText(mainSection)))
           .WaitForElementVisible(By.CssSelector("div.j_renderForums"))
           .ClickOnElement(driver.FindElement(By.CssSelector("div.j_renderForums")).FindElement(By.LinkText(minorSection)))
           .WaitForElementPresent(By.CssSelector("section.companies"));
            return this;
        }

        public Manager ChooseCity(string city)
        {
            ClickOnElement(driver.FindElement(By.CssSelector("div.j_cityList")))
            .WaitForElementVisible(By.CssSelector("div.j_cityList div.dropdown-list"))
            .ClickOnElement(driver.FindElement(By.CssSelector("div.j_cityList div.dropdown-list")).FindElement(By.LinkText(city)))
            .WaitForElementPresent(By.CssSelector("section.companies"));
            return this;
        }

        public string GetCurrentUrl()
        {
            return driver.Url.Substring(baseURL.Length);
        }

        public List<string> GetPromts(string itemName)
        {
            List<string> promts = new List<string>();
            ICollection<IWebElement> items = driver.FindElements(By.CssSelector("div.j_autocomplete div.wrap"));
            foreach (IWebElement item in items)
            {
                if (item.FindElement(By.CssSelector("span.name")).Text == itemName)
                {
                    ICollection<IWebElement> elements = item.FindElements(By.CssSelector("div.autocomplete-item"));
                    foreach (IWebElement element in elements)
                    {
                        promts.Add(element.Text);
                    }
                    break;
                }
                
            }
            return promts;
        }

    }
}