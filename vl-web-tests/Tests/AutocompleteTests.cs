using NUnit.Framework;
using OpenQA.Selenium;

namespace vl_web_tests
{
    [TestFixture]
    class AutocompleteTests : TestBase
    {
        [SetUp]
        public void SetupPage()
        {
            manager.OpenPage("/vladivostok");
        }

        [Test]
        public void AutocompleteWithSections()
        {
            manager
                .TypeSearchQuery("Тату")
                .WaitForElementVisible(By.CssSelector("div.j_autocomplete"));

            Assert.Contains("Тату и пирсинг", manager.GetPromts("Разделы"));
        }

        [Test]
        public void AutocompleteWithCompanies()
        {
            manager
                .TypeSearchQuery("Оки")
                .WaitForElementVisible(By.CssSelector("div.j_autocomplete"));

            Assert.Contains("Оки Доки", manager.GetPromts("Компании"));
        }

        [Test]
        public void AutocompleteWithProductsAndServices()
        {
            manager
                .TypeSearchQuery("Платье")
                .WaitForElementVisible(By.CssSelector("div.j_autocomplete"));

            Assert.Contains("платье в аренду", manager.GetPromts("Остальное"));
        }

        [Test]
        public void AutocompleteWithAddresses()
        {
            manager
                .TypeSearchQuery("Свет")
                .WaitForElementVisible(By.CssSelector("div.j_autocomplete"));

            Assert.Contains("ул. Светланская", manager.GetPromts("Адреса"));
        }

        [Test]
        public void AutocompleteWithHistory()
        {
            manager
                .SearchBy("Супра")
                .OpenPage("/vladivostok")
                .TypeSearchQuery("Суп")
                .WaitForElementVisible(By.CssSelector("div.j_autocomplete"));

            Assert.Contains("Супра", manager.GetPromts("История"));
        }



    }
}
