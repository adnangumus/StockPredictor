﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockPredictor.Helpers;
using StockPredictor.Tests;

namespace StockPredictor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //allow for an instance of this form to be called and used by other classes
        static Form1 instance;
        public static Form1 Instance
        {
            get {
                try {
                    return instance;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                return instance;
            } 
                }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            instance = this;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            instance = null;
        }
        //method used to append text box for data output
        public void AppendOutputText(String str)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendOutputText), new object[] { str });
                return;
            }

            tbOutput.AppendText(str);
        }

        private void Run_Click(object sender, EventArgs e)
        {
            //time the overall methods
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            tbOutput.Text = string.Empty;
            //intialize input to defualt input. 
            string input = "gild";
            if (!String.IsNullOrEmpty(tbInput.Text))
            {
                //intialize calsses that will be used
                input = tbInput.Text.ToLower();
            }
            RunMethods rm = new RunMethods();
            rm.runStockPredictor(input);
            //   taskC.Wait();
           //time the overall performance
            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;
            Console.WriteLine("Overall Time " + elapsedMs2);
            Console.WriteLine("Seconds " + elapsedMs2 / 1000);
            Console.WriteLine("minutes " + elapsedMs2 / 60000);

            //out put information to the text box
            displayTime(elapsedMs2);
        }
        //display the total time
        private void displayTime(long elapsedMs2)
        {
            AppendOutputText("\r\n");
            AppendOutputText("Overall Time " + elapsedMs2 + "\r\n");
            AppendOutputText("Seconds " + elapsedMs2 / 1000 + "\r\n");
            AppendOutputText("minutes " + elapsedMs2 / 60000 + "\r\n" + "seconds" + (elapsedMs2 % 60000) / 1000);
            AppendOutputText("\r\n");
        }

        private void Stem_Click(object sender, EventArgs e)
        {
            //stem the phrase and word lists
            StemMethods stem = new StemMethods();
            stem.stemAll();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            //clear the text from the output box
            //  tbOutput.Text = string.Empty;
            //  PosTaggerTest ptt = new PosTaggerTest();
            //  ptt.posTaggerNamedTest();
            //    BagOfWordsTest bwt = new BagOfWordsTest();

            //  Task taskA = Task.Run(() => Console.WriteLine("started task A "));
            //  bwt.processBagOfWordsTest();
            //  Task taskA = new Task(() => bwt.processBagOfWordsTest());
            //intialize and process the named and noun entities
            //  PosTagger pt = new PosTagger();
            //   Task taskB = new Task(() => ptt.testNounNamed());
            //stat the tasks         

            //  taskB.Start();
            //  taskA.RunSynchronously();
            // taskC.RunSynchronously();
            //  taskC.RunSynchronously();
            //  taskA.Wait();

            //  taskB.GetAwaiter();
            //  taskA.Wait();
            //  ExcelMethodsTest emt = new ExcelMethodsTest();
            // emt.testSaveDataToExcel();

            YahooMethodsTest yt = new YahooMethodsTest();
            yt.testGetStockPrices();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
