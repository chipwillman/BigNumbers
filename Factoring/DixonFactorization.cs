namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class DixonFactorization
    {
        public DixonFactorization()
        {
            Bounds = 7;
            factorsList = new List<BigInteger>();
        }

        public BigInteger Bounds { get; set; }
        public bool CheckPrimeMultiplierDisabled { get; set; }

        public BigInteger[] Factors
        {
            get
            {
                return factorsList.ToArray();
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

        public Dictionary<BigInteger, BigInteger[]> SmoothFactors
        {
            get
            {
                return this.smoothFactors ?? (this.smoothFactors = new Dictionary<BigInteger, BigInteger[]>());
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

            var modulus = Product % number;
            if (modulus == 0)
            {
                this.AddFactor(number, Product / number);
                return true;
            }

            if (!this.CheckPrimeMultiplierDisabled && CheckPrimeMultiple(number))
            {
                return true;
            }

            var numberSquared = BigInteger.ModPow(number, 2, Product);

            var factor = new Factor { Product = numberSquared };

            while (factor.Product != 1 && factor.LastX <= Bounds)
            {
                if (factor.FastFactor()) break;
            }

            var smooth = factor.Factors.All(f => f <= Bounds);
            if (smooth && factor.Product == 1)
            {
                CheckCongruence(number, numberSquared, factor.Factors);
            }

            return Factors.Length > 0;
        }

        #region Implementation

        private readonly List<BigInteger> factorsList;

        private Random generator;

        private Dictionary<BigInteger, BigInteger> smoothNumbers;

        private Dictionary<BigInteger, BigInteger[]> smoothFactors;

        private BigInteger? squareRoot;

        protected virtual BigInteger NextIntegerToCheck()
        {
            var max = Product - SquareRoot;
            var maxAsString = max.ToString();
            var requiredLength = maxAsString.Length;
            var firstDigit = int.Parse(maxAsString[0].ToString(CultureInfo.InvariantCulture));

            return this.RandomInteger(requiredLength, firstDigit);
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

        private bool CheckPrimeMultiple(BigInteger number)
        {
            var div = Product / number;
            var under = Product - (number * div);
            var over = (number * (div + 1)) - Product;
            var gcd = BigInteger.GreatestCommonDivisor(under, over);
            if (gcd > 1)
            {
                this.AddFactor(gcd, Product / gcd);
                return true;
            }
            return false;
        }

        private BigInteger CalculateSquareRoot(Dictionary<BigInteger, int> evenSquares)
        {
            var halfPower = evenSquares.ToDictionary(pair => pair.Key, pair => pair.Value / 2);

            return halfPower.Aggregate<KeyValuePair<BigInteger, int>, BigInteger>(1, (current, pair) => current * BigInteger.Pow(pair.Key, pair.Value));
        }

        private void CheckCongruence(BigInteger number, BigInteger numberSquared, BigInteger[] factors)
        {
            AddModulusSquare(number, numberSquared % Product, factors);

            Dictionary<BigInteger, int> sumOfSquares = null;
            BigInteger sumOfSquaresfactor1 = 0;
            BigInteger sumOfSquaresfactor2 = 0;

            for (int i = 0; i < SmoothNumbers.Keys.Count; i++)
            {
                sumOfSquaresfactor1 = SmoothNumbers.Keys.ToArray()[i];
                var value1 = SmoothNumbers[sumOfSquaresfactor1];
                BigInteger[] factorsList1 = SmoothFactors[value1];

                for (int j = i + 1; j < SmoothNumbers.Keys.Count; j++)
                {
                    sumOfSquaresfactor2 = SmoothNumbers.Keys.ToArray()[j];
                    var gcd = BigInteger.GreatestCommonDivisor(sumOfSquaresfactor1, sumOfSquaresfactor2);
                    if (gcd == 1)
                    {
                        var value2 = SmoothNumbers[sumOfSquaresfactor2];
                        BigInteger[] factorsList2 = SmoothFactors[value2];
                        var combinedList = CombineLists(factorsList1, factorsList2);
                        if (SumOfSquaresFound(combinedList))
                        {
                            var square1 = (sumOfSquaresfactor1 * sumOfSquaresfactor2) % Product;
                            var square2 = CalculateSquareRoot(combinedList);

                            if (square1 != square2)
                            {
                                var factor1 =
                                    BigInteger.GreatestCommonDivisor(
                                    (BigInteger.Max(square1, square2) - BigInteger.Min(square1, square2)), Product);
                                var factor2 =
                                    BigInteger.GreatestCommonDivisor(
                                        (BigInteger.Max(square1, square2) + BigInteger.Min(square1, square2)), Product);
                                if (factor1 != 1 && factor2 != 1)
                                {
                                    this.AddFactor(factor1, factor2);
                                    sumOfSquares = combinedList;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (sumOfSquares != null) break;
            }

            if (sumOfSquares != null)
            {
            }

        }

        private void AddFactor(BigInteger factor1, BigInteger factor2)
        {
            this.factorsList.Add(BigInteger.Min(factor1, factor2));
            this.factorsList.Add(BigInteger.Max(factor1, factor2));
        }

        private bool SumOfSquaresFound(Dictionary<BigInteger, int> factors)
        {
            return factors.All(pair => pair.Value % 2 == 0);
        }

        private Dictionary<BigInteger, int> CombineLists(IEnumerable<BigInteger> factorsList1, IEnumerable<BigInteger> factorsList2)
        {
            var result = new Dictionary<BigInteger, int>();
            foreach (var factor in factorsList1)
            {
                if (result.ContainsKey(factor))
                {
                    result[factor] += 1;
                }
                else
                {
                    result.Add(factor, 1);
                }
            }

            foreach (var factor in factorsList2)
            {
                if (result.ContainsKey(factor))
                {
                    result[factor] += 1;
                }
                else
                {
                    result.Add(factor, 1);
                }
            }

            return result;
        }

        private void AddModulusSquare(BigInteger number, BigInteger squaredModulus, BigInteger[] factors)
        {
            if (!SmoothNumbers.ContainsKey(number))
            {
                SmoothNumbers.Add(number, squaredModulus);
            }
            if (!SmoothFactors.ContainsKey(squaredModulus))
            {
                SmoothFactors.Add(squaredModulus, factors);
            }
        }

        #endregion

    }
}
