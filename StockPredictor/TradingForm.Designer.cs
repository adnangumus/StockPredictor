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
            this.tbTrade = new System.Windows.Forms.TextBox();
            this.tbTradeOutput = new System.Windows.Forms.TextBox();
            this.cbSell = new System.Windows.Forms.CheckBox();
            this.btTest = new System.Windows.Forms.Button();
            this.cbBag = new System.Windows.Forms.CheckBox();
            this.cbNamed = new System.Windows.Forms.CheckBox();
            this.cbNoun = new System.Windows.Forms.CheckBox();
            this.cb20 = new System.Windows.Forms.CheckBox();
            this.cbRandom = new System.Windows.Forms.CheckBox();
            this.cbManual = new System.Windows.Forms.CheckBox();
            this.cbRetry = new System.Windows.Forms.CheckBox();
            this.tbOpen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbClose = new System.Windows.Forms.TextBox();
            this.cbStrong = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btPrice = new System.Windows.Forms.Button();
            this.cbLong = new System.Windows.Forms.CheckBox();
            this.cbSellLong = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btTrade
            // 
            this.btTrade.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTrade.Image = global::StockPredictor.Properties.Resources.tradeSmall;
            this.btTrade.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btTrade.Location = new System.Drawing.Point(209, 164);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(128, 66);
            this.btTrade.TabIndex = 0;
            this.btTrade.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btTrade.UseVisualStyleBackColor = true;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // tbTrade
            // 
            this.tbTrade.Location = new System.Drawing.Point(232, 37);
            this.tbTrade.Name = "tbTrade";
            this.tbTrade.Size = new System.Drawing.Size(75, 20);
            this.tbTrade.TabIndex = 2;
            // 
            // tbTradeOutput
            // 
            this.tbTradeOutput.BackColor = System.Drawing.SystemColors.Control;
            this.tbTradeOutput.Location = new System.Drawing.Point(9, 236);
            this.tbTradeOutput.Multiline = true;
            this.tbTradeOutput.Name = "tbTradeOutput";
            this.tbTradeOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTradeOutput.Size = new System.Drawing.Size(550, 273);
            this.tbTradeOutput.TabIndex = 4;
            // 
            // cbSell
            // 
            this.cbSell.AutoSize = true;
            this.cbSell.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSell.Location = new System.Drawing.Point(9, 61);
            this.cbSell.Name = "cbSell";
            this.cbSell.Size = new System.Drawing.Size(89, 22);
            this.cbSell.TabIndex = 5;
            this.cbSell.Text = "Short sell";
            this.cbSell.UseVisualStyleBackColor = true;
            // 
            // btTest
            // 
            this.btTest.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTest.Location = new System.Drawing.Point(232, 515);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 34);
            this.btTest.TabIndex = 7;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // cbBag
            // 
            this.cbBag.AutoSize = true;
            this.cbBag.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBag.Location = new System.Drawing.Point(411, 5);
            this.cbBag.Name = "cbBag";
            this.cbBag.Size = new System.Drawing.Size(52, 22);
            this.cbBag.TabIndex = 10;
            this.cbBag.Text = "Bag";
            this.cbBag.UseVisualStyleBackColor = true;
            // 
            // cbNamed
            // 
            this.cbNamed.AutoSize = true;
            this.cbNamed.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNamed.Location = new System.Drawing.Point(411, 71);
            this.cbNamed.Name = "cbNamed";
            this.cbNamed.Size = new System.Drawing.Size(71, 22);
            this.cbNamed.TabIndex = 11;
            this.cbNamed.Text = "Named";
            this.cbNamed.UseVisualStyleBackColor = true;
            // 
            // cbNoun
            // 
            this.cbNoun.AutoSize = true;
            this.cbNoun.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNoun.Location = new System.Drawing.Point(411, 35);
            this.cbNoun.Name = "cbNoun";
            this.cbNoun.Size = new System.Drawing.Size(62, 22);
            this.cbNoun.TabIndex = 12;
            this.cbNoun.Text = "Noun";
            this.cbNoun.UseVisualStyleBackColor = true;
            // 
            // cb20
            // 
            this.cb20.AutoSize = true;
            this.cb20.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb20.Location = new System.Drawing.Point(232, 71);
            this.cb20.Name = "cb20";
            this.cb20.Size = new System.Drawing.Size(95, 22);
            this.cb20.TabIndex = 13;
            this.cb20.Text = "20m Trade";
            this.cb20.UseVisualStyleBackColor = true;
            // 
            // cbRandom
            // 
            this.cbRandom.AutoSize = true;
            this.cbRandom.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRandom.Location = new System.Drawing.Point(411, 107);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(81, 22);
            this.cbRandom.TabIndex = 14;
            this.cbRandom.Text = "Random";
            this.cbRandom.UseVisualStyleBackColor = true;
            // 
            // cbManual
            // 
            this.cbManual.AutoSize = true;
            this.cbManual.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbManual.Location = new System.Drawing.Point(9, 31);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(119, 22);
            this.cbManual.TabIndex = 15;
            this.cbManual.Text = "Manual Trade";
            this.cbManual.UseVisualStyleBackColor = true;
            // 
            // cbRetry
            // 
            this.cbRetry.AutoSize = true;
            this.cbRetry.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRetry.Location = new System.Drawing.Point(9, 131);
            this.cbRetry.Name = "cbRetry";
            this.cbRetry.Size = new System.Drawing.Size(62, 22);
            this.cbRetry.TabIndex = 16;
            this.cbRetry.Text = "Retry";
            this.cbRetry.UseVisualStyleBackColor = true;
            // 
            // tbOpen
            // 
            this.tbOpen.Location = new System.Drawing.Point(265, 110);
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(100, 20);
            this.tbOpen.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(177, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Open Price";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(177, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Close Price";
            // 
            // tbClose
            // 
            this.tbClose.Location = new System.Drawing.Point(265, 137);
            this.tbClose.Name = "tbClose";
            this.tbClose.Size = new System.Drawing.Size(100, 20);
            this.tbClose.TabIndex = 20;
            // 
            // cbStrong
            // 
            this.cbStrong.AutoSize = true;
            this.cbStrong.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStrong.Location = new System.Drawing.Point(9, 94);
            this.cbStrong.Name = "cbStrong";
            this.cbStrong.Size = new System.Drawing.Size(87, 22);
            this.cbStrong.TabIndex = 21;
            this.cbStrong.Text = "Is Strong";
            this.cbStrong.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(229, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 23;
            this.label4.Text = "Enter Ticker";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "Get Price Information";
            // 
            // btPrice
            // 
            this.btPrice.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPrice.Location = new System.Drawing.Point(9, 203);
            this.btPrice.Name = "btPrice";
            this.btPrice.Size = new System.Drawing.Size(100, 27);
            this.btPrice.TabIndex = 24;
            this.btPrice.Text = "Get Prices";
            this.btPrice.UseVisualStyleBackColor = true;
            this.btPrice.Click += new System.EventHandler(this.btPrice_Click);
            // 
            // cbLong
            // 
            this.cbLong.AutoSize = true;
            this.cbLong.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLong.Location = new System.Drawing.Point(411, 144);
            this.cbLong.Name = "cbLong";
            this.cbLong.Size = new System.Drawing.Size(103, 22);
            this.cbLong.TabIndex = 27;
            this.cbLong.Text = "Long Trade";
            this.cbLong.UseVisualStyleBackColor = true;
            // 
            // cbSellLong
            // 
            this.cbSellLong.AutoSize = true;
            this.cbSellLong.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSellLong.Location = new System.Drawing.Point(411, 187);
            this.cbSellLong.Name = "cbSellLong";
            this.cbSellLong.Size = new System.Drawing.Size(149, 22);
            this.cbSellLong.TabIndex = 28;
            this.cbSellLong.Text = "Sell Long Position";
            this.cbSellLong.UseVisualStyleBackColor = true;
            // 
            // TradingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(571, 549);
            this.Controls.Add(this.cbSellLong);
            this.Controls.Add(this.cbLong);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btPrice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbStrong);
            this.Controls.Add(this.tbClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOpen);
            this.Controls.Add(this.cbRetry);
            this.Controls.Add(this.cbManual);
            this.Controls.Add(this.cbRandom);
            this.Controls.Add(this.cb20);
            this.Controls.Add(this.cbNoun);
            this.Controls.Add(this.cbNamed);
            this.Controls.Add(this.cbBag);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.cbSell);
            this.Controls.Add(this.tbTradeOutput);
            this.Controls.Add(this.tbTrade);
            this.Controls.Add(this.btTrade);
            this.Location = new System.Drawing.Point(400, 400);
            this.Name = "TradingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TradingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btTrade;
        private System.Windows.Forms.TextBox tbTrade;
        private System.Windows.Forms.TextBox tbTradeOutput;
        private System.Windows.Forms.CheckBox cbSell;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.CheckBox cbBag;
        private System.Windows.Forms.CheckBox cbNamed;
        private System.Windows.Forms.CheckBox cbNoun;
        private System.Windows.Forms.CheckBox cb20;
        private System.Windows.Forms.CheckBox cbRandom;
        private System.Windows.Forms.CheckBox cbManual;
        private System.Windows.Forms.CheckBox cbRetry;
        private System.Windows.Forms.TextBox tbOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbClose;
        private System.Windows.Forms.CheckBox cbStrong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btPrice;
        private System.Windows.Forms.CheckBox cbLong;
        private System.Windows.Forms.CheckBox cbSellLong;
    }
}