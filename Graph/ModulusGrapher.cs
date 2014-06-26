namespace Factoring
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Numerics;

    public class ModulusGrapher
    {
        public ModulusGrapher()
        {
            DrawModMapEnabled = true;
            DrawFermatsEnabled = true;
        }

        public BigInteger Product { get; set; }

        public BigInteger Factor1 { get; set; }

        public BigInteger Factor2 { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool DrawPrimesEnabled { get; set; }
        public bool DrawModMapEnabled { get; set; }
        public bool DrawFermatsEnabled { get; set; }
        public bool DrawPointsOfInterestEnabled { get; set; }

        protected List<PointOfModulusInterest> PointsOfInterest = new List<PointOfModulusInterest>();

        public double ScalingFactorX { get; set; }

        public double ScalingFactorY { get; set; }

        public BigInteger AreaOfInterestStart { get; set; }

        public BigInteger AreaOfInterestEnd { get; set; }

        public void AddPointsOfInterest()
        {
            DrawPointsOfInterestEnabled = true;
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 8.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 7.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 6.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 5.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 4.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 3.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 2.0 / 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 3.0 / 2.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 5.0 / 6.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 4.0 / 5.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 3.0 / 4.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 2.0 / 3.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 3.0 / 5.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 2.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 3.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 4.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 5.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 6.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 7.0));
            PointsOfInterest.Add(new PointOfModulusInterest(Product, 1.0 / 8.0));
        }

        public Image GenerateModMap(Image image)
        {
            using (var g = Graphics.FromImage(image))
            {
                if (DrawModMapEnabled)
                {
                    this.DrawModMap(image, g);
                }

                if (DrawPrimesEnabled)
                {
                    DrawPrimes(g, image);
                }

                if (DrawPointsOfInterestEnabled)
                {
                    DrawPointsOfInterest(image, g);
                }

                if (DrawFermatsEnabled)
                {
                    DrawFermats(image, g);
                }
            }
            return image;
        }

        private void DrawFermats(Image image, Graphics g)
        {
            using (var pen = new Pen(new SolidBrush(Color.Lime)))
            {
                if (this.AreaOfInterestEnd.IsEven) this.AreaOfInterestEnd++;

                var lastX = (double)this.Width;
                var lastY = (double)this.Height;
                var step = BigInteger.Max(2, (BigInteger)((double)(1 / this.ScalingFactorX)));
                if (!step.IsEven) step++;
                for (BigInteger x = this.AreaOfInterestEnd; x >= this.AreaOfInterestStart; x -= step)
                {
                    //if (x % 3 == 0 || x % 5 == 0 || x % 7 == 0) continue; // || x % 5 == 0 || x % 7 == 0) continue; // || x%5 == 0 || x % 7 == 0 || x % 11 == 0 || x % 13 == 0) continue;
                    //if (!Primes.ContainsPrime(x)) continue;
                    //var mod = this.Product % (x - modOffset);
                    var mod = this.Product % x;
                    var y = this.Product / x;
                    var fermats = BigInteger.Max(1, PointOfModulusInterest.GetFermats(x, y, mod));
                    double pointY = 1.0;
                    if (fermats == 0)
                    {
                        pointY = 0;
                    }
                    else if (x < new BigInteger(100000000000))
                    {
                        var digits = x.Sqrt().ToString().Length;
                        switch (digits)
                        {
                            case 0:
                            case 1:
                                digits = 1;
                                break;
                            case 2:
                                digits = 2;
                                break;
                            case 3:
                                digits = 3;
                                break;
                            case 4:
                                digits = 4;
                                break;
                            case 5:
                            case 6:
                                digits = 4;
                                break;
                            default:
                                digits = 5;
                                break;
                        }
                        pointY = image.Height - ((double)fermats * this.ScalingFactorY * 5 * Math.Pow(2.0, digits));
                    }
                    else
                    {
                        pointY = image.Height - ((double)fermats * (double)fermats) * (1/10.0) * this.ScalingFactorY;
                        //pointY = image.Height - ((double)fermats) * (50.0) * this.ScalingFactorY;
                    }
                    var pointX = (double)(x - this.AreaOfInterestStart) * this.ScalingFactorX;
                    if (!double.IsNaN(pointY) && !double.IsNaN(pointX))
                    {
                        try
                        {
                            g.DrawLine(pen, (float)lastX, (float)lastY, (float)pointX, (float)pointY);
                            lastX = pointX;
                            lastY = pointY;
                        }
                        catch (OverflowException)
                        {
                        }
                    }
                    else
                        System.Diagnostics.Debug.WriteLine("Huh?");
                }
            }
        }

        private void DrawPointsOfInterest(Image image, Graphics graphics)
        {
            var offset = 10.0f;
            foreach (var point in PointsOfInterest)
            {
                var font = SystemFonts.MessageBoxFont;
                var brush = SystemBrushes.WindowText;
                var text = point.Multiple.ToString("0.000") + ": " + point.X.ToString("000000") + ", " + point.Y.ToString("000000")  + " mod " + point.Modulus.ToString() + " fer " + point.Fermats.ToString();
                graphics.DrawString(text, font, brush, 5.0f, offset);
                offset += 15f;
            }
        }

        private void DrawModMap(Image image, Graphics g)
        {
            using (var pen = new Pen(new SolidBrush(Color.Gray)))
            {
                if (this.AreaOfInterestEnd.IsEven) this.AreaOfInterestEnd++;

                var squareRoot = this.Product.Sqrt();
                using (var mod0pen = new Pen(new SolidBrush(Color.Red)))
                {
                    var lastX = (double)this.Width;
                    var lastY = (double)this.Height;
                    var step = BigInteger.Max(2, (BigInteger)((double)(1 / this.ScalingFactorX)));
                    if (!step.IsEven) step++;    
                    for (BigInteger x = this.AreaOfInterestEnd; x >= this.AreaOfInterestStart; x -= step)
                    {
                        //if (x % 3 == 0) continue; // || x % 5 == 0 || x % 7 == 0) continue; // || x%5 == 0 || x % 7 == 0 || x % 11 == 0 || x % 13 == 0) continue;
                        //if (!Primes.ContainsPrime(x)) continue;
                        //var mod = this.Product % (x - modOffset);
                        BigInteger mod;
                        if (x > squareRoot)
                        {
                            mod = (x * x) % this.Product;  // BigInteger.ModPow(x, new BigInteger(2), this.Product);
                        }
                        else
                        {
                            mod = this.Product % x;
                        }

                        var pointY = image.Height - ((double)mod * this.ScalingFactorY);
                        var pointX = (double)(x - this.AreaOfInterestStart) * this.ScalingFactorX;
                        g.DrawLine(mod == 0 ? mod0pen : pen, (float)lastX, (float)lastY, (float)pointX, (float)pointY);
                        lastX = pointX;
                        lastY = pointY;
                    }
                }
            }
        }

        private void DrawPrimes(Graphics g, Image image)
        {
            double lastX = Width;
            double lastY = Height;
            var primePen = new Pen(new SolidBrush(Color.Gold));
            var step = BigInteger.Max(1, (BigInteger)(1 / ScalingFactorX));
            for (BigInteger x = AreaOfInterestEnd; x >= AreaOfInterestStart; x -= step)
            {
                if (Primes.ContainsPrime(x))
                {
                    var mod = Product % x;
                    var pointY = image.Height - ((double)mod * ScalingFactorY);
                    var pointX = (double)(x - AreaOfInterestStart) * ScalingFactorX;
                    g.DrawLine(primePen, (float)lastX, (float)lastY, (float)pointX, (float)pointY);
                    lastX = pointX;
                    lastY = pointY;
                }
            }
        }
    }
}
