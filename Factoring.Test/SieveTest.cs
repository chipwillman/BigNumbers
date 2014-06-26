namespace Factoring.Test
{
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SieveTest
    {
        [TestMethod]
        public void TestGeneratePrimeList()
        {
            var sieve = new Sieve();
            sieve.Primes.Add(2);
            sieve.Primes.Add(3);
            sieve.Primes.Add(5);
            sieve.Primes.Add(7);
            sieve.Primes.Add(11);

            var newPrimes = sieve.SievePrimes();

            Assert.AreEqual(13, newPrimes[0]);

            sieve.Primes.AddRange(newPrimes);

            var largerPrimes = sieve.SievePrimes();

            Assert.AreEqual(3069, largerPrimes.Length);

            sieve.Primes.AddRange(largerPrimes);

            var hugePrimes = sieve.SievePrimes();

            Assert.AreEqual(661473, hugePrimes.Length);

            sieve.Primes.AddRange(hugePrimes);

            hugePrimes = sieve.SievePrimes();

            sieve.Primes.AddRange(hugePrimes);

            hugePrimes = sieve.SievePrimes();
        }
    }
}
