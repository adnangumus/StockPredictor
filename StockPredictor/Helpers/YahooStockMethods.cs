using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Models;
using System.ComponentModel;

namespace StockPredictor.Helpers
{
    class YahooStockMethods
    {
        public BindingList<Quote> Quotes { get; set; }
        private int Retries { get; set; }

        public void getBioStockPrices()
        {
            Quotes = new BindingList<Quote>();
            Retries = 0;
            //Some example tickers
            Quotes.Add(new Quote("GILD"));
            Quotes.Add(new Quote("HZNP"));
            Quotes.Add(new Quote("BIIB"));
            Quotes.Add(new Quote("CELG"));
            Quotes.Add(new Quote("IBB"));


            //get the data
            YahooStockEngine.Fetch(Quotes);

            foreach (Quote quote in Quotes)
            {
                //check the date corresponds to yesterdays date. This changes because of the time difference and returns inconsistent values
                DateTime? lastTradeDate = quote.LastTradeDate;
                DateTime dateYesterday = DateTime.Today.AddDays(-1);
                DateTime yahooDate = lastTradeDate ?? DateTime.Today.AddDays(-1);
                if (dateYesterday.Date == yahooDate.Date)
                {

                    //parse the infromation for dispalying and storing
                    decimal openPrice = Decimal.Parse(quote.Open.ToString());
                    decimal closePrice = Decimal.Parse(quote.PreviousClose.ToString());
                    decimal percentageChange = Decimal.Parse(quote.ChangeInPercent.ToString());
                    decimal lastTradePriceOnly = Decimal.Parse(quote.LastTradePrice.ToString());
                    //wrtie the infromation to the console
                    Console.WriteLine(quote.Name);
                    Console.WriteLine("Stock open price : " + openPrice);
                    Console.WriteLine("Stock close price : " + closePrice);
                    Console.WriteLine("Last trade price : " + lastTradePriceOnly);
                    Console.WriteLine("Percentage change : " + percentageChange);
                    Console.WriteLine("Percentage change : " + quote.Symbol);
                    //write the information to the text box
                    Form1.Instance.AppendOutputText("\r\n");
                    Form1.Instance.AppendOutputText(quote.Name + "\r\n");
                    Form1.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                    Form1.Instance.AppendOutputText("Previous stock close price : " + closePrice + "\r\n");
                    Form1.Instance.AppendOutputText("Last trade price : " + lastTradePriceOnly + "\r\n");
                    Form1.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                    Form1.Instance.AppendOutputText("Percentage change : " + quote.Symbol + "\r\n");

                    //store the price information in an excel file
                    ExcelMethods ex = new ExcelMethods();
                    ex.savePriceData(quote.Name, quote.Symbol, openPrice, closePrice, percentageChange, lastTradePriceOnly);
                }
                else
                {
                    Retries++;
                    if (Retries < 4)
                    {
                        getBioStockPrices();
                    } //write the information to the text box
                    Form1.Instance.AppendOutputText("\r\n" + "Failed to load correct date : " + quote.Symbol);
                    return;
                }
                }

            Console.WriteLine("Stock prices retrieved");
        }
      

        //get the infromation from the stock symbol in the text box
        public void getStockPriceInformation(string symbol)
        {
            Quotes = new BindingList<Quote>();
            //Some example tickers
            Quotes.Add(new Quote(symbol.ToUpper()));
            //get the data
            YahooStockEngine.Fetch(Quotes);

            foreach (Quote quote in Quotes)
            {
                //check the date corresponds to yesterdays date. This changes because of the time difference and returns inconsistent values
                DateTime? lastTradeDate = quote.LastTradeDate;
                DateTime dateYesterday = DateTime.Today.AddDays(-1);
                DateTime yahooDate = lastTradeDate ?? DateTime.Today.AddDays(-1);
                if (dateYesterday.Date == yahooDate.Date || yahooDate.Date == DateTime.Today.Date) { 

                decimal openPrice = Decimal.Parse(quote.Open.ToString());
                decimal closePrice = Decimal.Parse(quote.PreviousClose.ToString());
                decimal percentageChange = Decimal.Parse(quote.ChangeInPercent.ToString());
                decimal lastTradePriceOnly = Decimal.Parse(quote.LastTradePrice.ToString());

                Console.WriteLine(quote.Name + " " + lastTradeDate + " yesterday" + dateYesterday);
                Console.WriteLine("Stock open price : " + openPrice);
                Console.WriteLine("Percentage change : " + percentageChange);
                Console.WriteLine("Last trade price : " + lastTradePriceOnly);
                Console.WriteLine("Previous stock close price : " + closePrice);
                Console.WriteLine("Symbol : " + quote.Symbol);
             
                //write the information to the text box
                Form1.Instance.AppendOutputText("\r\n");
                Form1.Instance.AppendOutputText(quote.Name + "\r\n" + lastTradeDate + " yesterday " + dateYesterday + "\r\n");
                Form1.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                Form1.Instance.AppendOutputText("Last trade price : " + lastTradePriceOnly + "\r\n");
                Form1.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                Form1.Instance.AppendOutputText("Previous stock close price : " + closePrice + "\r\n");
                Form1.Instance.AppendOutputText("Symbol : " + quote.Symbol + "\r\n");


                if (Form1.Instance.dontSave()) { return; }
                //store the price information in an excel file
                ExcelMethods ex = new ExcelMethods();
                ex.savePriceData(quote.Name, quote.Symbol, openPrice, closePrice, percentageChange, lastTradePriceOnly);
                }
                else {
                    Retries++;
                    if (Retries < 4) { getStockPriceInformation(symbol);
                    } //write the information to the text box
                    Form1.Instance.AppendOutputText("\r\n" + "Failed to load correct date : " + symbol);
                    return;
                }
            }

        }
        //get the price change for simulated trading
        public string[] getStockPriceChange(string symbol)
        {
            string[] prices = new string[2];
            string open = "";
            string lastTrade = "";
            Quotes = new BindingList<Quote>();
            //Some example tickers
            Quotes.Add(new Quote(symbol.ToUpper()));
            //get the data
            YahooStockEngine.Fetch(Quotes);


            foreach (Quote quote in Quotes)
            {
                //check the date corresponds to yesterdays date. This changes because of the time difference and returns inconsistent values
                DateTime? lastTradeDate = quote.LastTradeDate;
                DateTime dateYesterday = DateTime.Today.AddDays(-1);
                DateTime yahooDate = lastTradeDate ?? DateTime.Today.AddDays(-1);

                if (dateYesterday.Date == yahooDate.Date || yahooDate.Date == DateTime.Today.Date)
                {
                    //check the date of the last trade
                    string tradeDate = quote.LastTradeDate.ToString();
                    Console.WriteLine(symbol + " last trade date :" + tradeDate);
                    open = quote.Open.ToString();
                    Console.WriteLine(symbol + " open price : " + open);
                    lastTrade = quote.LastTradePrice.ToString();
                    prices[0] = open;
                    prices[1] = lastTrade;

                    return prices;
                }
                else
                {
                    Retries++;
                    if (Retries < 4)
                    {
                        getStockPriceChange(symbol);
                    } //write the information to the text box
                    TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load correct date : " + symbol);
                    return prices;
                }
            }
            return prices;
        }          
        

        public string getStockName(string symbol)
        {
            Quotes = new BindingList<Quote>();
            //Some example tickers
            Quotes.Add(new Quote(symbol.ToUpper()));
            //get the data
            YahooStockEngine.Fetch(Quotes);

            foreach (Quote quote in Quotes)
            {
                return quote.Name;               
            }
            return "";
        }
    }
}
