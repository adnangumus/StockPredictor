using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockPredictor.Tests;
using StockPredictor.Helpers;

namespace StockPredictor
{
    public partial class TradingForm : Form
    {
        public TradingForm()
        {
            InitializeComponent();
        }

        //allow for an instance of this form to be called and used by other classes
        static TradingForm instance;
        public static TradingForm Instance
        {
            get
            {
                try
                {
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

            tbTradeOutput.AppendText(str);
        }
        //check if it a retried trade
        public bool isRetry()
        {
            return cbRetry.Checked;
        }

        //get open price
        public decimal getOpenPrice()
        {
            decimal openPrice;
            try { openPrice = Decimal.Parse(tbOpen.Text); }
            catch (Exception ex) { AppendOutputText("\r\n" + "please enter a number into the open price box" + "\r\n"); return 0; }
            return openPrice;
        }
            //get open price
        public decimal getClosePrice()
        {
            decimal closePrice;
            try { closePrice = Decimal.Parse(tbClose.Text); }
            catch (Exception ex) { AppendOutputText("\r\n" + "please enter a number into the close price box" + "\r\n"); return 0; }
            return closePrice;
        }
        //simulate trade 
        private void btTrade_Click(object sender, EventArgs e)
        {
        //process if it is a manual tade 
            bool isManual = cbManual.Checked;
            if (isManual) { manualTrade(); return; }
            //intialize the trading class
            Trading tr = new Trading();
            //get input from user
            string symbol = tbTrade.Text.ToUpper();                     
            bool is20 = cb20.Checked;
            decimal sellPrice = 0;
                //check that there is input before assigning local variable
            if (!String.IsNullOrEmpty(tb20.Text)) { 
                sellPrice = Decimal.Parse(tb20.Text);
            }

            if (String.IsNullOrEmpty(symbol))
            {
                //run the simulated trades for companies used in this project
                tbTradeOutput.AppendText("Running automatic trades " + "\r\n");
                Task taskA = new Task(() => tr.autoTrade("GILD", is20, sellPrice));
                Task taskB = new Task(() => tr.autoTrade("BIIB", is20, sellPrice));
                Task taskC = new Task(() => tr.autoTrade("CELG", is20, sellPrice));
                Task taskD = new Task(() => tr.autoTrade("HZNP", is20, sellPrice));
                Task taskE = new Task(() => tr.autoTrade("IBB", is20, sellPrice));
                //stat the tasks
                taskA.Start();
                taskB.RunSynchronously();
                taskC.RunSynchronously();
                taskD.RunSynchronously();
                taskE.RunSynchronously();
                //  taskC.RunSynchronously();
                taskA.GetAwaiter();
                taskB.Wait();
                taskC.Wait();
                taskD.Wait();
                taskE.Wait();
                //tr.autoTrade("GILD", is20, sellPrice);
                //tr.autoTrade("BIIB", is20, sellPrice);
                //tr.autoTrade("CELG", is20, sellPrice);
                //tr.autoTrade("HZNP", is20, sellPrice);
                //tr.autoTrade("IBB", is20, sellPrice);
                return;
            }
            //run the simulated trade
             tr.autoTrade(symbol, is20, sellPrice);

        }
       
        //this method process manual trade
        private void manualTrade()
        {
            Trading tr = new Trading();
            bool isShort = cbSell.Checked;
            bool is20 = cb20.Checked;
            bool isBag = cbBag.Checked;
            bool isNoun = cbNoun.Checked;
            bool isNamed = cbNamed.Checked;
            bool isRandom = cbRandom.Checked;
            string symbol = tbTrade.Text.ToUpper();

            if (String.IsNullOrEmpty(symbol))
            {
                //run the simulated trades for companies used in this project
                tbTradeOutput.AppendText("Please enter a stock symbol" + "\r\n");
                return;
            }
            if (String.IsNullOrEmpty(tb20.Text))
            {  //run the simulated trade
                tr.simulateTradeMaster(symbol, isShort, is20, 0, isBag, isNoun, isNamed, isRandom);
            }
            else
            {
                decimal sellPrice = Decimal.Parse(tb20.Text);
                //run the simulated trade
                tr.simulateTradeMaster(symbol, isShort, is20, sellPrice, isBag, isNoun, isNamed, isRandom);
            }
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            TradingTest tt = new TradingTest();
            tt.testTrade();
        }

        private void tb20_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
