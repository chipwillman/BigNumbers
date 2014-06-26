namespace Graph
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PrevButton = new System.Windows.Forms.Button();
            this.NextPrimeButton = new System.Windows.Forms.Button();
            this.Factor2AnimateButton = new System.Windows.Forms.Button();
            this.XLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.AnimationWaitUpDown = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.AreaOfInterestEndTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AreaOfInterestStartTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Factor2TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Factor1TextBox = new System.Windows.Forms.TextBox();
            this.ModMapButton = new System.Windows.Forms.Button();
            this.FactorButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ProductToFactorTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GraphPictureBox = new System.Windows.Forms.PictureBox();
            this.UpdateCoorTimer = new System.Windows.Forms.Timer(this.components);
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.Factor1CheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationWaitUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Factor1CheckBox);
            this.panel1.Controls.Add(this.PrevButton);
            this.panel1.Controls.Add(this.NextPrimeButton);
            this.panel1.Controls.Add(this.Factor2AnimateButton);
            this.panel1.Controls.Add(this.XLabel);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.PauseButton);
            this.panel1.Controls.Add(this.AnimationWaitUpDown);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.AreaOfInterestEndTextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.AreaOfInterestStartTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Factor2TextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Factor1TextBox);
            this.panel1.Controls.Add(this.ModMapButton);
            this.panel1.Controls.Add(this.FactorButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ProductToFactorTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 166);
            this.panel1.TabIndex = 0;
            // 
            // PrevButton
            // 
            this.PrevButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrevButton.Location = new System.Drawing.Point(252, 136);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(24, 23);
            this.PrevButton.TabIndex = 5;
            this.PrevButton.Text = "<";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // NextPrimeButton
            // 
            this.NextPrimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextPrimeButton.Location = new System.Drawing.Point(282, 136);
            this.NextPrimeButton.Name = "NextPrimeButton";
            this.NextPrimeButton.Size = new System.Drawing.Size(24, 23);
            this.NextPrimeButton.TabIndex = 6;
            this.NextPrimeButton.Text = ">";
            this.NextPrimeButton.UseVisualStyleBackColor = true;
            this.NextPrimeButton.Click += new System.EventHandler(this.NextPrimeButton_Click);
            // 
            // Factor2AnimateButton
            // 
            this.Factor2AnimateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Factor2AnimateButton.Location = new System.Drawing.Point(347, 136);
            this.Factor2AnimateButton.Name = "Factor2AnimateButton";
            this.Factor2AnimateButton.Size = new System.Drawing.Size(94, 23);
            this.Factor2AnimateButton.TabIndex = 7;
            this.Factor2AnimateButton.Text = "Cycle Primes";
            this.Factor2AnimateButton.UseVisualStyleBackColor = true;
            this.Factor2AnimateButton.Click += new System.EventHandler(this.Factor2AnimateButton_Click);
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(12, 141);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(17, 13);
            this.XLabel.TabIndex = 16;
            this.XLabel.Text = "X:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(758, 136);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PauseButton.Location = new System.Drawing.Point(677, 136);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 10;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // AnimationWaitUpDown
            // 
            this.AnimationWaitUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AnimationWaitUpDown.Increment = new decimal(new int[] {
            125,
            0,
            0,
            0});
            this.AnimationWaitUpDown.Location = new System.Drawing.Point(577, 139);
            this.AnimationWaitUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.AnimationWaitUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AnimationWaitUpDown.Name = "AnimationWaitUpDown";
            this.AnimationWaitUpDown.Size = new System.Drawing.Size(81, 20);
            this.AnimationWaitUpDown.TabIndex = 9;
            this.AnimationWaitUpDown.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.AnimationWaitUpDown.ValueChanged += new System.EventHandler(this.AnimationWaitUpDown_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(496, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Animate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "End:";
            // 
            // AreaOfInterestEndTextBox
            // 
            this.AreaOfInterestEndTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AreaOfInterestEndTextBox.Location = new System.Drawing.Point(114, 110);
            this.AreaOfInterestEndTextBox.Name = "AreaOfInterestEndTextBox";
            this.AreaOfInterestEndTextBox.Size = new System.Drawing.Size(881, 20);
            this.AreaOfInterestEndTextBox.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Start:";
            // 
            // AreaOfInterestStartTextBox
            // 
            this.AreaOfInterestStartTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AreaOfInterestStartTextBox.Location = new System.Drawing.Point(114, 84);
            this.AreaOfInterestStartTextBox.Name = "AreaOfInterestStartTextBox";
            this.AreaOfInterestStartTextBox.Size = new System.Drawing.Size(881, 20);
            this.AreaOfInterestStartTextBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Factor 2:";
            // 
            // Factor2TextBox
            // 
            this.Factor2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Factor2TextBox.Location = new System.Drawing.Point(114, 58);
            this.Factor2TextBox.Name = "Factor2TextBox";
            this.Factor2TextBox.Size = new System.Drawing.Size(881, 20);
            this.Factor2TextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Factor 1:";
            // 
            // Factor1TextBox
            // 
            this.Factor1TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Factor1TextBox.Location = new System.Drawing.Point(114, 32);
            this.Factor1TextBox.Name = "Factor1TextBox";
            this.Factor1TextBox.Size = new System.Drawing.Size(881, 20);
            this.Factor1TextBox.TabIndex = 1;
            // 
            // ModMapButton
            // 
            this.ModMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ModMapButton.Location = new System.Drawing.Point(920, 136);
            this.ModMapButton.Name = "ModMapButton";
            this.ModMapButton.Size = new System.Drawing.Size(75, 23);
            this.ModMapButton.TabIndex = 13;
            this.ModMapButton.Text = "Mod map";
            this.ModMapButton.UseVisualStyleBackColor = true;
            this.ModMapButton.Click += new System.EventHandler(this.ModMapButton_Click);
            // 
            // FactorButton
            // 
            this.FactorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FactorButton.Location = new System.Drawing.Point(839, 136);
            this.FactorButton.Name = "FactorButton";
            this.FactorButton.Size = new System.Drawing.Size(75, 23);
            this.FactorButton.TabIndex = 12;
            this.FactorButton.Text = "Factor";
            this.FactorButton.UseVisualStyleBackColor = true;
            this.FactorButton.Click += new System.EventHandler(this.FactorButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number To Factor:";
            // 
            // ProductToFactorTextBox
            // 
            this.ProductToFactorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductToFactorTextBox.Location = new System.Drawing.Point(114, 6);
            this.ProductToFactorTextBox.Name = "ProductToFactorTextBox";
            this.ProductToFactorTextBox.Size = new System.Drawing.Size(881, 20);
            this.ProductToFactorTextBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.GraphPictureBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1007, 335);
            this.panel2.TabIndex = 1;
            // 
            // GraphPictureBox
            // 
            this.GraphPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphPictureBox.Location = new System.Drawing.Point(0, 0);
            this.GraphPictureBox.Name = "GraphPictureBox";
            this.GraphPictureBox.Size = new System.Drawing.Size(1007, 335);
            this.GraphPictureBox.TabIndex = 0;
            this.GraphPictureBox.TabStop = false;
            this.GraphPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphPictureBox_MouseMove);
            // 
            // UpdateCoorTimer
            // 
            this.UpdateCoorTimer.Tick += new System.EventHandler(this.UpdateCoorTimer_Tick);
            // 
            // Factor1CheckBox
            // 
            this.Factor1CheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Factor1CheckBox.AutoSize = true;
            this.Factor1CheckBox.Location = new System.Drawing.Point(148, 140);
            this.Factor1CheckBox.Name = "Factor1CheckBox";
            this.Factor1CheckBox.Size = new System.Drawing.Size(65, 17);
            this.Factor1CheckBox.TabIndex = 17;
            this.Factor1CheckBox.Text = "Factor 1";
            this.Factor1CheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 501);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Graph";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationWaitUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GraphPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Factor2TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Factor1TextBox;
        private System.Windows.Forms.Button ModMapButton;
        private System.Windows.Forms.Button FactorButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProductToFactorTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox GraphPictureBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AreaOfInterestEndTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox AreaOfInterestStartTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown AnimationWaitUpDown;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Timer UpdateCoorTimer;
        private System.Windows.Forms.Button Factor2AnimateButton;
        private System.Windows.Forms.Timer AnimationTimer;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.Button NextPrimeButton;
        private System.Windows.Forms.CheckBox Factor1CheckBox;
    }
}

