namespace Factoring.Test
{
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PointsOfModulusInterestTest
    {
        [TestMethod]
        public void TestSquareRootPointOfSquare()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(23 * 23), 1);
            Assert.AreEqual(pointOfInterest.Modulus, 0);
            Assert.AreEqual(pointOfInterest.X, 23);
        }

        [TestMethod]
        public void TestOneToTwoPointOfInterest()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 23), 1.0 / 2);
            Assert.AreEqual(27, pointOfInterest.X);
            Assert.AreEqual(13, pointOfInterest.Modulus);
        }

        [TestMethod]
        public void TestTwoToOnePointOfInterest()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 23), 2.0);
            Assert.AreEqual(13, pointOfInterest.X);
            Assert.AreEqual(1, pointOfInterest.Modulus);
        }


        [TestMethod]
        public void TestThreeToTwoPointOfInterest()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 23), 3.0 / 2);
            Assert.AreEqual(16, pointOfInterest.X);
            Assert.AreEqual(7, pointOfInterest.Modulus);
        }

        [TestMethod]
        public void TestTwoToThreePointOfInterest()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 23), 2.0 / 3);
            Assert.AreEqual(24, pointOfInterest.X);
            Assert.AreEqual(7, pointOfInterest.Modulus);
        }

        [TestMethod]
        public void TestThreeToTwoPointOfInterest2()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 29), 3.0 / 2);
            Assert.AreEqual(18, pointOfInterest.X);
            Assert.AreEqual(7, pointOfInterest.Modulus);
        }

        [TestMethod]
        public void TestTwoToThreePointOfInterest2()
        {
            var pointOfInterest = new PointOfModulusInterest(new BigInteger(17 * 29), 2.0 / 3);
            Assert.AreEqual(27, pointOfInterest.X);
            Assert.AreEqual(7, pointOfInterest.Modulus);
        }


    }
}
