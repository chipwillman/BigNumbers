namespace Factoring.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DixonFactorizationTest
    {

        // 59 digits, factors p=200...437 and q=357...017 have both 30 digits)
        //  357440504101388365610785389017
        //  200429218120815554269743635437
        //
        public const string RSA59 = "71641520761751435455133616475667090434063332228247871795429";

        [TestMethod]
        public void TestDixonFactorsOf84923()
        {
            var p1 = new BigInteger(163);
            var p2 = new BigInteger(521);
            var product = p1 * p2;
            var random = new Random();

            var dixonFactorizer = new DixonFactorization { Product = product, Generator = random };

            int maxIterations = 5000;
            int i = 0;

            for (; i < maxIterations; i++)
            {
                if (dixonFactorizer.TakeStep()) break;
            }

            Assert.AreEqual(163, dixonFactorizer.Factors[0]);
            Assert.AreEqual(521, dixonFactorizer.Factors[1]);
        }

        [TestMethod]
        public void TestDixonFactorsOf1111219times1298489()
        {
            var p1 = BigInteger.Parse("1111219");
            var p2 = BigInteger.Parse("1298489");
            var product = p1 * p2;
            var random = new Random();

            var dixonFactorizer = new DixonFactorization { Product = product, Generator = random, CheckPrimeMultiplierDisabled = false, Bounds = 37 };

            int maxIterations = 5000000;
            int i = 0;

            for (; i < maxIterations; i++)
            {
                if (dixonFactorizer.TakeStep()) break;
            }

            Assert.AreEqual(1111219, dixonFactorizer.Factors[0]);
            Assert.AreEqual(new BigInteger(1298489), dixonFactorizer.Factors[1]);
        }

        [TestMethod]
        public void TestDixonFactorsOf79967times103079()
        {
            var p1 = BigInteger.Parse("79967");
            var p2 = BigInteger.Parse("103079");
            var product = p1 * p2;
            var random = new Random();

            var dixonFactorizer = new SearchParabolicRegions { Product = product, Generator = random, CheckPrimeMultiplierDisabled = true, Bounds = 110};

            int maxIterations = 50000;
            int i = 0;

            for (; i < maxIterations; i++)
            {
                if (dixonFactorizer.TakeStep()) break;
            }

            Assert.AreEqual(p1, dixonFactorizer.Factors[0]);
            Assert.AreEqual(p2, dixonFactorizer.Factors[1]);
        }

        [TestMethod]
        public void TestParabolicSearchOfRSA59()
        {
            var p1 = BigInteger.Parse("357440504101388365610785389017");
            var p2 = BigInteger.Parse("200429218120815554269743635437");
            var product = p1 * p2;

            var random = new Random();

            var dixonFactorizer = new SearchParabolicRegions { Product = product, Generator = random, CheckPrimeMultiplierDisabled = true, Bounds = BigInteger.Pow(2, 26) };

            int maxIterations = 5000000;
            int i = 0;

            for (; i < maxIterations; i++)
            {
                if (dixonFactorizer.TakeStep()) break;
            }

            Assert.AreEqual(p1, dixonFactorizer.Factors[0]);
            Assert.AreEqual(p2, dixonFactorizer.Factors[1]);
        }

        [TestMethod]
        public void TestDixonFactorsOfRSA59()
        {
            var random = new Random();

            var dixonFactorizer = new RandomParabolicSearch { Product = BigInteger.Parse(RSA59), Generator = random, Bounds = 1019, Iterations = 1000000 };

            BigInteger maxIterations = BigInteger.Parse("500000000");
            BigInteger i = 0;

            for (; i < maxIterations; i++)
            {
                if (dixonFactorizer.TakeStep()) break;
            }

            Assert.AreEqual(BigInteger.Parse("357440504101388365610785389017"), dixonFactorizer.Factors[1]);
            Assert.AreEqual(BigInteger.Parse("200429218120815554269743635437"), dixonFactorizer.Factors[0]);
        }

        [TestMethod]
        public void TestQuadradicResidue()
        {
            var rsa59 = BigInteger.Parse(RSA59);
            var rsa59SquareRoot = rsa59.Sqrt();
            BigInteger targetSquare = 1;

            for (BigInteger i = 1; i < 100000000; i ++)
            {
                targetSquare = i * i;
                var productWithDesiredSquare = rsa59 + targetSquare;
                var factor = new Factor { Product = productWithDesiredSquare };
                int j;
                for (j = 0; j < 1000; j++)
                {
                    if (factor.FastFactor())
                    {
                        break;
                    }
                }

                if (factor.Factors.Length > 1)
                {
                    BigInteger product = 1;
                    foreach (var foundFactor in factor.Factors)
                    {
                        product *= foundFactor;
                    }
                    if (product > rsa59SquareRoot || factor.Factors.Length > 10)
                    {
                        if (product > rsa59SquareRoot || factor.Product == 1)
                        {
                            if (factor.Product == 1)
                            {
                                Assert.AreEqual(4, factor.Factors.Length);
                            }
                        }
                    }
                }
            }
        }


        [TestMethod]
        public void TestFindFactorsOf84923()
        {
            var p1 = new BigInteger(163);
            var p2 = new BigInteger(521);

            var product = p1 * p2;

            var offset = product.Sqrt();

            var max = (product - offset) / 16;

            var random = new Random(1597);

            var congruences = new Congruences();

            var bounds = 7;
            var MaxIterations = 1000;

            for (int i = 1; i < 10000; i++)
            {
                var number = BigIntegerRandom(max, random) + offset;
                var modulusSquared = BigInteger.Pow(number, 2) % product;
                var factor = new Factor { Product = modulusSquared };

                for (int j = 0; j < MaxIterations; j++)
                {
                    if (factor.FastFactor()) break;
                }

                var smooth = factor.Factors.All(f => f <= bounds);

                if (smooth && factor.Product == 1)
                {
                    congruences.AddCongruence(number, modulusSquared, factor.Factors);
                }
            }
            Assert.IsNotNull(congruences.Values.FirstOrDefault(c => c.Key == 513));

            Dictionary<BigInteger, int> sumOfSquares = null;
            BigInteger sumOfSquaresfactor1 = 0;
            BigInteger sumOfSquaresfactor2 = 0;
            BigInteger[] factorsList1 = null;
            BigInteger[] factorsList2 = null;

            for (int i = 0; i < congruences.Values.Keys.Count; i++)
            {
                sumOfSquaresfactor1 = congruences.Values.Keys[i];
                var value1 = congruences.Values[sumOfSquaresfactor1];
                factorsList1 = congruences.Factors[value1[0]];

                for (int j = i + 1; j < congruences.Values.Keys.Count; j++)
                {
                    sumOfSquaresfactor2 = congruences.Values.Keys[j];
                    var value2 = congruences.Values[sumOfSquaresfactor2];
                    factorsList2 = congruences.Factors[value2[0]];
                    var combinedList = CombineLists(factorsList1, factorsList2);
                    if (SumOfSquaresFound(combinedList))
                    {
                        Assert.IsTrue(combinedList.Count > 0);
                        sumOfSquares = combinedList;
                        break;
                    }
                }
                if (sumOfSquares != null) break;
            }

            Assert.IsNotNull(sumOfSquares);

            var square1 = (sumOfSquaresfactor1 * sumOfSquaresfactor2) % product;
            var square2 = CalculateSquareRoot(sumOfSquares);

            var factor1 = BigInteger.GreatestCommonDivisor((BigInteger.Max(square1, square2) - BigInteger.Min(square1, square2)), product);
            var factor2 = BigInteger.GreatestCommonDivisor((BigInteger.Max(square1, square2) + BigInteger.Min(square1, square2)), product);

            Assert.AreEqual(163, BigInteger.Min(factor1, factor2));
            Assert.AreEqual(521, BigInteger.Max(factor1, factor2));
        }

        private BigInteger CalculateSquareRoot(Dictionary<BigInteger, int> evenSquares)
        {
            var halfPower = evenSquares.ToDictionary(pair => pair.Key, pair => pair.Value / 2);
            BigInteger result = 1;
            foreach (var pair in halfPower)
            {
                result *= BigInteger.Pow(pair.Key, pair.Value);
            }

            return result;
        }

        private bool SumOfSquaresFound(Dictionary<BigInteger, int> factors)
        {
            return factors.All(pair => pair.Value % 2 == 0);
        }

        private Dictionary<BigInteger, int> CombineLists(BigInteger[] factorsList1, BigInteger[] factorsList2)
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

        public BigInteger BigIntegerRandom(BigInteger max, Random random)
        {
            var maxAsString = max.ToString();
            var requiredLength = maxAsString.Length;
            var firstDigit = int.Parse(maxAsString[0].ToString());

            var sb = new StringBuilder();
            while (sb.Length < requiredLength)
            {
                var digit = random.Next(9);
                if (sb.Length == requiredLength - 1)
                {
                    if (digit < firstDigit)
                    {
                        sb.Insert(0, digit.ToString());
                    }
                }
                else
                {
                    sb.Insert(0, digit.ToString());
                }
            }
            return BigInteger.Parse(sb.ToString());
        }
    }
}
