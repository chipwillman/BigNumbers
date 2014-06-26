namespace Factoring.Test
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FindSqauresWithMinimalFactors
    {
        [TestMethod]
        public void TestSizeOf24bit()
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

            var smallestFactors = new List<SortedList<BigInteger, List<BigInteger>>>();

            for (int primeIndex = 0; primeIndex < primes.Length; primeIndex++)
            {
                BigInteger p1 = primes[primeIndex];

                var product = p1 * p2;

                var squareForms = new SmoothFactors() { Product = product, Bounds = 20 };
                int i = 0;
                for (; i < 1000000; i++)
                {
                    if (squareForms.TakeStep()) break;
                }
                smallestFactors.Add(squareForms.Factors);
            }

            Assert.IsTrue(smallestFactors.Count > 0);
        }
    }
}
