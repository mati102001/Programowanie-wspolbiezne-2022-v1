using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using System.Collections;

namespace LogicTest
{
    [TestClass]
    public class LogicTestApi
    {
        private LogicAPI api;
        private IList list;
        [TestMethod]
        public void Test_Constructor_Create()
        {
            api = LogicAPI.CreateBallAPI();
            Assert.IsNotNull(api);
        }

        [TestMethod]
        public void Create_IList()
        {
            api = LogicAPI.CreateBallAPI();
            list = api.CreateBalls(10, 450, 500);
            Assert.IsNotNull(list);
            Assert.AreEqual(10, list.Count);
        }
    }
}
