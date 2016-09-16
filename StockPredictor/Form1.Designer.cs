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
            this.cbRetry = new System.Windows.Forms.CheckBox();
            this.cbBing = new System.Windows.Forms.CheckBox();
            this.cbGoogle = new System.Windows.Forms.CheckBox();
            this.cbYahoo = new System.Windows.Forms.CheckBox();
            this.cbDelay = new System.Windows.Forms.CheckBox();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btOutput = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.lbSentiment = new System.Windows.Forms.Label();
            this.lbBoll = new System.Windows.Forms.Label();
            this.lbPB = new System.Windows.Forms.Label();
            this.lbDividend = new System.Windows.Forms.Label();
            this.lb50MA = new System.Windows.Forms.Label();
            this.lb200MA = new System.Windows.Forms.Label();
            this.lbVerdict = new System.Windows.Forms.Label();
            this.lbPEG = new System.Windows.Forms.Label();
            this.lbRSI = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tbOutput = new StockPredictor.CustomTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btTrade = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // Run
            // 
            this.Run.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Run.Font = new System.Drawing.Font("Old English Text MT", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Run.Location = new System.Drawing.Point(26, 99);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(144, 61);
            this.Run.TabIndex = 0;
            this.Run.Text = "Start";
            this.Run.UseVisualStyleBackColor = false;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Stem
            // 
            this.Stem.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stem.Location = new System.Drawing.Point(1107, 497);
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
            this.Test.Location = new System.Drawing.Point(670, 495);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 2;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // tbInput
            // 
            this.tbInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(26, 62);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(144, 31);
            this.tbInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input Stock Symbol";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Old English Text MT", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(136, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 34);
            this.label3.TabIndex = 9;
            this.label3.Text = "Get News Sentiment";
            // 
            // tbConsoleOutput
            // 
            this.tbConsoleOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.tbConsoleOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.tbConsoleOutput.Location = new System.Drawing.Point(670, 524);
            this.tbConsoleOutput.Multiline = true;
            this.tbConsoleOutput.Name = "tbConsoleOutput";
            this.tbConsoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbConsoleOutput.Size = new System.Drawing.Size(517, 171);
            this.tbConsoleOutput.TabIndex = 13;
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSave.Location = new System.Drawing.Point(67, 109);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(83, 19);
            this.cbSave.TabIndex = 14;
            this.cbSave.Text = "Don\'t Save";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // cbRetry
            // 
            this.cbRetry.AutoSize = true;
            this.cbRetry.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRetry.Location = new System.Drawing.Point(6, 109);
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
            this.cbBing.Location = new System.Drawing.Point(11, 115);
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
            this.cbGoogle.Location = new System.Drawing.Point(11, 181);
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
            this.cbYahoo.Location = new System.Drawing.Point(11, 147);
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
            this.cbDelay.Location = new System.Drawing.Point(6, 102);
            this.cbDelay.Name = "cbDelay";
            this.cbDelay.Size = new System.Drawing.Size(92, 19);
            this.cbDelay.TabIndex = 20;
            this.cbDelay.Text = "1 hour delay";
            this.cbDelay.UseVisualStyleBackColor = true;
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(6, 73);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(100, 20);
            this.tbDelay.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 45);
            this.label5.TabIndex = 22;
            this.label5.Text = "Delay execution: \r\nEnter the amount\r\nof Minutes ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 26);
            this.label9.TabIndex = 29;
            this.label9.Text = "Dividends";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 26);
            this.label2.TabIndex = 35;
            this.label2.Text = "Price to Book";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 26);
            this.label4.TabIndex = 36;
            this.label4.Text = "Bollinger";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 26);
            this.label6.TabIndex = 37;
            this.label6.Text = "P.E.G.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 26);
            this.label7.TabIndex = 38;
            this.label7.Text = "R.S.I.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 26);
            this.label8.TabIndex = 39;
            this.label8.Text = "Sentiment";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 26);
            this.label10.TabIndex = 40;
            this.label10.Text = "50 Day MA";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Old English Text MT", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 26);
            this.label11.TabIndex = 41;
            this.label11.Text = "200 Day MA";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Old English Text MT", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(165, 32);
            this.label12.TabIndex = 44;
            this.label12.Text = "Final Verdict";
            // 
            // btOutput
            // 
            this.btOutput.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOutput.Location = new System.Drawing.Point(26, 171);
            this.btOutput.Name = "btOutput";
            this.btOutput.Size = new System.Drawing.Size(144, 23);
            this.btOutput.TabIndex = 47;
            this.btOutput.Text = "Show Output Console";
            this.btOutput.UseVisualStyleBackColor = true;
            this.btOutput.Click += new System.EventHandler(this.btOutput_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Old English Text MT", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(852, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(199, 34);
            this.label13.TabIndex = 49;
            this.label13.Text = "Output Console";
            // 
            // lbSentiment
            // 
            this.lbSentiment.AutoSize = true;
            this.lbSentiment.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSentiment.Location = new System.Drawing.Point(139, 29);
            this.lbSentiment.Name = "lbSentiment";
            this.lbSentiment.Size = new System.Drawing.Size(0, 23);
            this.lbSentiment.TabIndex = 50;
            // 
            // lbBoll
            // 
            this.lbBoll.AutoSize = true;
            this.lbBoll.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBoll.Location = new System.Drawing.Point(139, 179);
            this.lbBoll.Name = "lbBoll";
            this.lbBoll.Size = new System.Drawing.Size(0, 23);
            this.lbBoll.TabIndex = 51;
            // 
            // lbPB
            // 
            this.lbPB.AutoSize = true;
            this.lbPB.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPB.Location = new System.Drawing.Point(158, 29);
            this.lbPB.Name = "lbPB";
            this.lbPB.Size = new System.Drawing.Size(0, 23);
            this.lbPB.TabIndex = 52;
            // 
            // lbDividend
            // 
            this.lbDividend.AutoSize = true;
            this.lbDividend.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDividend.Location = new System.Drawing.Point(158, 78);
            this.lbDividend.Name = "lbDividend";
            this.lbDividend.Size = new System.Drawing.Size(0, 23);
            this.lbDividend.TabIndex = 53;
            // 
            // lb50MA
            // 
            this.lb50MA.AutoSize = true;
            this.lb50MA.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb50MA.Location = new System.Drawing.Point(158, 129);
            this.lb50MA.Name = "lb50MA";
            this.lb50MA.Size = new System.Drawing.Size(0, 23);
            this.lb50MA.TabIndex = 54;
            // 
            // lb200MA
            // 
            this.lb200MA.AutoSize = true;
            this.lb200MA.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb200MA.Location = new System.Drawing.Point(158, 182);
            this.lb200MA.Name = "lb200MA";
            this.lb200MA.Size = new System.Drawing.Size(0, 23);
            this.lb200MA.TabIndex = 55;
            // 
            // lbVerdict
            // 
            this.lbVerdict.AutoSize = true;
            this.lbVerdict.Font = new System.Drawing.Font("Old English Text MT", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVerdict.Location = new System.Drawing.Point(177, 23);
            this.lbVerdict.Name = "lbVerdict";
            this.lbVerdict.Size = new System.Drawing.Size(0, 32);
            this.lbVerdict.TabIndex = 56;
            // 
            // lbPEG
            // 
            this.lbPEG.AutoSize = true;
            this.lbPEG.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPEG.Location = new System.Drawing.Point(141, 127);
            this.lbPEG.Name = "lbPEG";
            this.lbPEG.Size = new System.Drawing.Size(0, 23);
            this.lbPEG.TabIndex = 57;
            // 
            // lbRSI
            // 
            this.lbRSI.AutoSize = true;
            this.lbRSI.Font = new System.Drawing.Font("Old English Text MT", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRSI.Location = new System.Drawing.Point(139, 75);
            this.lbRSI.Name = "lbRSI";
            this.lbRSI.Size = new System.Drawing.Size(0, 23);
            this.lbRSI.TabIndex = 58;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cbBing);
            this.groupBox1.Controls.Add(this.cbGoogle);
            this.groupBox1.Controls.Add(this.cbYahoo);
            this.groupBox1.Location = new System.Drawing.Point(23, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 206);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 75);
            this.label14.TabIndex = 20;
            this.label14.Text = "Please select the \r\nsearch engines you \r\nwould like to use. \r\nIf you don\'t select" +
    " any\r\nall will be used";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbDelay);
            this.groupBox2.Controls.Add(this.cbDelay);
            this.groupBox2.Location = new System.Drawing.Point(181, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 141);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btOutput);
            this.groupBox3.Controls.Add(this.tbInput);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.Run);
            this.groupBox3.Location = new System.Drawing.Point(380, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(213, 200);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.cbRetry);
            this.groupBox4.Controls.Add(this.cbSave);
            this.groupBox4.Location = new System.Drawing.Point(181, 45);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(168, 134);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(163, 75);
            this.label15.TabIndex = 0;
            this.label15.Text = "If you don\'t want \r\nthe results stored in an\r\nexcel sheet check this box.\r\nRetry " +
    "will over-write the\r\nreults from the last execution";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.lbSentiment);
            this.groupBox5.Controls.Add(this.lbRSI);
            this.groupBox5.Controls.Add(this.lbBoll);
            this.groupBox5.Controls.Add(this.lbPEG);
            this.groupBox5.Location = new System.Drawing.Point(14, 378);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(257, 220);
            this.groupBox5.TabIndex = 63;
            this.groupBox5.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.lbPB);
            this.groupBox6.Controls.Add(this.lbDividend);
            this.groupBox6.Controls.Add(this.lb50MA);
            this.groupBox6.Controls.Add(this.lb200MA);
            this.groupBox6.Location = new System.Drawing.Point(305, 378);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(282, 220);
            this.groupBox6.TabIndex = 64;
            this.groupBox6.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.lbVerdict);
            this.groupBox7.Location = new System.Drawing.Point(94, 614);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(369, 67);
            this.groupBox7.TabIndex = 65;
            this.groupBox7.TabStop = false;
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOutput.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbOutput.Location = new System.Drawing.Point(670, 65);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(517, 421);
            this.tbOutput.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Old English Text MT", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(193, 329);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 34);
            this.label16.TabIndex = 66;
            this.label16.Text = "Results";
            // 
            // btTrade
            // 
            this.btTrade.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btTrade.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btTrade.Image = global::StockPredictor.Properties.Resources.tradeSmallxx;
            this.btTrade.Location = new System.Drawing.Point(506, 638);
            this.btTrade.Name = "btTrade";
            this.btTrade.Size = new System.Drawing.Size(87, 28);
            this.btTrade.TabIndex = 15;
            this.btTrade.UseVisualStyleBackColor = false;
            this.btTrade.Click += new System.EventHandler(this.btTrade_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(646, 714);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Stem);
            this.Controls.Add(this.btTrade);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbConsoleOutput);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jonh86-StockPredictor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btOutput;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbSentiment;
        private System.Windows.Forms.Label lbBoll;
        private System.Windows.Forms.Label lbPB;
        private System.Windows.Forms.Label lbDividend;
        private System.Windows.Forms.Label lb50MA;
        private System.Windows.Forms.Label lb200MA;
        private System.Windows.Forms.Label lbVerdict;
        private System.Windows.Forms.Label lbPEG;
        private System.Windows.Forms.Label lbRSI;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label16;
    }
}

