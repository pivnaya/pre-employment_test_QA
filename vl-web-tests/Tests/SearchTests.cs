using NUnit.Framework;
using OpenQA.Selenium;

namespace vl_web_tests
{
    [TestFixture]
    public class SearchTests : TestBase
    {
        [SetUp]
        public void SetupPage()
        {
            manager.OpenPage("/vladivostok");
        }

        [Test]
        public void SearchByPressSearchButton()
        {
            manager
                .TypeSearchQuery("Реми")
                .PressSearchButton();

            Assert.IsTrue(manager.WaitForElementPresent(By.CssSelector("section.companies")));
        }

        [Test]
        public void SearchBySendEnter()
        {
            manager
                .TypeSearchQuery("Реми")
                .SendEnter();

            Assert.IsTrue(manager.WaitForElementPresent(By.CssSelector("section.companies")));
        }

        [Test]
        public void NoSearchByPressSearchButton()
        {
            manager
                .TypeSearchQuery("")
                .PressSearchButton();

            Assert.IsFalse(manager.WaitForElementPresent(By.CssSelector("section.companies")));
        }

        [Test]
        public void NoSearchWithSpacesBySendEnter()
        {
            manager
                .TypeSearchQuery("   ")
                .SendEnter();

            Assert.IsFalse(manager.WaitForElementPresent(By.CssSelector("section.companies")));
        }

    }
}
