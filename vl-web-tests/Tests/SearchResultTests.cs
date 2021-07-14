using NUnit.Framework;

namespace vl_web_tests
{
    [TestFixture]
    public class SearchResultTests : TestBase
    {
        [Test]
        public void SearchByUppercaseNameInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("СПОРТМАСТЕР");

            Assert.Contains("Спортмастер", manager.GetCompanies());
        }

        [Test]
        public void SearchByServiceWithWrongLayoutInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("ecnfyjdrf jrjy");

            Assert.AreEqual("Установка окон", manager.GetSectionTitle());
        }

        [Test]
        public void SearchByProductWithMistakeInRootCategoryInAllCities()
        {
            manager
                .OpenPage("/primorskij-kraj")
                .SearchBy("Цетрамон");

            Assert.AreEqual("Аптеки", manager.GetSectionTitle());
        }

        [Test]
        public void SearchByPartialAddressInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("русск 94а");

            Assert.Contains("ул. Русская, 94А", manager.GetAddresses());
        }


        [Test]
        public void SearchByEnglishNameInCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok/trade")
                .SearchBy("Zara");

            Assert.Contains("Zara", manager.GetCompanies());
        }

        [Test]
        public void SearchByServiceWithOtherWordFormInOtherCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok/pets")
                .SearchBy("Шина");

            Assert.AreEqual("Шины и диски", manager.GetSectionTitle());
        }

        [Test]
        public void SearchByAbbreviationNameInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("СИТ");

            Assert.Contains("Современные Информационные Технологии", manager.GetCompanies());
        }

        [Test]
        public void SearchByNameWithSpaceInstedOfEnglishNameWithHyphenInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("гсм сервис");

            Assert.Contains("GSM-Service", manager.GetCompanies());
        }

        [Test]
        public void SearchByNameWithoutSpaceInstedOfEnglishNameWithSpaceInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("импульсхол");

            Assert.Contains("Impulse Hall", manager.GetCompanies());
        }

        [Test]
        public void EmptySearchByNameInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .SearchBy("ъъъъъ");

            Assert.IsEmpty(manager.GetCompanies());
        }

        [Test]
        public void EmptySearchByLongNameInRootCategoryInAllCities()
        {
            manager
                .OpenPage("/primorskij-kraj")
                .SearchBy("Lorem ipsum dolor sit amet, consectetur adipiscing elit. In viverra, massa sit amet rutrum laoreet, leo sem iaculis nisl, sit amet lacinia lacus leo et orci. Morbi at est vitae quam tristique euismod sit amet et lorem. Sed malesuada blandit massa, quis rhoncus eros ornare et. Vivamus maximus facilisis enim, vitae rhoncus erat ultrices et. Nulla vel viverra purus, vulputate posuere dui. Nam et nulla placerat purus malesuada tincidunt. Duis id neque et arcu egestas vestibulum id et purus. Etiam ju");

            Assert.IsEmpty(manager.GetCompanies());
        }

        [Test]
        public void EmptySearchByProductInCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok/education-work/universities-colleges")
                .SearchBy("Вязаная шапка с бубоном");

            Assert.IsEmpty(manager.GetCompanies());
        }

        [Test]
        public void EmptySearchByServiceInCategoryInAllCities()
        {
            manager
                .OpenPage("/primorskij-kraj/cafe/restaurants")
                .SearchBy("вегетарианские чебуреки на заказ");

            Assert.IsEmpty(manager.GetCompanies());
        }
    }
}
