using System.Drawing;

namespace StockPredictor
{
    partial class TradingForm
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
            this.btTrade = new System.Windows.Forms.Button();
            this.bt20Trade = new System.Windows.Forms.Button();
            this.tbTrade = new System.Windows.Forms.TextBox();
            this.tb20 = new System.Windows.Forms.TextBox();
            this.tbTradeOutput = new System.Windows.Forms.TextBox();
            this.cbSell = new System.Windows.Forms.CheckBox();
            this.btTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btTrade
            // 
            this.btTrade.Location = new System.Drawing.Point(21, 73);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(75, 23);
            this.btTrade.TabIndex = 0;
            this.btTrade.Text = "Trade";
            this.btTrade.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btTrade.UseVisualStyleBackColor = true;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // bt20Trade
            // 
            this.bt20Trade.Location = new System.Drawing.Point(140, 73);
            this.bt20Trade.Name = "bt20Trade";
            this.bt20Trade.Size = new System.Drawing.Size(75, 23);
            this.bt20Trade.TabIndex = 1;
            this.bt20Trade.Text = "20m Trade";
            this.bt20Trade.UseVisualStyleBackColor = true;
            this.bt20Trade.Click += new System.EventHandler(this.bt20Trade_Click);
            // 
            // tbTrade
            // 
            this.tbTrade.Location = new System.Drawing.Point(21, 47);
            this.tbTrade.Name = "tbTrade";
            this.tbTrade.Size = new System.Drawing.Size(75, 20);
            this.tbTrade.TabIndex = 2;
            // 
            // tb20
            // 
            this.tb20.Location = new System.Drawing.Point(140, 47);
            this.tb20.Name = "tb20";
            this.tb20.Size = new System.Drawing.Size(75, 20);
            this.tb20.TabIndex = 3;
            // 
            // tbTradeOutput
            // 
            this.tbTradeOutput.BackColor = System.Drawing.SystemColors.Control;
            this.tbTradeOutput.Location = new System.Drawing.Point(21, 120);
            this.tbTradeOutput.Multiline = true;
            this.tbTradeOutput.Name = "tbTradeOutput";
            this.tbTradeOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTradeOutput.Size = new System.Drawing.Size(304, 167);
            this.tbTradeOutput.TabIndex = 4;
            // 
            // cbSell
            // 
            this.cbSell.AutoSize = true;
            this.cbSell.Location = new System.Drawing.Point(245, 47);
            this.cbSell.Name = "cbSell";
            this.cbSell.Size = new System.Drawing.Size(69, 17);
            this.cbSell.TabIndex = 5;
            this.cbSell.Text = "Short sell";
            this.cbSell.UseVisualStyleBackColor = true;
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(239, 73);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 7;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter Stock Symbol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "20 Minute Price";
            // 
            // TradingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StockPredictor.Properties.Resources.internet;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(337, 299);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.cbSell);
            this.Controls.Add(this.tbTradeOutput);
            this.Controls.Add(this.tb20);
            this.Controls.Add(this.tbTrade);
            this.Controls.Add(this.bt20Trade);
            this.Controls.Add(this.btTrade);
            this.Location = new System.Drawing.Point(400, 400);
            this.Name = "TradingForm";
            this.Text = "TradingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btTrade;
        private System.Windows.Forms.Button bt20Trade;
        private System.Windows.Forms.TextBox tbTrade;
        private System.Windows.Forms.TextBox tb20;
        private System.Windows.Forms.TextBox tbTradeOutput;
        private System.Windows.Forms.CheckBox cbSell;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}