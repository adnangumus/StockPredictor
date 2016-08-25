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
        
        private void Run_Click(object sender, EventArgs e)
        {
            tbOutput.Text = string.Empty;
            // Set up background worker object & hook up handlers
            BackgroundWorker bgWorker;
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(runScans);
           // bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            // Launch background thread to do the work of reading the file.  
            bgWorker.RunWorkerAsync();
           
        }
// handle the scanning in a back ground worker
        private void runScans(object sender, DoWorkEventArgs e)
        {
            //the please wait form that indicates loading
            PleaseWait pleaseWait = new PleaseWait();
            // Display form modelesslyD:\VisualStudioProjects\StockPredictor\StockPredictor\Helpers\Mining.cs
            pleaseWait.Show();
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
                rm.runStockPredictor("ibb", dontSave);
                pleaseWait.Close();
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
            //close the please wait dialogue
            pleaseWait.Close();
        }
        //display the total time
        private void displayTime(long elapsedMs2)
        {
            AppendOutputText("\r\n");
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
        //this button will retrieve price information for stocks
        private void btPrice_Click(object sender, EventArgs e)
        {
            YahooStockMethods yahoo = new YahooStockMethods();
            //if the the input box is empty or equal to bio run the biotech stock search
            if(String.IsNullOrEmpty(tbPriceInput.Text) || tbPriceInput.Text.ToLower() == "bio")
            {
                yahoo.getBioStockPrices();
            }
            else
            {
                yahoo.getStockPriceInformation(tbPriceInput.Text.ToUpper());
            }
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

            //the please wait form that indicates loading
          //  PleaseWait pleaseWait = new PleaseWait();
            // Display form modelessly
         //   pleaseWait.Show();
            //  ALlow main UI thread to properly display please wait form.
         //   Application.DoEvents();

         //   //redirect console to text box here
            Console.SetOut(threadSafeWriter());

            //clear the text from the output box
            //  tbOutput.Text = string.Empty;
            //   PosTaggerTest ptt = new PosTaggerTest();
            //   ptt.testNounNamed();

            //    BagOfWordsTest bwt = new BagOfWordsTest();
            //    bwt.processBagOfWordsTest();
            // pleaseWait.Close();


            BingMethods bing = new BingMethods();
            bing.getBinglinks("http://cn.bing.com/news/search?q=gild+Gilead&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1");

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
