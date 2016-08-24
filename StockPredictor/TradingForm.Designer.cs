﻿using System.Drawing;

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
            this.tb20 = new System.Windows.Forms.TextBox();
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
            this.SuspendLayout();
            // 
            // btTrade
            // 
            this.btTrade.Image = global::StockPredictor.Properties.Resources.tradeSmall;
            this.btTrade.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btTrade.Location = new System.Drawing.Point(180, 81);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(128, 66);
            this.btTrade.TabIndex = 0;
            this.btTrade.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btTrade.UseVisualStyleBackColor = true;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // tbTrade
            // 
            this.tbTrade.Location = new System.Drawing.Point(207, 52);
            this.tbTrade.Name = "tbTrade";
            this.tbTrade.Size = new System.Drawing.Size(75, 20);
            this.tbTrade.TabIndex = 2;
            // 
            // tb20
            // 
            this.tb20.Location = new System.Drawing.Point(12, 105);
            this.tb20.Name = "tb20";
            this.tb20.Size = new System.Drawing.Size(75, 20);
            this.tb20.TabIndex = 3;
            this.tb20.TextChanged += new System.EventHandler(this.tb20_TextChanged);
            // 
            // tbTradeOutput
            // 
            this.tbTradeOutput.BackColor = System.Drawing.SystemColors.Control;
            this.tbTradeOutput.Location = new System.Drawing.Point(12, 203);
            this.tbTradeOutput.Multiline = true;
            this.tbTradeOutput.Name = "tbTradeOutput";
            this.tbTradeOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTradeOutput.Size = new System.Drawing.Size(494, 285);
            this.tbTradeOutput.TabIndex = 4;
            // 
            // cbSell
            // 
            this.cbSell.AutoSize = true;
            this.cbSell.Location = new System.Drawing.Point(12, 54);
            this.cbSell.Name = "cbSell";
            this.cbSell.Size = new System.Drawing.Size(69, 17);
            this.cbSell.TabIndex = 5;
            this.cbSell.Text = "Short sell";
            this.cbSell.UseVisualStyleBackColor = true;
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(207, 494);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 7;
            this.btTest.Text = "Test";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // cbBag
            // 
            this.cbBag.AutoSize = true;
            this.cbBag.Location = new System.Drawing.Point(393, 31);
            this.cbBag.Name = "cbBag";
            this.cbBag.Size = new System.Drawing.Size(45, 17);
            this.cbBag.TabIndex = 10;
            this.cbBag.Text = "Bag";
            this.cbBag.UseVisualStyleBackColor = true;
            // 
            // cbNamed
            // 
            this.cbNamed.AutoSize = true;
            this.cbNamed.Location = new System.Drawing.Point(393, 82);
            this.cbNamed.Name = "cbNamed";
            this.cbNamed.Size = new System.Drawing.Size(60, 17);
            this.cbNamed.TabIndex = 11;
            this.cbNamed.Text = "Named";
            this.cbNamed.UseVisualStyleBackColor = true;
            // 
            // cbNoun
            // 
            this.cbNoun.AutoSize = true;
            this.cbNoun.Location = new System.Drawing.Point(393, 59);
            this.cbNoun.Name = "cbNoun";
            this.cbNoun.Size = new System.Drawing.Size(52, 17);
            this.cbNoun.TabIndex = 12;
            this.cbNoun.Text = "Noun";
            this.cbNoun.UseVisualStyleBackColor = true;
            // 
            // cb20
            // 
            this.cb20.AutoSize = true;
            this.cb20.Location = new System.Drawing.Point(10, 82);
            this.cb20.Name = "cb20";
            this.cb20.Size = new System.Drawing.Size(77, 17);
            this.cb20.TabIndex = 13;
            this.cb20.Text = "20m Trade";
            this.cb20.UseVisualStyleBackColor = true;
            // 
            // cbRandom
            // 
            this.cbRandom.AutoSize = true;
            this.cbRandom.Location = new System.Drawing.Point(393, 108);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(66, 17);
            this.cbRandom.TabIndex = 14;
            this.cbRandom.Text = "Random";
            this.cbRandom.UseVisualStyleBackColor = true;
            // 
            // cbManual
            // 
            this.cbManual.AutoSize = true;
            this.cbManual.Location = new System.Drawing.Point(12, 31);
            this.cbManual.Name = "cbManual";
            this.cbManual.Size = new System.Drawing.Size(92, 17);
            this.cbManual.TabIndex = 15;
            this.cbManual.Text = "Manual Trade";
            this.cbManual.UseVisualStyleBackColor = true;
            // 
            // cbRetry
            // 
            this.cbRetry.AutoSize = true;
            this.cbRetry.Location = new System.Drawing.Point(218, 31);
            this.cbRetry.Name = "cbRetry";
            this.cbRetry.Size = new System.Drawing.Size(51, 17);
            this.cbRetry.TabIndex = 16;
            this.cbRetry.Text = "Retry";
            this.cbRetry.UseVisualStyleBackColor = true;
            // 
            // tbOpen
            // 
            this.tbOpen.Location = new System.Drawing.Point(238, 148);
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(100, 20);
            this.tbOpen.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "Open Price";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(147, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Close Price";
            // 
            // tbClose
            // 
            this.tbClose.Location = new System.Drawing.Point(239, 179);
            this.tbClose.Name = "tbClose";
            this.tbClose.Size = new System.Drawing.Size(100, 20);
            this.tbClose.TabIndex = 20;
            // 
            // TradingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StockPredictor.Properties.Resources.internet;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(528, 519);
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
            this.Controls.Add(this.tb20);
            this.Controls.Add(this.tbTrade);
            this.Controls.Add(this.btTrade);
            this.Location = new System.Drawing.Point(400, 400);
            this.Name = "TradingForm";
            this.Text = "TradingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btTrade;
        private System.Windows.Forms.TextBox tbTrade;
        private System.Windows.Forms.TextBox tb20;
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
    }
}