namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class SmoothFactors
    {
        public SmoothFactors()
        {
            this.factorsList = new SortedList<BigInteger, List<BigInteger>>();
        }
        
        public static Sieve Sieve
        {
            get
            {
                if (sieve == null)
                {
                    sieve = new Sieve();
                    sieve.Primes.AddRange(new BigInteger[] {2, 3, 5, 7, 11, 13, 17, 19 });
                }
                return sieve;
            }
        }

        private static Sieve sieve;

        public BigInteger Bounds    
        {
            get
            {
                return this.bounds;
            }
            set
            {
                this.bounds = value;
                this.MagicNumber = 1;
                while (Sieve.Primes.Last() < value)
                {
                    Sieve.Primes.AddRange(Sieve.SievePrimes());
                }
                int i = 0;
                while (Sieve.Primes[i] < value)
                {
                    this.MagicNumber *= Sieve.Primes[i];
                    i++;
                }
            }
        }

        public BigInteger MagicNumber { get; set; }

        public bool CheckPrimeMultiplierDisabled { get; set; }

        public SortedList<BigInteger, List<BigInteger>> Factors
        {
            get
            {
                return factorsList;
            }
        }

        public Random Generator
        {
            get
            {
                return this.generator ?? (this.generator = new Random());
            }
            set
            {
                generator = value;
            }
        }

        public BigInteger Product { get; set; }

        public BigInteger SquareRoot
        {
            get
            {
                if (!squareRoot.HasValue)
                {
                    squareRoot = Product.Sqrt();
                }
                return squareRoot.Value;
            }
        }

        public Dictionary<BigInteger, BigInteger> SmoothNumbers
        {
            get
            {
                return this.smoothNumbers ?? (this.smoothNumbers = new Dictionary<BigInteger, BigInteger>());
            }
        }

        public bool TakeStep()
        {
            var number = NextIntegerToCheck();
            var numberSquared = BigInteger.ModPow(number, 2, Product);

            var remainder = numberSquared;
            var gcd = BigInteger.GreatestCommonDivisor(this.MagicNumber, numberSquared);
            while (remainder > 1)
            {
                var reduceGcd = BigInteger.GreatestCommonDivisor(this.MagicNumber, remainder);
                if (reduceGcd == 1)
                {
                    break;
                }
                remainder = remainder / reduceGcd;
            }

            if (remainder == 1 && gcd > 1 && (this.Factors.Count == 0 || gcd < this.Factors.Last().Key))
            {
                if (!this.Factors.ContainsKey(gcd))
                {
                    this.Factors.Add(gcd, new List<BigInteger>());
                }

                this.Factors[gcd].Add(numberSquared);

                while (this.Factors.Count > 20)
                {
                    this.Factors.RemoveAt(this.Factors.Count - 1);
                }
            }

            return false;
        }

        #region Implementation

        private readonly SortedList<BigInteger, List<BigInteger>> factorsList;

        private Random generator;

        private Dictionary<BigInteger, BigInteger> smoothNumbers;

        private BigInteger? squareRoot;

        private BigInteger minimum;

        private BigInteger maximum;

        private BigInteger difference;

        private BigInteger bounds;

        protected virtual BigInteger NextIntegerToCheck()
        {
            if (this.minimum == 0)
            {
                this.minimum = SquareRoot.Sqrt();
                this.maximum = 2 * SquareRoot;
                this.difference = maximum - minimum;
            }

            var maxAsString = this.difference.ToString();
            var requiredLength = maxAsString.Length;
            var firstDigit = int.Parse(maxAsString[0].ToString(CultureInfo.InvariantCulture));

            return this.RandomInteger(requiredLength, firstDigit) + this.minimum;
        }

        protected BigInteger RandomInteger(int requiredLength, int firstDigit)
        {
            var sb = new StringBuilder();
            while (sb.Length < requiredLength)
            {
                var digit = this.Generator.Next(9);
                if (sb.Length == requiredLength - 1)
                {
                    if (digit < firstDigit)
                    {
                        sb.Insert(0, digit.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    sb.Insert(0, digit.ToString(CultureInfo.InvariantCulture));
                }
            }
            return BigInteger.Parse(sb.ToString()) + this.SquareRoot;
        }

        #endregion
    }
}
