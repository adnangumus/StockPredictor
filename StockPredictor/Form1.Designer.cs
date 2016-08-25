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
            this.btPrice = new System.Windows.Forms.Button();
            this.tbPriceInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbConsoleOutput = new System.Windows.Forms.TextBox();
            this.tbOutput = new StockPredictor.CustomTextBox();
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.btTrade = new System.Windows.Forms.Button();
            this.cbRetry = new System.Windows.Forms.CheckBox();
            this.cbBing = new System.Windows.Forms.CheckBox();
            this.cbGoogle = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(224, 90);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(100, 23);
            this.Run.TabIndex = 0;
            this.Run.Text = "Scan";
            this.Run.UseVisualStyleBackColor = false;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Stem
            // 
            this.Stem.Location = new System.Drawing.Point(415, 61);
            this.Stem.Name = "Stem";
            this.Stem.Size = new System.Drawing.Size(75, 23);
            this.Stem.TabIndex = 1;
            this.Stem.Text = "Stem Lists";
            this.Stem.UseVisualStyleBackColor = true;
            this.Stem.Click += new System.EventHandler(this.Stem_Click);
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(415, 94);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 2;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(226, 62);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(100, 20);
            this.tbInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input Stock Symbol";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btPrice
            // 
            this.btPrice.Location = new System.Drawing.Point(33, 88);
            this.btPrice.Name = "btPrice";
            this.btPrice.Size = new System.Drawing.Size(100, 23);
            this.btPrice.TabIndex = 6;
            this.btPrice.Text = "Price";
            this.btPrice.UseVisualStyleBackColor = true;
            this.btPrice.Click += new System.EventHandler(this.btPrice_Click);
            // 
            // tbPriceInput
            // 
            this.tbPriceInput.Location = new System.Drawing.Point(34, 64);
            this.tbPriceInput.Name = "tbPriceInput";
            this.tbPriceInput.Size = new System.Drawing.Size(100, 20);
            this.tbPriceInput.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Get Price Information";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(227, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Get News Sentiment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Input Stock Symbol";
            // 
            // tbConsoleOutput
            // 
            this.tbConsoleOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.tbConsoleOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.tbConsoleOutput.Location = new System.Drawing.Point(33, 551);
            this.tbConsoleOutput.Multiline = true;
            this.tbConsoleOutput.Name = "tbConsoleOutput";
            this.tbConsoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbConsoleOutput.Size = new System.Drawing.Size(457, 98);
            this.tbConsoleOutput.TabIndex = 13;
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
            this.tbOutput.Size = new System.Drawing.Size(457, 426);
            this.tbOutput.TabIndex = 12;
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Location = new System.Drawing.Point(139, 94);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(79, 17);
            this.cbSave.TabIndex = 14;
            this.cbSave.Text = "Don\'t Save";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // btTrade
            // 
            this.btTrade.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btTrade.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btTrade.Image = global::StockPredictor.Properties.Resources.tradeSmallxx;
            this.btTrade.Location = new System.Drawing.Point(415, 26);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(75, 23);
            this.btTrade.TabIndex = 15;
            this.btTrade.UseVisualStyleBackColor = false;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // cbRetry
            // 
            this.cbRetry.AutoSize = true;
            this.cbRetry.Location = new System.Drawing.Point(139, 64);
            this.cbRetry.Name = "cbRetry";
            this.cbRetry.Size = new System.Drawing.Size(51, 17);
            this.cbRetry.TabIndex = 16;
            this.cbRetry.Text = "Retry";
            this.cbRetry.UseVisualStyleBackColor = true;
            // 
            // cbBing
            // 
            this.cbBing.AutoSize = true;
            this.cbBing.Location = new System.Drawing.Point(332, 94);
            this.cbBing.Name = "cbBing";
            this.cbBing.Size = new System.Drawing.Size(47, 17);
            this.cbBing.TabIndex = 17;
            this.cbBing.Text = "Bing";
            this.cbBing.UseVisualStyleBackColor = true;
            this.cbBing.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbGoogle
            // 
            this.cbGoogle.AutoSize = true;
            this.cbGoogle.Location = new System.Drawing.Point(332, 64);
            this.cbGoogle.Name = "cbGoogle";
            this.cbGoogle.Size = new System.Drawing.Size(60, 17);
            this.cbGoogle.TabIndex = 18;
            this.cbGoogle.Text = "Google";
            this.cbGoogle.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::StockPredictor.Properties.Resources.Invest;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(528, 651);
            this.Controls.Add(this.cbGoogle);
            this.Controls.Add(this.cbBing);
            this.Controls.Add(this.cbRetry);
            this.Controls.Add(this.btTrade);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.tbConsoleOutput);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPriceInput);
            this.Controls.Add(this.btPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.Stem);
            this.Controls.Add(this.Run);
            this.Name = "Form1";
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
        private System.Windows.Forms.Button btPrice;
        private System.Windows.Forms.TextBox tbPriceInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private CustomTextBox tbOutput;
        private System.Windows.Forms.TextBox tbConsoleOutput;
        private System.Windows.Forms.CheckBox cbSave;
        private System.Windows.Forms.Button btTrade;
        private System.Windows.Forms.CheckBox cbRetry;
        private System.Windows.Forms.CheckBox cbBing;
        private System.Windows.Forms.CheckBox cbGoogle;
    }
}

