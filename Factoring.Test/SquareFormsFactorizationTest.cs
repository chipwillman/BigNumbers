namespace Factoring.Test
{
    using System;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SquareFormsFactorizationTest
    {
        // 59 digits, factors p=200...437 and q=357...017 have both 30 digits)
        //  357440504101388365610785389017
        //  200429218120815554269743635437
        //
        public const string RSA59 = "71641520761751435455133616475667090434063332228247871795429";

        [TestMethod]
        public void TestFactor11111()
        {
            var p1 = 41;
            var p2 = 271;

            var product = p1 * p2;

            var squareForms = new SquareFormsFactorization() { Product = product };

            for (int i = 0; i < 10000; i++)
            {
                if (squareForms.TakeStep()) break;
            }

            Assert.AreEqual(squareForms.Factors[0], p1);
            Assert.AreEqual(squareForms.Factors[1], p2);
        }

        [TestMethod]
        public void TestFactor98429times67819()
        {
            var p1 = BigInteger.Parse("67819");
            var p2 = BigInteger.Parse("98429");

            var product = p1 * p2;

            var squareForms = new SquareFormsFactorization() { Product = product };
            int i = 0;
            for (; i < 10000000000; i++)
            {
                if (squareForms.TakeStep()) break;
                if (i % 100000 == 0)
                {
                    squareForms.Multiplier++;
                    squareForms.PreviousP = 0;
                    squareForms.PerfectSquareFound = false;
                }
            }

            Assert.AreEqual(squareForms.Factors[0], p1);
            Assert.AreEqual(squareForms.Factors[1], p2);
        }

        //67157  67169  67181  67187  67189  67211  67213  67217  67219  67231 
        //67247  67261  67271  67273  67289  67307  67339  67343  67349  67369 
        //67391  67399  67409  67411  67421  67427  67429  67433  67447  67453 
        //67477  67481  67489  67493  67499  67511  67523  67531  67537  67547 
        //67559  67567  67577  67579  67589  67601  67607  67619  67631  67651 
        //67679  67699  67709  67723  67733  67741  67751  67757  67759  67763 
        //67777  67783  67789  67801  67807  67819  67829  67843  67853  67867 
        //67883  67891  67901  67927  67931  67933  67939  67943  67957  67961 
        //67967  67979  67987  67993  68023  68041  68053  68059  68071  68087 
        //68099  68111  68113  68141  68147  68161  68171  68207  68209  68213

        [TestMethod]
        public void TestFactor98429timesARange()
        {
            var primes = new[]
                             {
                                 67157, 67169, 67181, 67187, 67189, 67211, 67213, 67217, 67219, 67231, 67247, 67261, 67271
                                 , 67273, 67289, 67307, 67339, 67343, 67349, 67369, 67391, 67399, 67409, 67411, 67421,
                                 67427, 67429, 67433, 67447, 67453, 67477, 67481, 67489, 67493, 67499, 67511, 67523, 67531
                                 , 67537, 67547, 67559, 67567, 67577, 67579, 67589, 67601, 67607, 67619, 67631, 67651,
                                 67679, 67699, 67709, 67723, 67733, 67741, 67751, 67757, 67759, 67763, 67777, 67783, 67789
                                 , 67801, 67807, 67819, 67829, 67843, 67853, 67867, 67883, 67891, 67901, 67927, 67931,
                                 67933, 67939, 67943, 67957, 67961, 67967, 67979, 67987, 67993, 68023, 68041, 68053, 68059
                                 , 68071, 68087, 68099, 68111, 68113, 68141, 68147, 68161, 68171, 68207, 68209, 68213
                             };
            var p2 = BigInteger.Parse("98429");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization() { Product = product };
                int i = 0;
                for (; i < 1000000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1} \tIteration: {2}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration));
                Assert.AreEqual(squareForms.Factors[0], p1);
                Assert.AreEqual(squareForms.Factors[1], p2);
            }

            Assert.IsTrue(sb.Length > 0);
        }


        [TestMethod]
        public void TestFactor896723timesARange()
        {
            var primes = new[]
                             {
                                 1294649, 1294651, 1294691, 1294721, 1294723, 1294729, 1294753, 1294757, 1294759, 1294817,
                                 1294823, 1294841, 1294849, 1294939, 1294957, 1294967, 1294973, 1294987, 1294999, 1295003,
                                 1295027, 1295033, 1295051, 1295057, 1295069, 1295071, 1295081, 1295089, 1295113, 1295131,
                                 1295137, 1295159, 1295183, 1295191, 1295201, 1295207, 1295219, 1295221, 1295243, 1295263,
                                 1295279, 1295293, 1295297, 1295299, 1295309, 1295317, 1295321, 1295323, 1295339, 1295347,
                                 1295369, 1295377, 1295387, 1295389, 1295447, 1295473, 1295491, 1295501, 1295513, 1295533,
                                 1295543, 1295549, 1295551
                             };
            var p2 = BigInteger.Parse("896723");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization() { Product = product };
                int i = 0;
                for (; i < 1000000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1} \tIteration: {2}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration));
                Assert.AreEqual(squareForms.Factors[1], p1);
                Assert.AreEqual(squareForms.Factors[0], p2);
            }

            Assert.IsTrue(sb.Length > 0);
        }

        [TestMethod]
        public void TestFactor14648021timesARange()
        {
            var primes = new[]
                             {
                                 15481619, 15481633, 15481657, 15481663, 15481727, 15481733, 15481769, 15481787, 15481793,
                                 15481801, 15481819, 15481859, 15481871, 15481897, 15481901, 15481933, 15481981, 15481993,
                                 15481997, 15482011, 15482023, 15482029, 15482119, 15482123, 15482149, 15482153, 15482161,
                                 15482167, 15482177, 15482219, 15482231, 15482263, 15482309, 15482323, 15482329, 15482333,
                                 15482347, 15482371, 15482377, 15482387, 15482419, 15482431, 15482437, 15482447, 15482449,
                                 15482459, 15482477, 15482479, 15482531, 15482567, 15482569, 15482573, 15482581, 15482627,
                                 15482633, 15482639, 15482669, 15482681, 15482683, 15482711, 15482729, 15482743, 15482771,
                                 15482773, 15482783, 15482807, 15482809, 15482827, 15482851, 15482861, 15482893, 15482911,
                                 15482917, 15482923, 15482941, 15482947, 15482977, 15482993, 15483023, 15483029
                             };
            var p2 = BigInteger.Parse("896723");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization() { Product = product };
                int i = 0;
                for (; i < 1000000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1} \tIteration: {2}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration));
                Assert.AreEqual(squareForms.Factors[1], p1);
                Assert.AreEqual(squareForms.Factors[0], p2);
            }

            Assert.IsTrue(sb.Length > 0);
        }

        [TestMethod]
        public void TestFactor961823747timesARange()
        {
            var primes = new[]
                             {
 982448837, 982448851, 982448867, 982448879, 982448881, 982448893, 982448899, 982448923, 
 982448953, 982448959, 982449001, 982449031, 982449053, 982449059, 982449067, 982449107, 
 982449109, 982449113, 982449121, 982449133, 982449137, 982449229, 982449239, 982449277, 
 982449313, 982449343, 982449361, 982449371, 982449379, 982449401, 982449409, 982449437, 
 982449449, 982449463, 982449487, 982449491, 982449539, 982449553, 982449557, 982449563, 
 982449569, 982449571, 982449581, 982449607, 982449619, 982449631, 982449647, 982449653, 
 982449673, 982449679, 982449683, 982449691, 982449707, 982449731, 982449751, 982449757, 
 982449763, 982449773, 982449847, 982449851, 982449869, 982449889, 982449901, 982449943, 
 982449961, 982449983, 982449989, 982449997, 982450003, 982450031, 982450039, 982450057, 
 982450069, 982450087, 982450093, 982450097, 982450121, 982450151, 982450177, 982450187, 
 982450199, 982450207, 982450223, 982450253, 982450289, 982450291, 982450303, 982450319, 
 982450321, 982450327, 982450373, 982450411, 982450457, 982450459, 982450507, 982450523 
                             };
            var p2 = BigInteger.Parse("961823747");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization { Product = product, MaxIteration = 50000 };
                int i = 0;
                for (; i < 1000000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1} \tIteration: {2}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration));
                Assert.AreEqual(squareForms.Factors[1], p1);
                Assert.AreEqual(squareForms.Factors[0], p2);
            }

            Assert.IsTrue(sb.Length > 0);
        }

        [TestMethod]
        public void TestFactor81715197569timesARange()
        {
            var start = DateTime.Now;

            var primes = new[]
                             {
                                 32415194401, 32415194411, 32415194437, 32415194447, 32415194467, 32415194477, 32415194503
                                 , 32415194507, 32415194569, 32415194573, 32415194593, 32415194597, 32415194633,
                                 32415194677, 32415194687, 32415194723, 32415194729, 32415194741, 32415194783, 32415194789
                                 , 32415194849, 32415194879, 32415194909, 32415194927, 32415194971, 32415194983,
                                 32415195013, 32415195023, 32415195041, 32415195049, 32415195083, 32415195089, 32415195103
                                 , 32415195181, 32415195191, 32415195277, 32415195301, 32415195331, 32415195401,
                                 32415195427, 32415195437, 32415195457, 32415195473, 32415195497, 32415195523, 32415195569
                                 , 32415195581, 32415195619, 32415195641, 32415195677, 32415195719, 32415195739,
                                 32415195779, 32415195863, 32415195871, 32415195887, 32415195941, 32415195947, 32415196001
                                 , 32415196007, 32415196043, 32415196057, 32415196129, 32415196139, 32415196141,
                                 32415196147, 32415196201, 32415196243, 32415196271, 32415196277, 32415196279, 32415196291
                                 , 32415196309, 32415196337, 32415196361, 32415196379, 32415196381, 32415196421,
                                 32415196447, 32415196481
                             };
            var p2 = BigInteger.Parse("81715197569");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization { Product = product, MaxIteration = 1000 };
                int i = 0;
                for (; i < 100000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1:00000} \tIteration: {2:0000000} \tloops: {3:000000000}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration, i));
                if (squareForms.Factors.Length > 0)
                {
                    Assert.AreEqual(squareForms.Factors[0], p1);
                    Assert.AreEqual(squareForms.Factors[1], p2);
                }
            }

            var finished = DateTime.Now;

            var ellapsed = finished - start;

            Assert.IsTrue(sb.Length > 0);
        }

        [TestMethod]
        public void TestFactor733429220731timesARange()
        {
            var start = DateTime.Now;

            var primes = new[]
                             {
200429219041,	200429219063,	200429219069,	200429219119,	200429219129,	200429219167,	200429219191,	200429219197,	200429219201,	200429219209,
200429219227,	200429219303,	200429219329,	200429219387,	200429219407,	200429219413,	200429219569,	200429219579,	200429219587,	200429219593,
200429219597,	200429219611,	200429219623,	200429219651,	200429219659,	200429219671,	200429219689,	200429219699,	200429219707,	200429219759,
200429219843,	200429219863,	200429219911,	200429219929,	200429219933,	200429219951,	200429219981,	200429220037,	200429220053,	200429220073,
200429220119,	200429220143,	200429220161,	200429220221,	200429220293,	200429220307,	200429220319,	200429220361,	200429220389,	200429220391,
200429220401,	200429220437,	200429220443,	200429220473,	200429220569,	200429220611,	200429220613,   200429220617,	200429220619,	200429220647,
200429220659,	200429220713,	200429220751,	200429220793,	200429220811,	200429220841,	200429220869,	200429220877,	200429220917,	200429220977,
                             };
            var p2 = BigInteger.Parse("733429220731");
            var sb = new StringBuilder();
            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SquareFormsFactorization { Product = product, MaxIteration = 1200 };
                //var squareForms = new DixonFactorization { Product = product, Bounds = 4096 };
                int i = 0;
                for (; i < 100000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                sb.Append(
                    string.Format(
                        "Prime 1: {0} \tMultiplier: {1:00000} \tIteration: {2:0000000} \tloops: {3:000000000}\n",
                        p1,
                        squareForms.Multiplier,
                        squareForms.Iteration, 
                        i));
                if (squareForms.Factors.Length > 0)
                {
                    Assert.AreEqual(squareForms.Factors[0], p1);
                    Assert.AreEqual(squareForms.Factors[1], p2);
                }
            }

            var finished = DateTime.Now;

            var ellapsed = finished - start;

            Assert.IsTrue(sb.Length > 0);
        }

        //  357440504101388365610785389017
        //  200429218120815554269743635437

        [TestMethod]
        public void TestRSA59SquareForms()
        {
            var p1 = BigInteger.Parse("200429218120815554269743635437");
            var p2 = BigInteger.Parse("357440504101388365610785389017");

            var product = p1 * p2;

            var squareForms = new SquareFormsFactorization() { Product = product, MaxIteration = 100000000 };
            int i = 0;
            for (; i < 10000000000; i++)
            {
                if (squareForms.TakeStep()) break;
            }

            Assert.AreEqual(squareForms.Factors[0], p1);
            Assert.AreEqual(squareForms.Factors[1], p2);
            
        }

    }
}
