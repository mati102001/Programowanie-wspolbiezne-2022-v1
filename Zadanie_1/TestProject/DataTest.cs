using NUnit.Framework;

namespace Data
{
    public class DataTest
    {
        private DataAbstractApi api;

        [Test]
        public void CreateDataApiTest()
        {
            api = DataAbstractApi.CreateDataLayer();
            Assert.IsNotNull(api);
        }
    }
}