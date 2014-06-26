namespace Factoring
{
    using System.Numerics;

    public class RandomParabolicSearch : DixonFactorization
    {
        public BigInteger Multiplier { get; set; }

        public BigInteger ParabolicCentre { get; set; }

        public BigInteger CurrentIteration { get; set; }
        public BigInteger Iterations { get; set; }
        public int Width { get; set; }
        public int FirstInteger { get; set; }

        protected override BigInteger NextIntegerToCheck()
        {
            if (this.CurrentIteration == 0 || this.CurrentIteration >= this.Iterations)
            {
                Multiplier += 1;
                var sqrt = Product.Sqrt();
                var width = sqrt.Sqrt();
                Width = width.ToString().Length;
                FirstInteger = int.Parse(Width.ToString()[0].ToString());
                ParabolicCentre = sqrt * Multiplier;
                this.CurrentIteration = 1;
            }
            this.CurrentIteration += 1;
            BigInteger result = this.RandomInteger(Width, FirstInteger) + ParabolicCentre;
            return result;
        }
    }
}
