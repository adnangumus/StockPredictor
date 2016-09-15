namespace StockPredictor
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
            this.Run = new System.Windows.Forms.Button();
            this.Stem = new System.Windows.Forms.Button();
            this.Test = new System.Windows.Forms.Button();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbConsoleOutput = new System.Windows.Forms.TextBox();
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.btTrade = new System.Windows.Forms.Button();
            this.cbRetry = new System.Windows.Forms.CheckBox();
            this.cbBing = new System.Windows.Forms.CheckBox();
            this.cbGoogle = new System.Windows.Forms.CheckBox();
            this.cbYahoo = new System.Windows.Forms.CheckBox();
            this.cbDelay = new System.Windows.Forms.CheckBox();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSentiment = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbRSI = new System.Windows.Forms.TextBox();
            this.tbPEG = new System.Windows.Forms.TextBox();
            this.tbBollinger = new System.Windows.Forms.TextBox();
            this.tbPB = new System.Windows.Forms.TextBox();
            this.tbDividends = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tb50MA = new System.Windows.Forms.TextBox();
            this.tb200MA = new System.Windows.Forms.TextBox();
            this.tbOutput = new StockPredictor.CustomTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbVerdict = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Run
            // 
            this.Run.Image = global::StockPredictor.Properties.Resources.startble;
            this.Run.Location = new System.Drawing.Point(140, 79);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(109, 34);
            this.Run.TabIndex = 0;
            this.Run.UseVisualStyleBackColor = false;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Stem
            // 
            this.Stem.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stem.Location = new System.Drawing.Point(564, 49);
            this.Stem.Name = "Stem";
            this.Stem.Size = new System.Drawing.Size(75, 23);
            this.Stem.TabIndex = 1;
            this.Stem.Text = "Stem Lists";
            this.Stem.UseVisualStyleBackColor = true;
            this.Stem.Click += new System.EventHandler(this.Stem_Click);
            // 
            // Test
            // 
            this.Test.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Test.Location = new System.Drawing.Point(564, 78);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 2;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(142, 58);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(107, 20);
            this.tbInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(139, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input Stock Symbol";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(137, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Get News Sentiment";
            // 
            // tbConsoleOutput
            // 
            this.tbConsoleOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.tbConsoleOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.tbConsoleOutput.Location = new System.Drawing.Point(33, 551);
            this.tbConsoleOutput.Multiline = true;
            this.tbConsoleOutput.Name = "tbConsoleOutput";
            this.tbConsoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbConsoleOutput.Size = new System.Drawing.Size(616, 98);
            this.tbConsoleOutput.TabIndex = 13;
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSave.Location = new System.Drawing.Point(255, 58);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(83, 19);
            this.cbSave.TabIndex = 14;
            this.cbSave.Text = "Don\'t Save";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // btTrade
            // 
            this.btTrade.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btTrade.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btTrade.Image = global::StockPredictor.Properties.Resources.tradeSmallxx;
            this.btTrade.Location = new System.Drawing.Point(459, 75);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(87, 28);
            this.btTrade.TabIndex = 15;
            this.btTrade.UseVisualStyleBackColor = false;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // cbRetry
            // 
            this.cbRetry.AutoSize = true;
            this.cbRetry.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRetry.Location = new System.Drawing.Point(255, 88);
            this.cbRetry.Name = "cbRetry";
            this.cbRetry.Size = new System.Drawing.Size(55, 19);
            this.cbRetry.TabIndex = 16;
            this.cbRetry.Text = "Retry";
            this.cbRetry.UseVisualStyleBackColor = true;
            // 
            // cbBing
            // 
            this.cbBing.AutoSize = true;
            this.cbBing.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBing.Location = new System.Drawing.Point(47, 90);
            this.cbBing.Name = "cbBing";
            this.cbBing.Size = new System.Drawing.Size(52, 19);
            this.cbBing.TabIndex = 17;
            this.cbBing.Text = "Bing";
            this.cbBing.UseVisualStyleBackColor = true;
            // 
            // cbGoogle
            // 
            this.cbGoogle.AutoSize = true;
            this.cbGoogle.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGoogle.Location = new System.Drawing.Point(47, 62);
            this.cbGoogle.Name = "cbGoogle";
            this.cbGoogle.Size = new System.Drawing.Size(62, 19);
            this.cbGoogle.TabIndex = 18;
            this.cbGoogle.Text = "Google";
            this.cbGoogle.UseVisualStyleBackColor = true;
            // 
            // cbYahoo
            // 
            this.cbYahoo.AutoSize = true;
            this.cbYahoo.Font = new System.Drawing.Font("Modern No. 20", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbYahoo.Location = new System.Drawing.Point(47, 37);
            this.cbYahoo.Name = "cbYahoo";
            this.cbYahoo.Size = new System.Drawing.Size(55, 18);
            this.cbYahoo.TabIndex = 19;
            this.cbYahoo.Text = "Yahoo";
            this.cbYahoo.UseVisualStyleBackColor = true;
            // 
            // cbDelay
            // 
            this.cbDelay.AutoSize = true;
            this.cbDelay.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDelay.Location = new System.Drawing.Point(341, 84);
            this.cbDelay.Name = "cbDelay";
            this.cbDelay.Size = new System.Drawing.Size(92, 19);
            this.cbDelay.TabIndex = 20;
            this.cbDelay.Text = "1 hour delay";
            this.cbDelay.UseVisualStyleBackColor = true;
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(341, 58);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(100, 20);
            this.tbDelay.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(338, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "Delay: Minutes";
            // 
            // tbSentiment
            // 
            this.tbSentiment.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSentiment.Location = new System.Drawing.Point(833, 192);
            this.tbSentiment.Name = "tbSentiment";
            this.tbSentiment.Size = new System.Drawing.Size(100, 25);
            this.tbSentiment.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(685, 417);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 26);
            this.label9.TabIndex = 29;
            this.label9.Text = "Dividends";
            // 
            // tbRSI
            // 
            this.tbRSI.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRSI.Location = new System.Drawing.Point(833, 136);
            this.tbRSI.Name = "tbRSI";
            this.tbRSI.Size = new System.Drawing.Size(100, 25);
            this.tbRSI.TabIndex = 30;
            // 
            // tbPEG
            // 
            this.tbPEG.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPEG.Location = new System.Drawing.Point(833, 246);
            this.tbPEG.Name = "tbPEG";
            this.tbPEG.Size = new System.Drawing.Size(100, 25);
            this.tbPEG.TabIndex = 31;
            // 
            // tbBollinger
            // 
            this.tbBollinger.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBollinger.Location = new System.Drawing.Point(833, 301);
            this.tbBollinger.Name = "tbBollinger";
            this.tbBollinger.Size = new System.Drawing.Size(100, 25);
            this.tbBollinger.TabIndex = 32;
            // 
            // tbPB
            // 
            this.tbPB.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPB.Location = new System.Drawing.Point(833, 355);
            this.tbPB.Name = "tbPB";
            this.tbPB.Size = new System.Drawing.Size(100, 25);
            this.tbPB.TabIndex = 33;
            // 
            // tbDividends
            // 
            this.tbDividends.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDividends.Location = new System.Drawing.Point(833, 420);
            this.tbDividends.Name = "tbDividends";
            this.tbDividends.Size = new System.Drawing.Size(100, 25);
            this.tbDividends.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(685, 355);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 26);
            this.label2.TabIndex = 35;
            this.label2.Text = "Price to Book";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(685, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 26);
            this.label4.TabIndex = 36;
            this.label4.Text = "Bollinger";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(685, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 26);
            this.label6.TabIndex = 37;
            this.label6.Text = "P.E.G.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(685, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 26);
            this.label7.TabIndex = 38;
            this.label7.Text = "R.S.I.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(685, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 26);
            this.label8.TabIndex = 39;
            this.label8.Text = "Sentiment";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(685, 474);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 26);
            this.label10.TabIndex = 40;
            this.label10.Text = "50 Day MA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(685, 533);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 26);
            this.label11.TabIndex = 41;
            this.label11.Text = "200 Day MA";
            // 
            // tb50MA
            // 
            this.tb50MA.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb50MA.Location = new System.Drawing.Point(833, 477);
            this.tb50MA.Name = "tb50MA";
            this.tb50MA.Size = new System.Drawing.Size(100, 25);
            this.tb50MA.TabIndex = 42;
            // 
            // tb200MA
            // 
            this.tb200MA.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb200MA.Location = new System.Drawing.Point(833, 533);
            this.tb200MA.Name = "tb200MA";
            this.tb200MA.Size = new System.Drawing.Size(100, 25);
            this.tb200MA.TabIndex = 43;
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOutput.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOutput.Location = new System.Drawing.Point(33, 119);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(616, 426);
            this.tbOutput.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(685, 596);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 26);
            this.label12.TabIndex = 44;
            this.label12.Text = "Final Verdict";
            // 
            // tbVerdict
            // 
            this.tbVerdict.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVerdict.Location = new System.Drawing.Point(833, 596);
            this.tbVerdict.Name = "tbVerdict";
            this.tbVerdict.Size = new System.Drawing.Size(100, 25);
            this.tbVerdict.TabIndex = 45;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::StockPredictor.Properties.Resources.Invest;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(982, 651);
            this.Controls.Add(this.tbVerdict);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tb200MA);
            this.Controls.Add(this.tb50MA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDividends);
            this.Controls.Add(this.tbPB);
            this.Controls.Add(this.tbBollinger);
            this.Controls.Add(this.tbPEG);
            this.Controls.Add(this.tbRSI);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbSentiment);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDelay);
            this.Controls.Add(this.cbDelay);
            this.Controls.Add(this.cbYahoo);
            this.Controls.Add(this.cbGoogle);
            this.Controls.Add(this.cbBing);
            this.Controls.Add(this.cbRetry);
            this.Controls.Add(this.btTrade);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.tbConsoleOutput);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.Stem);
            this.Controls.Add(this.Run);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jonh86-StockPredictor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Button Stem;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.Label label3;
        private CustomTextBox tbOutput;
        private System.Windows.Forms.TextBox tbConsoleOutput;
        private System.Windows.Forms.CheckBox cbSave;
        private System.Windows.Forms.Button btTrade;
        private System.Windows.Forms.CheckBox cbRetry;
        private System.Windows.Forms.CheckBox cbBing;
        private System.Windows.Forms.CheckBox cbGoogle;
        private System.Windows.Forms.CheckBox cbYahoo;
        private System.Windows.Forms.CheckBox cbDelay;
        private System.Windows.Forms.TextBox tbDelay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSentiment;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbRSI;
        private System.Windows.Forms.TextBox tbPEG;
        private System.Windows.Forms.TextBox tbBollinger;
        private System.Windows.Forms.TextBox tbPB;
        private System.Windows.Forms.TextBox tbDividends;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb50MA;
        private System.Windows.Forms.TextBox tb200MA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbVerdict;
    }
}

