using NUnit.Framework;

namespace Logic
{
    public abstract class LogicApiTest
    {
       
        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public void CreateBallsTests()
        {
            Assert.AreEqual(0, api.GetBalls().Count);
            api.CreateBalls(3);
            Assert.AreEqual(3, api.GetBalls().Count);
        }
    }
