using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
    [TestClass]
    public class Unit_Test_1
    {

        [TestMethod]
        public void Test_Add()
        {
            ClassLibrary1.Calculator x = new ClassLibrary1.Calculator();
            Assert.AreEqual(x.Add(2.5,1), 3.5);
            Assert.AreEqual(x.Add(123.5, 123.5), 247);
            Assert.AreEqual(x.Add(-10, 5), -5);
        }

        [TestMethod]
        public void Test_Subtract()
        {
            ClassLibrary1.Calculator y = new ClassLibrary1.Calculator();
            Assert.AreEqual(y.Subtract(5, 3), 2);
            Assert.AreEqual(y.Subtract(-2, -3), 1);
            Assert.AreEqual(y.Subtract(2, 0), 2);
        }

        [TestMethod]
        public void Test_Multiply()
        {
            ClassLibrary1.Calculator z = new ClassLibrary1.Calculator();
            Assert.AreEqual(z.Multiply(5, 3), 15);
            Assert.AreEqual(z.Multiply(-2, -3), 6);
            Assert.AreEqual(z.Multiply(2, 0), 0);
        }

        [TestMethod]
        public void Test_Divide()
        {
            ClassLibrary1.Calculator v = new ClassLibrary1.Calculator();
            Assert.AreEqual(v.Divide(15, 3), 5);
            Assert.AreEqual(v.Divide(-6, -3), 2);
            Assert.ThrowsException<ArgumentException>(() => v.Divide(-2, 0));
        }

        [TestMethod]
        public void Test_Condemnation()
        {
            ClassLibrary1.Calculator c = new ClassLibrary1.Calculator();
            Assert.AreEqual(c.Condemnation(5, 3), 125);
            Assert.AreEqual(c.Condemnation(5, -2), 0.04);
            Assert.ThrowsException<ArgumentException>(() => c.Condemnation(0,-2));
        }


    }
}