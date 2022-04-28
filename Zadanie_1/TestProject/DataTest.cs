using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data
{
    [TestClass]
    public class DataTest
    {
        private DataAbstractApi api;

        [TestMethod]
        public void CreateDataApiTest()
        {
            api = DataAbstractApi.CreateDataLayer();
            Assert.IsNotNull(api);
        }
    }
}