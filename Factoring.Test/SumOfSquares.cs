namespace Factoring.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SumOfSquares
    {
        // 59 digits, factors p=200...437 and q=357...017 have both 30 digits)
        //  357440504101388365610785389017
        //  200429218120815554269743635437
        //
        public const string RSA59 = "71641520761751435455133616475667090434063332228247871795429";

        [TestMethod]
        public void TestSquaresUpToSqrt()
        {
            var rsa59 = BigInteger.Parse(RSA59);
            var sqrt = rsa59.Sqrt();
            var candidates = new Dictionary<BigInteger, BigInteger>();

            var current = sqrt / 4;
            var square = current * current;
            while (square < rsa59 && candidates.Count < 1)
            {
                var remainder = rsa59 - square;
                var remSqrt = remainder.Sqrt();
                var modulus = remainder % remSqrt;
                if (modulus == 0)
                {
                    candidates.Add(current, remSqrt);
                }
                current *= BigInteger.Max(1, modulus / current) + 1;
                square = current * current;
            }

            Assert.IsTrue(candidates.Count > 0);
        }

        [TestMethod]
        public void TestLagendreMethod()
        {
            var rsa59 = BigInteger.Parse(RSA59);
            var a = new BigInteger(1);
            var b = new BigInteger(0);
            var c = -rsa59;
            var n = rsa59.Sqrt(); // + 1?

            while (a + c != 0)
            {
                var m = (BigInteger.Abs(b) + n) / BigInteger.Abs(a);
                var ai = a;
                a = ai * m * m + 2 * b * m + c;
                b = ai * m + b;
                c = ai;
            }

            var product = a *a + b * b;
            Assert.AreEqual(rsa59, product);
        }

        [TestMethod]
        public void TestHeathBrown()
        {
            // p = 4 * k + 1
            var rsa59 = BigInteger.Parse(RSA59);// BigInteger.Parse(RSA59);
            Assert.AreEqual(new BigInteger(1), rsa59 % 4);

            var k = (rsa59 - 1) / 4;

            var x = k;
            var y = new BigInteger(1);
            var z = new BigInteger(1);
            while (x != y)
            {
                var xi = x;
                var yi = y;
                var zi = z;
                if (x > (y + z))
                {
                    x = xi - yi - zi;
                    // y = yi;
                    z = 2 * yi + zi;
                }
                else if (x < (y + z))
                {
                    x = yi + zi - xi;
                    y = xi;
                    z = 2 * xi - zi;
                }

                //if (4 * x * y + z * z > 0)
                //{
                //    Assert.AreEqual(x, y);
                //}
            }

            var prime = 4 * x * x + z * z;
            Assert.AreEqual(rsa59, prime);
        }

        [TestMethod]
        public void TestFermatsAnswerOnRSA59()
        {
            var p1 = BigInteger.Parse("200429218120815554269743635437");
            var p2 = BigInteger.Parse("357440504101388365610785389017");
            var rsa59 = BigInteger.Parse(RSA59);

            var difference = p2 - p1;
            var goal = (difference / 2);
            var centerPoint = p2 - goal;
            Assert.AreEqual(p1 + goal, centerPoint);

            for (BigInteger i = centerPoint - 100; i < centerPoint; i++)
            {

                var square = (i * i) - rsa59;

                var remSqrt = square.Sqrt();

                var product = remSqrt * remSqrt;

                var modulus = square % remSqrt;

                Assert.IsTrue(modulus > 0);
            }

            for (BigInteger i = 1; i < 100; i++)
            {
                var product1 = p1 * i;
                var product2 = p2 * i;

                var mod1 = rsa59 % product1;
                var mod2 = rsa59 % product2;

                Assert.IsTrue(mod1 >= 0);
            }
        }

        [TestMethod]
        public void TestgcdOfProductTimesPrime()
        {
            var p1 = 13;
            var p2 = 59;
            var product = p1 * p2;
            var largeprime = 17;
            var multiple = largeprime * product;
            var algo = new ExtendedEuclideanAlgorithm()
                           {
                               Equation =
                                   new CongruentEquation()
                                       {
                                           Modulus = multiple,
                                           Remainder = 17
                                       },
                               Multiple = product
                           };
            var inverseModule = algo.FindInverseModulo();
            Assert.AreNotEqual(0, inverseModule);
        }

        [TestMethod]
        public void TestModulusCrugruenceOfSquares()
        {
            var p1 = BigInteger.Parse("30319");
            var p2 = BigInteger.Parse("48593");
            var product = p1 * p2;
            var sb = new StringBuilder();
            var squareRoot = product.Sqrt();
            int i = 0;
            for (; i < (product - squareRoot)/4; i++)
            {
                var number = squareRoot + i;
                var squareModulus = BigInteger.ModPow(number, 2, product);
                var factor = new Factor() { Product = squareModulus };
                for (int j = 0; j < 5; j++)
                {
                    if (factor.FastFactor()) break;
                }

                if (factor.Product == 1 && factor.Factors.Length > 1)
                {
                    sb.Append(string.Format("b: {0}\tsquare mod: {1}\t", number, squareModulus));
                    if (factor.Product == 1)
                    {
                        sb.Append(string.Format("composit factors: "));
                        foreach (var primeFactor in factor.Factors)
                        {
                            sb.Append(primeFactor + " ");
                        }
                    }

                    if (factor.Product > 1)
                    {
                        //sb.Append(string.Format("? {0}", factor.Product));
                    }
                    sb.Append("\n");
                }
                else
                {
                    //if (factor.Factors.Length > 0 && factor.Product == 1)
                    //{
                    //    sb.Append(string.Format("is prime factors: {0}", factor.Factors[0]));
                    //}
                    //else
                    //{
                    //    if (factor.Factors.Length == 1)
                    //    {
                    //        sb.Append(string.Format("composit factors: {0} {1}", factor.Factors[0], factor.Product));
                    //    }
                    //    else
                    //    {
                    //        sb.Append(string.Format("?? prime factors: {0}", factor.Product));
                    //    }
                    //}
                }
            }
            var result = sb.ToString();
            Assert.IsNotNull(result);
        }
    }
}
