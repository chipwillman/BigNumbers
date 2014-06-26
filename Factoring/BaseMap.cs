namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    public class BaseMap
    {
        public BaseMap(BigInteger numberBase)
            : this(numberBase, 0, 0, 0)
        {
        }

        public BaseMap(BigInteger numberBase, int currentDigit, BigInteger fixedX, BigInteger fixedY)
        {
            this.Base = numberBase;
            this.CurrentDigit = currentDigit;
            this.FixedX = fixedX;
            this.FixedY = fixedY;
        }

        public BigInteger Base { get; set; }

        public int CurrentDigit { get; private set; }

        public BigInteger FixedX { get; private set; }
        public BigInteger FixedY { get; private set; }

        public BigInteger Product { get; set; }

        public BaseNode[] NextDigit { get; set; }

        public void GenerateMap()
        {
            var xList = GetBaseList();
            var yList = GetBaseList();
            var matchingNodes = new Dictionary<BigInteger, BaseNode>();

            foreach (var yPower in yList)
            {
                BigInteger y = yPower * this.FixedDigitsOffset() + FixedY;
                foreach (var xPower in xList)
                {
                    var x = xPower * this.FixedDigitsOffset() + FixedX;
                    var product = y * x;

                    var digitsAsString = ToBaseString(product);
                    string lastDigits;
                    if (digitsAsString.Length <= CurrentDigit + 1) lastDigits = digitsAsString;
                    else lastDigits = digitsAsString.Substring(digitsAsString.Length - (CurrentDigit + 1));

                    if (LastDigitsMatch(lastDigits, this.TargetMatchString))
                    {
                        if (!matchingNodes.ContainsKey(x) && !matchingNodes.ContainsKey(y))
                        {
                            var node = new BaseNode { Digit = CurrentDigit + 1, Base = Base, X = x, Y = y, Product = this.Product };
                            matchingNodes.Add(x, node);
                        }
                    }
                }
            }
            if (matchingNodes.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("no matches found");
            }
            NextDigit = matchingNodes.Values.ToArray();
        }

        private bool LastDigitsMatch(string lastDigits, string matchString)
        {
            if (lastDigits.Length >= matchString.Length)
            {
                lastDigits = lastDigits.Substring(lastDigits.Length - (CurrentDigit + 1));
            }
            if (matchString.Length > lastDigits.Length)
            {
                matchString = matchString.Substring(matchString.Length - (CurrentDigit + 1));
            }
            return lastDigits == matchString;
        }

        protected string TargetMatchString
        {
            get
            {
                if (this.targetMatchString == null)
                {
                    var productString = this.ToBaseString(Product);
                    if (productString.Length >= CurrentDigit + 1)
                    {
                        this.targetMatchString = productString.Substring(productString.Length - (CurrentDigit + 1));
                    }
                    while (productString.Length < CurrentDigit + 1)
                    {
                        this.targetMatchString = "0" + this.targetMatchString;
                    }
                }
                return this.targetMatchString;
            }
        }

        private string targetMatchString;

        private BigInteger FixedDigitsOffset()
        {
            BigInteger result = 1;
            if (CurrentDigit > 0)
            {
                result = Base;
                var digit = 1;
                while (digit < CurrentDigit)
                {
                    result *= Base;
                    digit++;
                }
            }
            return result;
        }

        private List<BigInteger> GetBaseList()
        {
            var result = new List<BigInteger>();
            for (var i = 0; i < this.Base; i ++)
            {
                result.Add(i);
            }
            return result;
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

            while (sb.Length < CurrentDigit + 1)
            {
                sb.Insert(0, "0");
            }

            var baseResult = sb.ToString();
            return baseResult;
        }

        public void IterateMap()
        {
            foreach (var node in NextDigit)
            {
                node.GenerateMap();
            }
        }

        public int PossibleFactors()
        {
            var result = 0;
            if (NextDigit != null)
            {
                foreach (var possibleDigit in NextDigit)
                {
                    result += possibleDigit.PossibleFactors();
                }
            }
            return result;
        }

        public int DigitDepth()
        {
            var result = 0;
            if (NextDigit != null)
            {
                foreach (var digit in NextDigit)
                {
                    result = Math.Max(result, digit.DigitDepth());
                }
            }
            return result;
        }
    }
}
