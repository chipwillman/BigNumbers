
namespace Graph
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    using System.Numerics;

    using Factoring;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double scalingFactorX;

        private double scalingFactorY;

        private BigInteger areaOfInterestStart;

        private BigInteger areaOfInterestEnd;


        private const long MaxIterations = 10000000;
        private void FactorButton_Click(object sender, EventArgs e)
        {
            try
            {
                var numberToFactor = ParseInput(ProductToFactorTextBox.Text);
                var factorer = new Factor { Product = numberToFactor };
                for (int i = 0; i < MaxIterations; i ++)
                {
                    if (factorer.FastFactor()) break;
                }
                if (factorer.Factors.Length > 0)
                {
                    Factor1TextBox.Text = factorer.Factors[0].ToString();
                    Factor2TextBox.Text = factorer.Factors[1].ToString();
                }
                else if (numberToFactor.ToString().Length > 20)
                {
                    AreaOfInterestStartTextBox.Text = factorer.LastX.ToString();
                    AreaOfInterestEndTextBox.Text = BigInteger.Min(numberToFactor / factorer.LastX, numberToFactor.Sqrt()).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ModMapButton_Click(object sender, EventArgs e)
        {
            this.GenerateModMap();
        }

        private void GenerateModMap()
        {
            try
            {
                var product = this.ParseInput(this.ProductToFactorTextBox.Text);
                this.areaOfInterestStart = this.ParseInput(this.AreaOfInterestStartTextBox.Text);
                this.areaOfInterestEnd = this.ParseInput(this.AreaOfInterestEndTextBox.Text);

                this.scalingFactorX = this.GraphPictureBox.Width / ((double)(this.areaOfInterestEnd - this.areaOfInterestStart));
                if (this.areaOfInterestEnd > product.Sqrt())
                { 
                    this.scalingFactorY = this.GraphPictureBox.Height / (double)product; }
                else
                { 
                    this.scalingFactorY = this.GraphPictureBox.Height / (double)this.areaOfInterestEnd; 
                }

                var graph = new ModulusGrapher
                                {
                                    Product = product,
                                    AreaOfInterestStart = this.areaOfInterestStart,
                                    AreaOfInterestEnd = this.areaOfInterestEnd,
                                    ScalingFactorX = this.scalingFactorX,
                                    ScalingFactorY = this.scalingFactorY,
                                    Width = this.GraphPictureBox.Width,
                                    Height = this.GraphPictureBox.Height
                                };
                graph.AddPointsOfInterest();
                var bitmap = new Bitmap(this.GraphPictureBox.Width, this.GraphPictureBox.Height);
                var image = graph.GenerateModMap(bitmap);

                if (this.GraphPictureBox.Image != null)
                {
                    this.GraphPictureBox.Image.Dispose();
                }
                this.GraphPictureBox.Image = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private BigInteger ParseInput(string input)
        {
            BigInteger result;
            try
            {
                result = BigInteger.Parse(input);
            }
            catch (Exception)
            {
                var exp = double.Parse(input);
                result = (BigInteger)exp;
            }
            return BigInteger.Max(1, result);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnimationTimer.Tick += (o, args) => this.AnimateStart();
            AnimationTimer.Interval = (int)AnimationWaitUpDown.Value;
            AnimationTimer.Start();
        }

        // private Thread AnimationThread;
        private bool run = true;
        private bool done = false;

        private void PauseButton_Click(object sender, EventArgs e)
        {
            run = !run;
        }

        private void AnimateStart()
        {
            AnimationTimer.Stop();
            BigInteger start = ParseInput(ProductToFactorTextBox.Text);
            if (run)
            {
                ProductToFactorTextBox.Text = (start + 2).ToString();
                Application.DoEvents();
                this.ModMapButton_Click(null, EventArgs.Empty);
                Application.DoEvents();
            }
            AnimationTimer.Start();
        }

        private void CycleFactors()
        {
            AnimationTimer.Stop();

            BigInteger factor1 = BigInteger.Parse(Factor1TextBox.Text);
            BigInteger start = BigInteger.Parse(Factor2TextBox.Text);
            if (run)
            {
                var nextPrime = Primes.GetNextPrime(start);
                Factor2TextBox.Text = nextPrime.ToString();
                ProductToFactorTextBox.Text = (nextPrime * factor1).ToString();
                Application.DoEvents();
                this.ModMapButton_Click(null, EventArgs.Empty);
                Application.DoEvents();
            }
            AnimationTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            done = true;
            AnimationTimer.Stop();
        }

        private void GraphPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCoorTimer.Stop();
            UpdateCoorTimer.Interval = 500;
            UpdateCoorTimer.Start();
            lastX = e.X;
        }

        private int lastX;

        private void UpdateCoorTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateCoorTimer.Stop();
                if (!string.IsNullOrEmpty(ProductToFactorTextBox.Text) && scalingFactorX != 0)
                {
                    var xScaled = ((BigInteger)(lastX / scalingFactorX)) + areaOfInterestStart;
                    // var xScaled = ((BigInteger)(lastX * scalingFactorX)) + (BigInteger)areaOfInterestStart;
                    var product = this.ParseInput(ProductToFactorTextBox.Text);
                    XLabel.Text = "X: " + xScaled.ToString() + " Y: " + (xScaled * xScaled) % product + " (" + product / xScaled + ")";
                }
            }
            catch(Exception){}
        }

        private void Factor2AnimateButton_Click(object sender, EventArgs e)
        {
            done = false;
            AnimationTimer.Interval = (int)AnimationWaitUpDown.Value;
            AnimationTimer.Tick += (o, args) => this.CycleFactors();
            AnimationTimer.Start();
        }

        private void AnimationWaitUpDown_ValueChanged(object sender, EventArgs e)
        {
            AnimationTimer.Interval = (int)AnimationWaitUpDown.Value;
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            if (Factor1CheckBox.Checked)
            {
                BigInteger factor2 = BigInteger.Parse(Factor2TextBox.Text);
                BigInteger start = BigInteger.Parse(Factor1TextBox.Text);
                var nextPrime = Primes.GetPreviousPrime(start);
                Factor1TextBox.Text = nextPrime.ToString();
                ProductToFactorTextBox.Text = (nextPrime * factor2).ToString();
            }
            else
            {
                BigInteger factor1 = BigInteger.Parse(Factor1TextBox.Text);
                BigInteger start = BigInteger.Parse(Factor2TextBox.Text);
                var nextPrime = Primes.GetPreviousPrime(start);
                Factor2TextBox.Text = nextPrime.ToString();
                ProductToFactorTextBox.Text = (nextPrime * factor1).ToString();
            }
            this.ModMapButton_Click(null, EventArgs.Empty);
        }

        private void NextPrimeButton_Click(object sender, EventArgs e)
        {
            if (Factor1CheckBox.Checked)
            {
                BigInteger factor2 = BigInteger.Parse(Factor2TextBox.Text);
                BigInteger start = BigInteger.Parse(Factor1TextBox.Text);
                var nextPrime = Primes.GetNextPrime(start);
                Factor1TextBox.Text = nextPrime.ToString();
                ProductToFactorTextBox.Text = (nextPrime * factor2).ToString();
            }
            else
            {
                BigInteger factor1 = BigInteger.Parse(Factor1TextBox.Text);
                BigInteger start = BigInteger.Parse(Factor2TextBox.Text);
                var nextPrime = Primes.GetNextPrime(start);
                Factor2TextBox.Text = nextPrime.ToString();
                ProductToFactorTextBox.Text = (nextPrime * factor1).ToString();
            }
            this.ModMapButton_Click(null, EventArgs.Empty);
        }
    }
}
