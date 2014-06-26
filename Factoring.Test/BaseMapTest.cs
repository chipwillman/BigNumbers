namespace Factoring.Test
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BaseMapTest
    {
        // 59 digits, factors p=200...437 and q=357...017 have both 30 digits)
        //  357440504101388365610785389017
        //  200429218120815554269743635437
        //
        public const string RSA59 = "71641520761751435455133616475667090434063332228247871795429";

        [TestMethod]
        public void TestGenerateMap()
        {
            var smallestdigit = new BaseMap(12);
            smallestdigit.Product = BigInteger.Parse(RSA59);

            smallestdigit.GenerateMap();
            
            Assert.AreEqual(2, smallestdigit.NextDigit.Length);
            var nextDigit1 = smallestdigit.NextDigit[0];
            Assert.AreEqual(1, nextDigit1.Digit);

            smallestdigit.IterateMap();

            nextDigit1 = smallestdigit.NextDigit[0];
            Assert.IsNotNull(nextDigit1.Map.NextDigit[0]);

            var possibleFactors = smallestdigit.PossibleFactors();
            var digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(24, possibleFactors);
            Assert.AreEqual(2, digitDepth);

            smallestdigit.IterateMap();
            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(288, possibleFactors);
            Assert.AreEqual(3, digitDepth);

            smallestdigit.IterateMap();

            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(3456, possibleFactors);
            Assert.AreEqual(4, digitDepth);

            smallestdigit.IterateMap();

            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(41472, possibleFactors);
            Assert.AreEqual(5, digitDepth);

            smallestdigit.IterateMap();

            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(497664, possibleFactors);
            Assert.AreEqual(6, digitDepth);

            smallestdigit.IterateMap();

            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(5971968, possibleFactors);
            Assert.AreEqual(7, digitDepth);

            smallestdigit.IterateMap();

            possibleFactors = smallestdigit.PossibleFactors();
            digitDepth = smallestdigit.DigitDepth();
            Assert.AreEqual(71660736, possibleFactors);
            Assert.AreEqual(8, digitDepth);
        }

        private BigInteger Gcd(BigInteger x, BigInteger y)
        {
            var result = 1;
            while (x.IsEven && y.IsEven)
            {
                result *= 2;
                x = x / 2;
                y = y / 2;
            }

            while (y != 0 && x != 0)
            {
                if (y.IsEven)
                {
                    y = y / 2;
                }
                else if (x.IsEven)
                {
                    x = x / 2;
                }
                else
                {
                    if (x > y)
                    {
                        x = BigInteger.Abs(x - y) / 2;
                    }
                    else
                    {
                        y = BigInteger.Abs(x - y) / 2;
                    }
                }
                //r = x % y;
                //x = y;
                //y = r;
            }
            return result * (x + y);
        }

        [TestMethod]
        public void GenerateDifferencesList()
        {
            var p1 = BigInteger.Parse("113");
            var p2 = BigInteger.Parse("59");

            var product = p1 * p2;
            var end = product / 2;
            //var sqrtPlus1 = product.Sqrt() + 1;
            var sb = new StringBuilder();
            for (int i = 7; i <= end; i++)
            {
                var low = product / i;
                var high = low + 1;
                var under = product - (i * low);
                var over = (i * high) - product;
                var gcd = this.Gcd(over, under);
                BigInteger mod1;
                BigInteger mod2;
                if (gcd != 1)
                {
                    mod1 = under % gcd;
                    mod2 = over % gcd;
                }
                else if (over != 0 && under != 0)
                {
                    mod1 = i % under;
                    mod2 = i % over;
                }
                else
                {
                    mod1 = 1;
                    mod2 = 1;
                }

                //if (mod1 == 0 && mod2 == 0) //(over != 1 && under != 1 && mod == 0) || (mod > 9 && under % mod == 0 && over % mod == 0) || (final1 > 7 && (final1 == final2 || final2 == 0) && mod % final1 == 0))
                {
                    //sb.Append("Multiple Found\n");
                    if (over != 0)
                    {
                        mod2 = i % over;
                    }

                    if (under != 0)
                    {
                        mod1 = i % under;
                    }
                    double remainder = 0.0;
                    if (over != 0 && under != 0)
                    {
                        remainder = (double)over / (double)under;

                    }
                    sb.Append(string.Format("{0}\t{1}\t{2}\t\t{3}\t{4}\t\t{5}\t{6}\t{7}\t\t{8}\t{9}\n", i, low, high, under, over, gcd, mod1, mod2, BigInteger.Max(mod1, mod2) % BigInteger.Max(1, BigInteger.Min(mod1, mod2)), remainder));
                }
            }

            Assert.IsFalse(string.IsNullOrEmpty(sb.ToString()));
        }

        //  357440504101388365610785389017
        //  200429218120815554269743635437

        [TestMethod]
        public void TestRandomSearchForMultiples()
        {
            var p1 = BigInteger.Parse("1111219");
            var p2 = BigInteger.Parse("1298489");
            var product = p1 * p2;
            var end = product.Sqrt() * 10;
            //var maxCongruence = 23; // product / (1024 * 1024);

            var random = new Random();
            int iteration = 0;
            var length = end.ToString().Length;

            var congruences = new Congruences();

            var result = new BigInteger(0);

            while (iteration < 10000000)
            {
                //var rand = random.Next((int)end);
                var sb = new StringBuilder();
                while (sb.Length < length)
                {
                    sb.Append(random.Next(9).ToString());
                }

                var under = new BigInteger(0);
                var over = new BigInteger(0);
                BigInteger mod1 = 0;
                BigInteger mod2 = 0;
                var rand = BigInteger.Parse(sb.ToString());

                //for (int i = -5; i <= 5; i++)
                {
                    int i = 0;
                    var test = rand + i;
                    var low = product / test;
                    var high = low + 1;
                    under = product - (test * low);
                    over = (test * high) - product;

                    var gcd = this.Gcd(over, under);
                    if (gcd != 1)
                    {
                        mod1 = under % gcd;
                        mod2 = over % gcd;
                        if (mod1 < 111 || mod1 == mod2)
                        {
                            congruences.AddCongruence(mod1, test);
                        }
                    }
                    else if (over != 0 && under != 0)
                    {
                        mod1 = under % over;
                        mod2 = over % under;
                        if (mod1 < 111 || mod1 == mod2)
                        {
                            congruences.AddCongruence(mod1, test);
                        }
                    }
                    else
                    {
                        mod1 = 1;
                        mod2 = 1;
                    }

                    if (mod1 == 0 && mod2 == 0)
                    {
                        result = gcd;
                        break;
                    }
                }
                if (mod1 == 0 && mod2 == 0)
                {
                    break;
                }
                iteration++;
            }

            Assert.IsTrue(p1 == result || p2 == result || result == 0) ;

            var candidates = congruences.LikelyCandidates();

            Assert.IsTrue(iteration < 100000000);

            foreach (var candidateKey in candidates.Keys)
            {
                var candidateList = candidates[candidateKey];
                var sortedList = new SortedList<BigInteger, BigInteger>();
                foreach (var item in candidateList)
                {
                    sortedList.Add(item, item);
                }

                for (var i = sortedList.Count - 1; i >= 1; i--)
                {
                    var n1 = sortedList[sortedList.Keys[i - 1]];
                    var n2 = sortedList[sortedList.Keys[i]];

                    //if (BigInteger.Abs(n1 - n2) == new BigInteger(1))
                    //{
                    //    var mod = n1 % candidateKey;
                    //    Assert.AreEqual(0, mod);
                    //}
                    //else
                    {
                        var test = n2 - (n2 - n1) / 2;
                        var low = product / test;
                        var high = low + 1;
                        var under = product - (test * low);
                        var over = (test * high) - product;
                        var mod = over == 0 || under == 0 ? 0 : under > over ? under % over : over % under;
                        if ((over != 1 && under != 1 && mod == 0) || (mod > 9 && under % mod == 0 && over % mod == 0))
                        {
                            Assert.AreEqual(0, mod);
                        }
                    }
                }
            }
        }
    }
}
