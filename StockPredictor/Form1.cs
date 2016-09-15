using System;
using System.ComponentModel;
using System.Windows.Forms;
using StockPredictor.Helpers;
using System.Collections;
using StockPredictor.Models;
using System.Collections.Generic;
using StockPredictor.Tests;
using System.Drawing;

namespace StockPredictor
{
    public partial class Form1 : Form
    {
        //variables used for the timer class
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
      
        //ranging from -2 to 2 Strong sell sell neutral buy strong buy
        public int rsi { get; set; }
        //the real rsi
        public double realRSI { get; set; }
        //the scores from the moving average ranging from -2 to 2 Strong sell sell neutral buy strong buy
        public string moving200 {get;set;}
        public string moving50 { get; set;}
        public string priceBook { get; set; }
        public string peg { get; set; }
        public string dividend { get; set; }
        public Hashtable fundamentals {get; set;}
        public string verdict { get; set; }
        public List<HistoricalStock> historicalPriceData { get; set; }
        public double TwoDayOldClosePrice { get; set; }
        public double lastClosePrice { get; set; }
        public string upperBand { get; set; }
        public string lowerBand { get; set; }
        public string mean { get; set; }
        public int bollingerVerdict { get; set; }


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

        public bool isRetry()
        {
            return cbRetry.Checked;
        } 
        public bool dontSave()
        {
            return cbSave.Checked;
        }  
        //check if the user wants to use bing
        public bool useBing()
        {
            return cbBing.Checked;
        }
        //check if the google method is checked
        public bool useGoogle()
        {
            return cbGoogle.Checked;
        }
        //check if the yahoo method is checked
        public bool useYahoo()
        {
            return cbYahoo.Checked;
        }
        //get tbOutput's text
        public string getTBOutputText()
        {
            return tbOutput.Text;
        }

        private void Run_Click(object sender, EventArgs e)
        {
           
            //  tbOutput.Text = string.Empty;
            // Set up background worker object & hook up handlers
            BackgroundWorker bgWorker;
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(checkTimer);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            // Launch background thread to do the work of reading the file.  
            bgWorker.RunWorkerAsync();
            //close the please wait dialogue
         
        }
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // AppendOutputText("\r\nAll processes are completed");
        }

        private void checkTimer(object sender, DoWorkEventArgs e)
        {
            //check the value for delaying in textbox  60000 = 1 minute
            int delay =0;
            try { delay = Int32.Parse(tbDelay.Text); } catch (Exception) { }
                delay *= 60000;
            //the program will sleep for an hour = 3600000
            if (cbDelay.Checked) { delay += 3600000; };
            // Form1.instance.AppendOutputText("\r\n" + "Delaying process for " + delay/ 60000 + " minutes" + "\r\n");
            Console.WriteLine("Delaying process for " + delay / 60000 + " minutes");
            //calculate the time of execution
            DateTime currentTime = DateTime.Now;
            double minuts = Convert.ToDouble(delay / 60000 + 1) ;
            currentTime = currentTime.AddMinutes(minuts);
            Console.WriteLine("Execution time : " + currentTime);
            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                runScans();
                timer.Dispose();
            },
                        null, delay +1, System.Threading.Timeout.Infinite);
           
        }
       
        // handle the scanning in a back ground worker
        private void runScans()
        {        

               
          
            //time the overall methods
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            bool dontSave = cbSave.Checked;
            //intialize input to defualt input. 
            string input = tbInput.Text.ToLower();
            RunMethods rm = new RunMethods();
            
            if (String.IsNullOrEmpty(tbInput.Text) || tbInput.Text.ToLower() == "bio")
            {               
                rm.runStockPredictor("gild", dontSave);
                rm.runStockPredictor("hznp", dontSave);
                rm.runStockPredictor("biib", dontSave);
                rm.runStockPredictor("celg", dontSave);
               // rm.runStockPredictor("ibb", dontSave);
               
                return;
            }

            rm.runStockPredictor(input, dontSave);
            //   taskC.Wait();
            //time the overall performance
            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;
            Console.WriteLine("Overall Time " + elapsedMs2);
            Console.WriteLine("Seconds " + elapsedMs2 / 1000);

            //out put information to the text box
             displayTime(elapsedMs2);
          
        }
        //display the total time
        private void displayTime(long elapsedMs2)
        {
            AppendOutputText("\r\n All processes completed"+"\r\n");
            AppendOutputText("Overall Time " + elapsedMs2 + "\r\n");
            AppendOutputText("Seconds " + elapsedMs2 / 1000 + "\r\n");
            AppendOutputText("minutes " + elapsedMs2 / 60000 + "    " + "seconds" + (elapsedMs2 % 60000) / 1000);
            AppendOutputText("\r\n");
        }

        private void Stem_Click(object sender, EventArgs e)
        {
            //stem the phrase and word lists
            StemMethods stem = new StemMethods();
            stem.stemAll();
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }
        

        //thread safe console output writer
        private DelegateTextWriter threadSafeWriter() { 
        var threadSafeLogWriter = new DelegateTextWriter(str => {
            Action updateCmd = () => {
                tbConsoleOutput.AppendText(str);
                tbConsoleOutput.Show();
            };
            if (tbConsoleOutput.InvokeRequired) tbConsoleOutput.BeginInvoke(updateCmd);
            else updateCmd();
        });
            return threadSafeLogWriter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            //  ALlow main UI thread to properly display please wait form.
            Application.DoEvents();
            //redirect console to text box here
            Console.SetOut(threadSafeWriter());
        }

        private void btTrade_Click(object sender, EventArgs e)
        {
            var frm = new TradingForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
           
        }

        private void Test_Click(object sender, EventArgs e)
        {
               // YahooStockMethods yahoo = new YahooStockMethods();
            
            //  Hashtable funda = YahooStockMethods.getFundamentals("gild");
            //  CalculatorMethods cal = new CalculatorMethods();
              //cal.calculateBollingerBands();
            //  int rsi = cal.RSI("aapl");
            //  funda = yahoo.getFundamentals("aapl");
            //  cal.ProcessAllMetrics(funda, 20, rsi);

           // cal.ProcessAllMetrics(funda, 40, 2, "named", 2);
           // cal.displayResults();
            //the please wait form that indicates loading
            //  PleaseWait pleaseWait = new PleaseWait();
            // Display form modelessly
            //   pleaseWait.Show();
            //  ALlow main UI thread to properly display please wait form.
            //   Application.DoEvents();

            //   //redirect console to text box here


            //clear the text from the output box
            //  tbOutput.Text = string.Empty;
            //   PosTaggerTest ptt = new PosTaggerTest();
            //   ptt.testNounNamed();

            //    BagOfWordsTest bwt = new BagOfWordsTest();
            //    bwt.processBagOfWordsTest();
            // pleaseWait.Close();
            // SpellCheckTest sct = new SpellCheckTest();
            // sct.testSpellCheckSpeed();

            // BingMethods bing = new BingMethods();
            //  bing.getBinglinks("http://cn.bing.com/news/search?q=gild+Gilead&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1");

            // YahooMinerTest yt = new YahooMinerTest();
            // yt.testLinkProcessor();

        }

      
        //display the results
        public void AppendTextBoxes(string resultsAsString)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBoxes), new object[] { resultsAsString });
                return;
            }
            string[] results = resultsAsString.Split(',');

            tbSentiment.Text = results[0];
            tbRSI.Text = results[1]; 
            tbPEG.Text = results[2];
            tbBollinger.Text = results[3];
            tbPB.Text = results[4];
            tbDividends.Text = results[5];
            tb50MA.Text = results[6];
            tb200MA.Text = results[7];

            tbVerdict.Text = verdict;
        }
    }
}
