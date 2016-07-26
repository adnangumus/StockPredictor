using System;
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

        private void Test_Click(object sender, EventArgs e)
        {
            //intialize calsses that will be used
            string input = "gild";
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            Mining miner = new Mining();
            GoogleMethods gm = new GoogleMethods();
            List<string> links = new List<string>();
            String url = "https://www.google.com/search?q=nasdaq+" + input + "+News&tbm=nws&tbs=qdr:d";          
            links = gm.getGooglelinks(url);
            string articles = miner.getAllArticles(links);
            //intialize and set up a thread for processing bag of words
            BagOfWords bag = new BagOfWords();
            Task taskA = new Task(() => bag.processBagOfWords(articles, input));
            //intialize and process the named and noun entities
            PosTagger pt = new PosTagger();
            Task taskB = new Task(() => pt.processNamedNoun(articles, input)); 
            //stat the tasks        
            taskA.Start();
            taskB.RunSynchronously();
            //  taskC.RunSynchronously();

            taskA.Wait();
            taskB.Wait();
            //   taskC.Wait();
           //time the overall performance
            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;
            Console.WriteLine("Overall Time " + elapsedMs2);
            Console.WriteLine("Seconds " + elapsedMs2 / 1000);
            Console.WriteLine("minutes " + elapsedMs2 / 60000);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //stem the phrase and word lists
            StemMethods stem = new StemMethods();
            stem.stemAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PosTaggerTest ptt = new PosTaggerTest();
            ptt.posTaggerNamedTest();
        }
    }
}
