namespace Factoring
{
    using System.Numerics;

    public class SearchParabolicRegions : DixonFactorization
    {
        private BigInteger? squareRootSquareRoot;

        public BigInteger Multiplier { get; set; }

        public BigInteger ParabolicCentre { get; set; }

        public BigInteger Current { get; set; }
        public BigInteger EndRange { get; set; }
        public BigInteger SubStep { get; set; }

        public BigInteger MaxSubStep
        {
            get
            {
                if (!squareRootSquareRoot.HasValue)
                {
                    squareRootSquareRoot = 1024;
                }
                return squareRootSquareRoot.Value;
            }
        }

        protected override BigInteger NextIntegerToCheck()
        {
            if (Current == 0)
            {
                Current = 1;
                SubStep = 0;
            }
            else
            {
                if (SubStep < MaxSubStep)
                {
                    SubStep++;
                }
                else
                {
                    SubStep = 0;
                    Current++;
                }
            }
            var result = (Current * Product).Sqrt() + SubStep;
            return result;
        }
    }
}
