namespace Factoring
{
    using System;
    using System.Numerics;

    public static class BigIntegerExtensions
    {
        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        public static bool IsSquare(this BigInteger n)
        {
            if (n == 0) return false;
            var lastDigit = n & 0xf;
            if (lastDigit > 9)
            {
                return false;
            }
            if (lastDigit != 2 && lastDigit != 3 && lastDigit != 5 && lastDigit != 6 && lastDigit != 7 && lastDigit != 8)
            {
                var squareRoot = Sqrt(n);
                return (n % squareRoot == 0);
            }
            return false;
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }
    }
}
