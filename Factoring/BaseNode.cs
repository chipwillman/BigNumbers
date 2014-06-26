namespace Factoring
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Text;

    public class BaseNode
    {
        public BigInteger Base { get; set; }

        public BigInteger Digit { get; set; }
        public BigInteger X { get; set; }
        public BigInteger Y { get; set; }

        public BaseMap Map { get; set; }

        public BigInteger Product { get; set; }

        public string ValueAsBaseString
        {
            get
            {
                return this.ToBaseString(X * Y);
            }
        }

        public void GenerateMap()
        {
            if (Map == null)
            {
                Map = new BaseMap(this.Base, (int)Digit, X, Y);
                Map.Product = Product;
                Map.GenerateMap();
            }
            else
            {
                Map.IterateMap();
            }
        }

        public int PossibleFactors()
        {
            if (Map != null)
            {
                return Map.PossibleFactors();
            }
            return 1;
        }

        public int DigitDepth()
        {
            if (Map != null)
            {
                return Map.DigitDepth() + 1;
            }
            return 1;
        }

        private string ToBaseString(BigInteger number)
        {
            var digits = new Dictionary<int, BigInteger>();
            var length = number.ToByteArray().Length;

            var digit = 0;
            while (digit <= length * 4 && number > 0)
            {
                digits[digit] = number % this.Base;
                number /= this.Base;
                digit++;
            }

            StringBuilder sb = new StringBuilder(digits.Count);
            for (int i = digits.Count - 1; i >= 0; i--)
            {
                if (digits[i] < 10)
                {
                    sb.Append(digits[i]);
                }
                else if (digits[i] >= 10)
                {
                    var value = (char)((int)'A' + (digits[i] - 10));
                    sb.Append(value);
                }
            }

            while (sb.Length < Digit)
            {
                sb.Insert(0, "0");
            }

            var baseResult = sb.ToString();
            return baseResult;
        }
    }
}
