namespace Factoring
{
    using System.Collections.Generic;
    using System.Numerics;

    public class Congruences
    {
        public SortedList<BigInteger, List<BigInteger>> Values
        {
            get
            {
                if (values == null)
                {
                    values = new SortedList<BigInteger, List<BigInteger>>();
                }
                return values;
            }
        }

        public SortedList<BigInteger, BigInteger[]> Factors
        {
            get
            {
                if (factorsList == null)
                {
                    factorsList = new SortedList<BigInteger, BigInteger[]>();
                }
                return factorsList;
            }
        }

        public SortedList<BigInteger, List<BigInteger>> LikelyCandidates()
        {
            var result = new SortedList<BigInteger, List<BigInteger>>();
            foreach (var key in Values.Keys)
            {
                if (Values[key].Count > 1)
                {
                    result.Add(key, Values[key]);
                }
            }
            return result;
        }

        public void AddCongruence(BigInteger modulus, BigInteger value)
        {
            if (Values.ContainsKey(modulus))
            {
                if (!Values[modulus].Contains(value))
                {
                    Values[modulus].Add(value);
                }
            }
            else
            {
                var list = new List<BigInteger>();
                list.Add(value);
                Values.Add(modulus, list);
            }
        }

        public void AddFactors(BigInteger modulus, BigInteger[] factors)
        {
            if (!Factors.ContainsKey(modulus))
            {
                Factors.Add(modulus, factors);
            }
        }

        public void AddCongruence(BigInteger modulus, BigInteger value, BigInteger[] factors)
        {
            this.AddCongruence(modulus, value);
            this.AddFactors(value, factors);
        }

        private SortedList<BigInteger, List<BigInteger>> values;
        private SortedList<BigInteger, BigInteger[]> factorsList;
    }
}
