namespace Factoring.Test
{
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QuadraticSieveTest
    {
        [TestMethod]
        public void TestFindResidue()
        {
            BigInteger product = 15347;
            var squareRoot = product.Sqrt();

            // X ~= 15347 ** 0.5 - 124 ~= 1 (mod 2)
            BigInteger i;
            for (i = 1; i < 20 * squareRoot; i++)
            {
                var mod1 = (i * i) % product;
                var productMinusI = product - i;
                var mod2 = ((productMinusI) * (productMinusI)) % product;

                Assert.AreEqual(mod1, mod2);
                var result = BigInteger.GreatestCommonDivisor(15119, 912);
            }
        }
    }
}
