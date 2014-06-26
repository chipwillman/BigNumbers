namespace Factoring.Test
{
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrimesTest
    {
        [TestMethod]
        public void TestGetNextPrime()
        {
            var prime = 2;
            var nextPrime = Primes.GetNextPrime(prime);
            Assert.AreEqual(3, nextPrime);
        }

        // Product of the first 20 primes
        // Product       557940830126698960967415390
        // Next Prime is 557940830126698960967415493
        [TestMethod]
        public void TestRunOfNonPrimeNumbers()
        {
            BigInteger startNumber = 1;
            BigInteger largestPrime = 2;
            for (int i = 0; i < 20; i++)
            {
                var prime = Primes.GetNthPrime(i);
                startNumber *= prime;
                largestPrime = prime;
            }

            for (int i = 0; i < largestPrime; i++)
            {
                var nonPrimeNumber = startNumber + i;
                var factorer = new Factor { Product = nonPrimeNumber };
                for (int j = 0; j < 1000; j++)
                {
                    if (factorer.FastFactor()) break;
                }
                Assert.IsTrue(factorer.Factors.Length >= 2, factorer.Product.ToString());
            }
        }
    }
}
