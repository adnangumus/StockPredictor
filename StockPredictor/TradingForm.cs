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
            bool is20 = false;
            string symbol = tbTrade.Text;
            if(String.IsNullOrEmpty(symbol))
            {
                //run the simulated trades for companies used in this project
                tr.simulateTrade("GILD", isShort, is20, 0);
                tr.simulateTrade("HZNP", isShort, is20, 0);
                tr.simulateTrade("BIIB", isShort, is20, 0);
                tr.simulateTrade("CELG", isShort, is20, 0);
                tr.simulateTrade("IBB", isShort, is20, 0);
            }
            else
            {
                //run the simulated trade
                tr.simulateTrade(symbol,isShort, is20, 0);
            }
        }
        //simulate trade using 20 minute mark
        private void bt20Trade_Click(object sender, EventArgs e)
        {
            Trading tr = new Trading();
            bool isShort = cbSell.Checked;
            bool is20 = true;
            string symbol = tbTrade.Text;
            decimal sellPrice= Decimal.Parse(tb20.Text);
            //run the simulated trade
            tr.simulateTrade(symbol, isShort, is20, sellPrice);
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            TradingTest tt = new TradingTest();
            tt.testTrade();
        }
    }
}
