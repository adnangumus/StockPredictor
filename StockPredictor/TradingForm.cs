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

        //simulate trade 
        private void btTrade_Click(object sender, EventArgs e)
        {
            Trading tr = new Trading();
            bool isShort = cbSell.Checked;
            bool is20 = cb20.Checked;
            bool isBag = cbBag.Checked;
            bool isNoun = cbNoun.Checked;
            bool isNamed = cbNamed.Checked;
            string symbol = tbTrade.Text.ToUpper();
            
            if(String.IsNullOrEmpty(symbol))
            {
                //run the simulated trades for companies used in this project
                tbTradeOutput.AppendText("Please enter a stock symbol" + "\r\n");
                return;
            }
            if (String.IsNullOrEmpty(tb20.Text))
            {  //run the simulated trade
                tr.simulateTradeMaster(symbol, isShort, is20, 0, isBag, isNoun, isNamed);
            }
            else
            {
                decimal sellPrice = Decimal.Parse(tb20.Text);
                //run the simulated trade
                tr.simulateTradeMaster(symbol,isShort, is20, sellPrice, isBag, isNoun, isNamed);
            }
        }
       

        private void btTest_Click(object sender, EventArgs e)
        {
            TradingTest tt = new TradingTest();
            tt.testTrade();
        }
    }
}
