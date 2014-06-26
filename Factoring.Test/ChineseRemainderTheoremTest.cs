namespace Factoring.Test
{
    using System.Numerics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ChineseRemainderTheoremTest
    {
        [TestMethod]
        public void SolveMadeEasyEquation()
        {
            var congruent1 = new CongruentEquation { Remainder = 2, Modulus = 3 };
            var congruent2 = new CongruentEquation { Remainder = 2, Modulus = 4 };
            var congruent3 = new CongruentEquation { Remainder = 1, Modulus = 5 };

            var solver = new ChineseRemainderTheorem { Congruents = new[] { congruent1, congruent2, congruent3 } };

            var result = solver.Find();

            Assert.IsNotNull(result);

            Assert.AreEqual(new BigInteger(86), result.Remainder);
            Assert.AreEqual(new BigInteger(60), result.Modulus);
        }

        [TestMethod]
        public void TestSolve31Times59()
        {
            BigInteger product = 31 * 59;
            var congruent1 = new CongruentEquation { Remainder = product % 3, Modulus = 3 };
            var congruent2 = new CongruentEquation { Remainder = product % 4, Modulus = 4 };
            var congruent3 = new CongruentEquation { Remainder = product % 5, Modulus = 5 };
            var congruent4 = new CongruentEquation { Remainder = product % 29, Modulus = 29 };
            var congruent7 = new CongruentEquation { Remainder = product % 11, Modulus = 11 };
            var congruent5 = new CongruentEquation { Remainder = product % 63, Modulus = 63 };
            var congruent6 = new CongruentEquation { Remainder = product % 41, Modulus = 41 };

            var solver = new ChineseRemainderTheorem { Congruents = new[] { congruent1, congruent2, congruent3, congruent4, congruent5, congruent6, congruent7 } }; // 

            var result = solver.Find();

            Assert.IsNotNull(result);

            Assert.IsTrue((product - result.Remainder) % result.Modulus == 0);
            Assert.AreEqual(new BigInteger(420), result.Modulus);
        }

        [TestMethod]
        public void TestSolve2543Times6791()
        {
            BigInteger product = 2543 * 6791;
            var congruent1 = new CongruentEquation { Remainder = product % 3, Modulus = 3 };
            var congruent2 = new CongruentEquation { Remainder = 353, Modulus =  353};
            var congruent3 = new CongruentEquation { Remainder = 827, Modulus = 827 };
            //var congruent4 = new CongruentEquation { Remainder = product % 29, Modulus = 29 };
            //var congruent7 = new CongruentEquation { Remainder = product % 11, Modulus = 11 };
            //var congruent5 = new CongruentEquation { Remainder = product % 63, Modulus = 63 };
            //var congruent6 = new CongruentEquation { Remainder = product % 41, Modulus = 41 };
            var solver = new ChineseRemainderTheorem { Congruents = new[] { congruent1, congruent2, congruent3 } }; // congruent4, , congruent4, congruent5, congruent6, congruent7

            var result = solver.Find();

            Assert.IsNotNull(result);

            Assert.IsTrue((product - result.Remainder) % result.Modulus == 0);
            Assert.AreEqual(new BigInteger(420), result.Modulus);
        }

        [TestMethod]
        public void TestSolveExtendedEuclideanAlgorithm()
        {
            var algorithm = new ExtendedEuclideanAlgorithm
                                {
                                    Equation =
                                        new CongruentEquation { Modulus = 43, Remainder = 1 },
                                    Multiple = 17
                                };

            var result = algorithm.FindInverseModulo();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSolve120And23Equation()
        {
            var algorithm = new ExtendedEuclideanAlgorithm
            {
                Equation =
                    new CongruentEquation { Modulus = 23, Remainder = 1 },
                Multiple = 120
            };

            var result = algorithm.FindInverseModulo();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSolve11180And482Equation()
        {
            var algorithm = new ExtendedEuclideanAlgorithm
            {
                Equation =
                    new CongruentEquation { Modulus = 482, Remainder = 1 },
                Multiple = 1180
            };

            var result = algorithm.FindInverseModulo();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestSolve986And104Equation()
        {
            var algorithm = new ExtendedEuclideanAlgorithm
            {
                Equation =
                    new CongruentEquation { Modulus = 104, Remainder = 1 },
                Multiple = 986
            };

            var result = algorithm.FindInverseModulo();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestQuadraticReciprical()
        {
            
        }
    }
}
