using NUnit.Framework;

namespace vl_web_tests
{
    public class TestBase
    {
        protected Manager manager;

        [SetUp]
        public void SetupManager()
        {
            manager = Manager.GetInstance();
        }
    }
}
