namespace Factoring
{
    using System.Collections.Generic;
    using System.Numerics;

    public class ExtendedEuclideanAlgorithm
    {
        public CongruentEquation Equation { get; set; }

        public BigInteger Multiple { get; set; }

        protected class NumericEquation
        {
            public BigInteger Modulus { get; set; }

            public BigInteger Multiple { get; set; }

            public BigInteger ModDivMul { get; set; }

            public BigInteger ModModMul { get; set; }
        }

        protected class SecondOrderEquation
        {
            public BigInteger X { get; set; }

            public BigInteger CoX { get; set; }

            public BigInteger Y { get; set; }

            public BigInteger CoY { get; set; }
        }

        public CongruentEquation FindInverseModulo()
        {
            // a X = r (mod m)

            // m = r (a * (a / m) + (a % m)
            var numberEquations = new List<NumericEquation>();

            var previousEquation = new NumericEquation();
            previousEquation.Modulus = BigInteger.Max(Multiple, Equation.Modulus);
            previousEquation.Multiple = BigInteger.Min(Multiple, Equation.Modulus);
            previousEquation.ModDivMul = previousEquation.Modulus / previousEquation.Multiple;
            previousEquation.ModModMul = previousEquation.Modulus % previousEquation.Multiple;

            numberEquations.Add(previousEquation);

            while (previousEquation.ModModMul != 1 && previousEquation.ModModMul != 0)
            {
                var numberEquation = new NumericEquation();
                numberEquation.Modulus = previousEquation.Multiple;
                numberEquation.Multiple = previousEquation.ModModMul;
                numberEquation.ModDivMul = numberEquation.Modulus / numberEquation.Multiple;
                numberEquation.ModModMul = numberEquation.Modulus % numberEquation.Multiple;
                numberEquations.Add(numberEquation);
                previousEquation = numberEquation;
            }

            int startingIndex = numberEquations.Count - 1;
            if (previousEquation.ModModMul == 0 && startingIndex > 0)
            {
                startingIndex--;
            }

            previousEquation = numberEquations[startingIndex];
            var previousSubstitutedEquation = new SecondOrderEquation();
            previousSubstitutedEquation.X = previousEquation.Modulus;
            previousSubstitutedEquation.CoX = 1;
            previousSubstitutedEquation.Y = previousEquation.Multiple;
            previousSubstitutedEquation.CoY = -previousEquation.ModDivMul;

            for (int i = startingIndex - 1; i >= 0; i--)
            {
                previousEquation = numberEquations[i];

                var substitutedEquation = new SecondOrderEquation();
                substitutedEquation.X = previousEquation.Modulus;
                substitutedEquation.CoX = previousSubstitutedEquation.CoY;
                substitutedEquation.Y = previousEquation.Multiple;
                substitutedEquation.CoY = -previousEquation.ModDivMul * previousSubstitutedEquation.CoY + previousSubstitutedEquation.CoX;
                previousSubstitutedEquation = substitutedEquation;
            }
            var result = new CongruentEquation()
                             {
                                 Remainder = BigInteger.Min(previousSubstitutedEquation.CoX,previousSubstitutedEquation.CoY),
                                 Modulus = BigInteger.Max(previousSubstitutedEquation.CoX, previousSubstitutedEquation.CoY)
                             };
            return result;
        }
    }
}
