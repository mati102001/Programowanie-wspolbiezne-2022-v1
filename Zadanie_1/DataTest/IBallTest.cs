using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTest
{
    [TestClass]
    public class IBallTest
    {
        private DataAbstractApi api;

        [TestMethod]
        public void TestSetGet()
        {
            api = DataAbstractApi.CreateDataLayer();
            IBall ball = api.createBall();
            Assert.IsNotNull(ball);
            Assert.IsNotNull(ball.X);
            Assert.IsNotNull(ball.Y);
            Assert.IsNotNull(ball.Weight);
            Assert.IsNotNull(ball.R);
            Assert.IsNotNull(ball.XSpeed);
            Assert.IsNotNull(ball.YSpeed);
            ball.ChangeSpeed(120, 120);
            Assert.Equals(120, ball.XSpeed);
            Assert.Equals(120, ball.YSpeed);
        }

    }
}