namespace Factoring
{
    using System;
    using System.Numerics;

    public class PointOfModulusInterest
    {
        public PointOfModulusInterest(BigInteger product, double multiple)
        {
            Multiple = multiple;
            if (product < new BigInteger(1000000000) || multiple < 1)
            {
                X = ((BigInteger)((double)product / multiple)).Sqrt();
                Modulus = product % X;
                Y = ((BigInteger)(multiple * (double)X));
                Y += Modulus / Y;
                Fermats  = GetFermats(X, Y, Modulus);
            }
            else
            {
                if (multiple >= 1)
                {
                    X = (product / (BigInteger)multiple).Sqrt();
                    Modulus = product % X;
                    Y = ((BigInteger)multiple * X);
                    Y += Modulus / Y;
                    Fermats = GetFermats(X, Y, Modulus);
                }
            }
        }

        public static BigInteger GetFermats(BigInteger x, BigInteger y, BigInteger modulus)
        {
            var p1 = BigInteger.Min(x, y);
            var p2 = BigInteger.Max(x, y);
            var quotient = p1 * (p2 - 1);
            if (quotient / p2 < new BigInteger(100000000))
            {
                double doubleCo = ((double)quotient / (double)p2) - (double)(p1 - 1);
                var result = doubleCo * Math.Sqrt((double)p2 * (double)modulus / (double)p1);
                return (BigInteger)result;
            }
            else
            {
                var co = BigInteger.Max(1, ((p1 * (p2 - 1)) / p2) - (p1 - 1));
                var result = co * (p2 * modulus / p1).Sqrt();
                return result;
            }

        }

        public BigInteger Y { get; set; }
        public BigInteger X { get; set; }
        public BigInteger Modulus { get; set; }
        public BigInteger Fermats { get; set; }
        public double Multiple { get; set; }
    }
}
