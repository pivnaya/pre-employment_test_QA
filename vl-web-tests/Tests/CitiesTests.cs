using NUnit.Framework;

namespace vl_web_tests
{
    [TestFixture]
    public class CitiesTests : TestBase
    {
        [Test]
        public void ChooseOtherCityInRootCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .ChooseCity("Артём");

            Assert.AreEqual("/artem", manager.GetCurrentUrl());
        }

        [Test]
        public void ChooseAllCitiesInCategoryInVladivostok()
        {
            manager
                .OpenPage("/vladivostok/furniture-interior/kitchens")
                .ChooseCity("Все города");

            Assert.AreEqual("/primorskij-kraj/furniture-interior/kitchens", manager.GetCurrentUrl());
        }

        [Test]
        public void ChooseOtherCityInRootCategoryInAllCities()
        {
            manager
                .OpenPage("/primorskij-kraj")
                .ChooseCity("Владивосток");

            Assert.AreEqual("/vladivostok", manager.GetCurrentUrl());
        }

    }
}
