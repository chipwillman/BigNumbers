namespace Factoring
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public class ChineseRemainderTheorem
    {
        public CongruentEquation[] Congruents { get; set; }

        protected class EquationSection
        {
            public EquationSection()
            {
                Moduli = new List<BigInteger>();
            }

            public List<BigInteger> Moduli { get; set; }

            public BigInteger Factor { get; set; }

            public BigInteger ModuliProduct()
            {
                return this.Moduli.Aggregate<BigInteger, BigInteger>(1, (current, modulus) => current * modulus);
            }
        }

        public CongruentEquation Find()
        {
            if (CoPrime(Congruents, 1000))
            {
                var result = new CongruentEquation();
                var sections = new List<EquationSection>();

                for (int i = 0; i < Congruents.Length; i++)
                {
                    var section = new EquationSection();
                    for (var j = 0; j < Congruents.Length; j++)
                    {
                        var congruent = Congruents[j];
                        if (i != j)
                        {
                            section.Moduli.Add(congruent.Modulus);
                        }
                    }
                    sections.Add(section);
                }

                for (int i = 0; i < Congruents.Length; i++)
                {
                    var congruent = Congruents[i];
                    SetFactor(congruent, sections[i]);
                }

                result.Modulus = 1;
                foreach (var congurent in Congruents)
                {
                    result.Modulus *= congurent.Modulus;
                }

                result.Remainder = 0;
                foreach (var section in sections)
                {
                    result.Remainder += section.Factor;
                }

                return result;
            }
            return null;
        }

        private void SetFactor(CongruentEquation congruent, EquationSection equationSection)
        {
            var desiredRemainder = congruent.Remainder;
            var factor = equationSection.ModuliProduct();

            var currentRemainder = factor % congruent.Modulus;
            if (desiredRemainder == currentRemainder)
            {
                equationSection.Factor = factor;
            }
            else
            {
                var x = 2;
                var maxLargestNumber = 100000000;
                while (x < maxLargestNumber)
                {
                    var product = factor * x;
                    if (product % congruent.Modulus == desiredRemainder)
                    {
                        equationSection.Factor = product;
                        break;
                    }
                    x++;
                }
            }

        }

        private bool CoPrime(CongruentEquation[] congruents, BigInteger primesToTest)
        {
            bool result = true;

            for (int i = 0; i < primesToTest; i++)
            {
                var prime = Primes.GetNthPrime(i);
                var factorOfCongruents = congruents.Count(congruent => congruent.Modulus % prime == 0);
                if (factorOfCongruents > 1)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
