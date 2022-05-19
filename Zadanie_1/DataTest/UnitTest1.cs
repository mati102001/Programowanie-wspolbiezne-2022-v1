using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTest
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