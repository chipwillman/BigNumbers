namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class Factor
    {
        public BigInteger Product { get; set; }

        public BigInteger SquareRoot
        {
            get
            {
                if (!this.squareRoot.HasValue)
                {
                    this.squareRoot = Product.Sqrt();
                }
                return this.squareRoot.Value;
            }
        }

        private BigInteger? squareRoot;

        public BigInteger[] Factors
        {
            get
            {
                return factors.ToArray();
            }
        }

        public int ProductLength
        {
            get
            {
                return Product.ToString().Length;
            }
        }

        public BigInteger LastX { get; set; }
        private BigInteger LastY { get; set; }

        private double LastSlope { get; set; }

        public BigInteger Delta()
        {
            switch (Product.ToString().Length)
            {
                case 1:
                    return new BigInteger(1);
                case 2:
                    return new BigInteger(2);
                case 3:
                    return new BigInteger(10);
                default :
                    return new BigInteger(Product.ToString().Length/100);
            }
        }

        public bool FastFactor()
        {
            if (LastX == 0)
            {
                while (Product.IsEven)
                {
                    AddFactor(2);
                }

                LastX = 3;
                while (Product % LastX == 0)
                {
                    AddFactor(LastX);
                }

                LastX = 5;
            }
            else
            {
                LastX = LastX + 6;
            }
            this.TryFactors(LastX);
            return Product == 1;
        }

        private void TryFactors(BigInteger x)
        {
            var nextX1 = x;
            var nextX2 = x + 2;
            while (this.Product % nextX1 == 0)
            {
                this.AddFactor(nextX1);
            }
            while (this.Product % nextX2 == 0)
            {
                this.AddFactor(nextX2);
            }
        }

        private void AddFactor(BigInteger divisor)
        {
            var y = this.Product / divisor;
            var mod = this.Product % y;
            if (mod != 0)
            {
                throw new ApplicationException("Physical Laws have broken down or you just introduced a bug");
            }
            this.factors.Add(divisor);
            Product = y;
        }

        private void SetFactors(BigInteger divisor)
        {
            var y = this.Product / divisor;
            var mod = this.Product % y;
            if (mod != 0)
            {
                throw new ApplicationException("Physical Laws have broken down or you just introduced a bug");
            }

            this.factors.Add(BigInteger.Min(divisor, y));
            this.factors.Add(BigInteger.Max(divisor, y));
        }

        public bool FindFactors()
        {
            if (LastX < SquareRoot)
            {
                while (Product.IsEven)
                {
                    factors.Add(2);
                    Product = Product / 2;
                }

                if (LastX == 0)
                {
                    var lastYof2 = Product / 2;
                    LastX = 3;
                    LastY = Product / LastX;
                    var lastMod = Product % LastY;
                    if (lastMod == 0)
                    {
                        factors.Add(new BigInteger(3));
                    }
                    LastSlope = (double)(LastY - lastYof2); 
                }
                else if (LastY == 0)
                {
                    LastY = Product / LastX;
                    var lastYP = Product / (LastX - 1);
                    LastSlope = (double)(LastY - lastYP);
                }

                BigInteger mod;
                BigInteger y;
                BigInteger x;
                if (LastSlope < -0.5)
                {
                    mod = Product % LastY;
                    double mantissa = (double)mod / (double)Product;

                    BigInteger offset = (BigInteger)((1.0 - mantissa) * LastSlope);
                    if (offset >= -1)
                    {
                        y = LastY - 1;
                        x = Product / y;
                    }
                    else
                    {
                        y = LastY + offset;
                        x = Product / y;
                    }

                    mod = Product % x;
                    if (mod == 0 && Product % y == 0)
                    {
                        factors.Add(BigInteger.Min(x, y));
                        factors.Add(BigInteger.Max(x, y));
                    }
                }
                else
                {
                    try
                    {
                        x = LastX + BigInteger.Max((BigInteger)(1 / -LastSlope), BigInteger.One);
                    }
                    catch (Exception)
                    {
                        x = LastX + 1;
                    }
                    y = Product / x;
                    mod = Product % y;

                    if (mod == 0)
                    {
                        if (Product % x != 0)
                        {
                            x = Product / y;
                        }

                        factors.Add(BigInteger.Min(x, y));
                        factors.Add(BigInteger.Max(x, y));
                    }
                }

                var newSlope = ((double)y - (double)LastY) / ((double)x - (double)LastX);
                if (!double.IsInfinity(newSlope))
                {
                    LastSlope = newSlope;
                }
                LastX = x;
                LastY = y;
            }
            return Factors.Length > 0;
        }

        public bool CheckForPrimesCloseToSquareRoot()
        {
            var points = new SortedList<BigInteger, BigInteger>();
            var sqrtMod = Product % SquareRoot;
            points[SquareRoot] = sqrtMod;

            var bounds = SquareRoot.Sqrt();
            var parabolaMaximum = SquareRoot - (bounds / 2);
            var testPoint = SquareRoot - (bounds / 3);

            points[parabolaMaximum] = Product % parabolaMaximum;
            points[testPoint] = Product % testPoint;

            var midwayDownParabola = parabolaMaximum + bounds / 4;

            var threeQuartersDownParabola = midwayDownParabola + (SquareRoot - midwayDownParabola) / 2;
            points[midwayDownParabola] = Product % midwayDownParabola;
            points[threeQuartersDownParabola] = Product % threeQuartersDownParabola;


            if (points[parabolaMaximum] > points[midwayDownParabola])
            {
                this.WalkParabolaDown(threeQuartersDownParabola, points);
            }
            else
            {
                this.WalkParabolaUp(parabolaMaximum, midwayDownParabola, points);
            }
            return factors.Count > 0;
        }

        private void WalkParabolaUp(BigInteger parabolaMaximum, BigInteger midwayDownParabola, SortedList<BigInteger, BigInteger> points)
        {
            var previousStep = midwayDownParabola;
            var currentStep = parabolaMaximum + (previousStep - parabolaMaximum) / 2;
            points[currentStep] = Product % currentStep;
            while ((points[previousStep] < points[currentStep]) && points[currentStep] != 0)
            {
                previousStep = currentStep;
                if ((this.SquareRoot - previousStep) / 2 == 0)
                {
                    break;
                }
                currentStep = parabolaMaximum + (previousStep - parabolaMaximum) / 2;
                points[currentStep] = this.Product % currentStep;
            }
            if (points[currentStep] == 0)
            {
                SetFactors(currentStep);
            }
            else
            {
                var min = currentStep;
                var max = previousStep;
                currentStep = this.BinarySearch(points, max, min);
                if (points[currentStep] == 0)
                {
                    this.SetFactors(currentStep);
                }
            }
        }

        private void WalkParabolaDown(BigInteger threeQuartersDownParabola, SortedList<BigInteger, BigInteger> points)
        {
            var previousStep = threeQuartersDownParabola;
            var currentStep = previousStep + (this.SquareRoot - previousStep) / 2;
            points[currentStep] = this.Product % currentStep;
            while ((points[previousStep] > points[currentStep]) && points[currentStep] != 0)
            {
                previousStep = currentStep;
                if ((this.SquareRoot - previousStep) / 2 == 0)
                {
                    break;
                }
                currentStep = previousStep + (this.SquareRoot - previousStep) / 2;
                points[currentStep] = this.Product % currentStep;
            }
            if (points[currentStep] == 0)
            {
                SetFactors(currentStep);
            }
            else
            {
                var min = previousStep;
                var max = currentStep;
                currentStep = this.BinarySearch(points, max, min);
                if (points[currentStep] == 0)
                {
                    this.SetFactors(currentStep);
                }
            }
        }

        private BigInteger BinarySearch(SortedList<BigInteger, BigInteger> points, BigInteger max, BigInteger min)
        {
            BigInteger currentStep = min;
            bool found = false;
            while (!found)
            {
                if ((max - min) / 2 == 0)
                {
                    break;
                }
                var middle = min + (max - min) / 2;
                points[middle] = this.Product % middle;
                if (points[middle] == 0)
                {
                    found = true;
                    currentStep = middle;
                }
                else if (points[middle] < points[min])
                {
                    min = middle;
                }
                else
                {
                    max = middle;
                }
            }
            return currentStep;
        }

        #region Implementation

        private List<BigInteger> factors = new List<BigInteger>();

        private BigInteger NextX(double fractionalPart, double slope)
        {
            BigInteger result;
            if (slope < -0.5)
            {
                result = (BigInteger)(slope + fractionalPart);
            }
            else
            {
                result = (BigInteger)(slope - fractionalPart);
            }
            return result;
        }

        #endregion
    }
}
