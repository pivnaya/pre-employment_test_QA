using NUnit.Framework;

namespace vl_web_tests
{
    [TestFixture]
    public class SectionTests : TestBase
    {
        [Test]
        public void GoToFirstLevelSectionInVladivostok()
        {
            manager
                .OpenPage("/vladivostok")
                .ChooseMainSection("Общественное питание");

           Assert.AreEqual("Общественное питание", manager.GetSectionTitle());
        }

        [Test]
        public void GoToSecondLevelSectionInAllCities()
        {
            manager
                .OpenPage("/primorskij-kraj")
                .ChooseMinorSection("Красота и уход", "Солярии");

            Assert.AreEqual("Солярии", manager.GetSectionTitle());
        }

        [Test]
        public void GoToThirdLevelSectionInArtem()
        {
            manager
                .OpenPage("/artem")
                .ChooseMinorSection("Магазины", "Чай и кофе");

            Assert.AreEqual("Чай и кофе", manager.GetSectionTitle());
        }
    }
}
