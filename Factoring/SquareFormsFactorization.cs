namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    public class SquareFormsFactorization
    {
        public SquareFormsFactorization()
        {
            factorsList = new List<BigInteger>();
            Multiplier = 1;// Generator.Next(5000) + 1;
            this.MaxIteration = 10000;
        }

        public BigInteger Product { get; set; }

        public BigInteger[] Factors
        {
            get
            {
                return factorsList.ToArray();
            }
        }

        public Random Generator
        {
            get
            {
                return this.generator ?? (this.generator = new Random());
            }
            set
            {
                generator = value;
            }
        }


        public BigInteger Multiplier { get; set; }

        public BigInteger PreviousP { get; set; }

        public BigInteger P { get; set; }

        public BigInteger PreviousQ { get; set; }

        public BigInteger Q { get; set; }

        public BigInteger RootOfMultiple { get; set; }

        public bool PerfectSquareFound { get; set; }

        public BigInteger Iteration { get; set; }

        public BigInteger MaxIteration { get; set; }

        public bool TakeStep()
        {
            this.Iteration ++;
            if ((!PerfectSquareFound && this.Iteration % this.MaxIteration == 0) || (PerfectSquareFound && this.Iteration % (this.MaxIteration * 10) == 0))
            {
                this.NewMultiplier();
            }

            if (this.PreviousP <= 0)
            {
                this.InitializeSquareSearch();
            }

            if (!this.PerfectSquareFound)
            {
                this.SquareSearch();

                this.PerfectSquareFound = this.Q.IsSquare();
                if (this.PerfectSquareFound)
                {
                    this.InitializeFactorSearch();
                }
            }

            if (this.PerfectSquareFound)
            {
                this.FactorSearch();
                if (this.PreviousP == this.P)
                {
                    this.CheckFactor();
                }
                else
                {
                    this.PreviousP = this.P;
                }
            }

            return Factors.Length > 0;
        }

        #region Implementation

        private int loopCount;

        private int loopMultiplier = 1;

        private readonly List<BigInteger> factorsList;

        private Random generator;
        private void CheckFactor()
        {
            var factor = BigInteger.GreatestCommonDivisor(this.P, this.Product);
            if (factor > 1)
            {
                this.AddFactor(factor, this.Product / factor);
            }
            else
            {
                this.NewMultiplier();
            }
        }

        private void FactorSearch()
        {
            var bi = (this.RootOfMultiple + this.PreviousP) / this.Q;
            this.P = bi * this.Q - this.PreviousP;
            var currentQ = this.Q;
            this.Q = this.PreviousQ + bi * (this.PreviousP - this.P);
            this.PreviousQ = currentQ;
        }

        private void InitializeFactorSearch()
        {
            var squareRootQ = this.Q.Sqrt();
            var b0 = (this.RootOfMultiple - this.PreviousP) / squareRootQ;
            this.P = b0 * squareRootQ + this.PreviousP;
            this.PreviousQ = squareRootQ;
            this.Q = (this.Multiplier * this.Product - this.P * this.P) / this.PreviousQ;
            this.PreviousP = this.P;
        }

        private void NewMultiplier()
        {
            this.Multiplier++;
            //this.Multiplier = this.Generator.Next(100 * this.loopMultiplier) + 1;
            this.PreviousP = 0;
            this.PerfectSquareFound = false;
            this.Iteration = 1;
            //this.loopCount++;
            //if (this.loopCount > 50 * this.loopMultiplier)
            //{
            //    this.loopCount = 0;
            //    this.loopMultiplier *= 2;
            //}
        }

        private void SquareSearch()
        {
            var bi = (this.RootOfMultiple + this.PreviousP) / this.Q;
            this.P = bi * this.Q - this.PreviousP;
            var currentQ = this.Q;
            this.Q = this.PreviousQ + bi * (this.PreviousP - this.P);
            this.PreviousP = this.P;
            this.PreviousQ = currentQ;
        }

        private void InitializeSquareSearch()
        {
            var multipleProduct = this.Product * this.Multiplier;
            this.RootOfMultiple = multipleProduct.Sqrt();
            this.PreviousP = this.RootOfMultiple;
            this.PreviousQ = 1;
            this.Q = (multipleProduct) - this.PreviousP * this.PreviousP;
        }

        private void AddFactor(BigInteger factor1, BigInteger factor2)
        {
            this.factorsList.Add(BigInteger.Min(factor1, factor2));
            this.factorsList.Add(BigInteger.Max(factor1, factor2));
        }

        #endregion
    }
}
