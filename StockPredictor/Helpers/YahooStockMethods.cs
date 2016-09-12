using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Models;
using System.ComponentModel;
using System.Net;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
                    prices[0] = "";
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
      
        //gett the historical data from yahoo
        public static List<HistoricalStock> DownloadData(string ticker)
        {
            List<HistoricalStock> retval = new List<HistoricalStock>();
           
            using (WebClient web = new WebClient())
            {
                DateTime startDate = DateTime.Today.AddDays(-7);
                DateTime endDate = DateTime.Today;

                //"http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=d&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&ignore=.csv"
                //http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=w&ignore=.csv

                // string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", ticker, yearToStartFrom));
                try { 
                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=d&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&ignore=.csv"));
                data = data.Replace("r", "");

                string[] rows = Regex.Split(data, @"\n");
                //string[] rows = data.Split('n'); string[] rows = Regex.Split(data, @"\n");

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    HistoricalStock hs = new HistoricalStock();
                    string date1 = cols[0].ToString();
                    string open1 = cols[1].ToString();
                    hs.Date = DateTime.Parse(cols[0]);
                    hs.Open = Convert.ToDouble(cols[1]);
                    hs.High = Convert.ToDouble(cols[2]);
                    hs.Low = Convert.ToDouble(cols[3]);
                    hs.Close = Convert.ToDouble(cols[4]);
                    hs.Volume = Convert.ToDouble(cols[5]);
                    hs.AdjClose = Convert.ToDouble(cols[6]);

                    retval.Add(hs);
                }
                }
                catch (Exception)
                { return null; }

                return retval;
            }
        }

            public double getStockPriceTrendWeek(string ticker)
        {
            List<HistoricalStock> data = YahooStockMethods.DownloadData(ticker);
            if (data == null)
            {
                Console.WriteLine("data from yahoo return null. No interent?");
                return 0; };
            int last = data.Count;
            int i = 1;
            double open = 0;
            double lastClose = 0;
            double change = 0;
            double trend = 0;
            try
            {
                foreach (HistoricalStock stock in data)
                {
                    if (i == 1)
                    {
                        lastClose = stock.Close;
                       
                    }
                    if (i == last) { open = stock.Open; }
                    i++;
                       Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));
                }
              
                change = lastClose - open;
                trend = (100 / open) * change;
                Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nChange = " + change +
                    "\r\n Open 5 days ago" + open + 
                    "\r\nLast close and open" + lastClose + "\r\nTrend " + trend);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine(trend.ToString());
            return trend;
        }
        }
    }

