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

        [TestMethod]
        public void BoardWidthHeight()
        {
            api = DataAbstractApi.CreateDataLayer();
            Assert.AreEqual(640, api.BoardWidth);
            Assert.AreEqual(320, api.BoardHeight);
        }

        [TestMethod]
        public void CreateBalls()
        {
            api = DataAbstractApi.CreateDataLayer();
            Assert.IsNotNull(api.createBall());
            Assert.IsNotNull(api.createBall());
        }
    }
}