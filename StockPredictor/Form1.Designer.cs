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
            this.tbOutput = new StockPredictor.CustomTextBox();
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.btTrade = new System.Windows.Forms.Button();
            this.cbRetry = new System.Windows.Forms.CheckBox();
            this.cbBing = new System.Windows.Forms.CheckBox();
            this.cbGoogle = new System.Windows.Forms.CheckBox();
            this.cbYahoo = new System.Windows.Forms.CheckBox();
            this.cbDelay = new System.Windows.Forms.CheckBox();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.Stem.Location = new System.Drawing.Point(538, 58);
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
            this.Test.Location = new System.Drawing.Point(538, 90);
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
            this.tbConsoleOutput.Size = new System.Drawing.Size(606, 98);
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
            this.tbOutput.Size = new System.Drawing.Size(606, 426);
            this.tbOutput.TabIndex = 12;
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
            this.btTrade.Location = new System.Drawing.Point(33, 83);
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
            this.cbBing.Location = new System.Drawing.Point(472, 94);
            this.cbBing.Name = "cbBing";
            this.cbBing.Size = new System.Drawing.Size(52, 19);
            this.cbBing.TabIndex = 17;
            this.cbBing.Text = "Bing";
            this.cbBing.UseVisualStyleBackColor = true;
            this.cbBing.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbGoogle
            // 
            this.cbGoogle.AutoSize = true;
            this.cbGoogle.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGoogle.Location = new System.Drawing.Point(472, 71);
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
            this.cbYahoo.Location = new System.Drawing.Point(472, 46);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::StockPredictor.Properties.Resources.Invest;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(662, 651);
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
    }
}

