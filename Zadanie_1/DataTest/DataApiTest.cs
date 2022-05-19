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
            api.createBalls(5);
            Assert.IsNotNull(api.GetAll());
            Assert.AreEqual(5, api.Count());
            api.createBalls(10);
            Assert.IsNotNull(api.GetAll());
            Assert.AreEqual(10, api.Count());
        }

        [TestMethod]
        public void GetBallTest()
        {
            api = DataAbstractApi.CreateDataLayer();
            api.createBalls(10);
            for (int i = 0; i < 10; i++)
            Assert.IsNotNull(api.GetBall(i));
        }
    }
}