namespace Factoring
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class Sieve
    {
        public List<BigInteger> Primes
        {
            get
            {
                return primes;
            }
        }

        private List<BigInteger> primes = new List<BigInteger>();

        public BigInteger[] SievePrimes()
        {
            var startInteger = Primes[Primes.Count - 1] + 2;
            var endInteger = BigInteger.Min(startInteger * startInteger, startInteger + new BigInteger(10000000));
            var numbers = new Dictionary<BigInteger, bool>();
            for (var i = startInteger; i < endInteger; i++)
            {
                numbers.Add(i, true);
            }

            var maxRequiredPrime = endInteger.Sqrt();

            foreach (var prime in Primes)
            {
                if (prime > maxRequiredPrime) break;

                var modulus = startInteger % prime;
                var multiple = startInteger - modulus + prime;
                while (multiple < endInteger)
                {
                    numbers[multiple] = false;
                    multiple += prime;
                }
            }

            return numbers.Where(a => a.Value).Select(a => a.Key).ToArray();
        }
    }
}
