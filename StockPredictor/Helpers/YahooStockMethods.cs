using System;
using System.Collections.Generic;

using StockPredictor.Models;
using System.ComponentModel;
using System.Net;

using System.Text.RegularExpressions;
using System.Collections;

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
                    TradingForm.Instance.AppendOutputText("\r\n");
                    TradingForm.Instance.AppendOutputText(quote.Name + "\r\n");
                    TradingForm.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                    TradingForm.Instance.AppendOutputText("Previous stock close price : " + closePrice + "\r\n");
                    TradingForm.Instance.AppendOutputText("Last trade price : " + lastTradePriceOnly + "\r\n");
                    TradingForm.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                    TradingForm.Instance.AppendOutputText("Percentage change : " + quote.Symbol + "\r\n");                   
                }
                else
                {
                    Retries++;
                    if (Retries < 4)
                    {
                        getBioStockPrices();
                    } //write the information to the text box
                    TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load correct date : " + quote.Symbol);
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
                TradingForm.Instance.AppendOutputText("\r\n");
                    TradingForm.Instance.AppendOutputText(quote.Name + "\r\n" + lastTradeDate + " yesterday " + dateYesterday + "\r\n");
                    TradingForm.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                    TradingForm.Instance.AppendOutputText("Last trade price : " + lastTradePriceOnly + "\r\n");
                    TradingForm.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                    TradingForm.Instance.AppendOutputText("Previous stock close price : " + closePrice + "\r\n");
                    TradingForm.Instance.AppendOutputText("Symbol : " + quote.Symbol + "\r\n");

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
            List<HistoricalStock> quotes = DownloadHistoricalData1Day(symbol);


            foreach (HistoricalStock quote in quotes)
            {
                //check the date corresponds to yesterdays date. This changes because of the time difference and returns inconsistent values
                DateTime? lastTradeDate = quote.Date;
                DateTime dateYesterday = DateTime.Today.AddDays(-1);
                DateTime yahooDate = lastTradeDate ?? DateTime.Today.AddDays(-1);

                if (dateYesterday.Date == yahooDate.Date || yahooDate.Date == DateTime.Today.Date)
                {
                    //check the date of the last trade
                    string tradeDate = quote.Date.ToString();
                    Console.WriteLine(symbol + " last trade date :" + tradeDate);
                    open = quote.Open.ToString();
                    Console.WriteLine(symbol + " open price : " + open);
                    lastTrade = quote.Close.ToString();
                    prices[0] = open;
                    prices[1] = lastTrade;

                    return prices;
                }
                else
                {
                    Retries++;
                    if (Retries < 4)
                    {
                        Console.WriteLine("Retrying Quotes");
                        prices = getStockPriceChange(symbol);
                        return prices;    
                    } //write the information to the text box
                    TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load correct date : " + symbol);
                    prices[0] = "";
                    return prices;
                }
            }
            return prices;
        }          
        
      
        //get the historical data from yahoo
        public static List<HistoricalStock> DownloadHistoricalData1Day(string ticker)
        {
            List<HistoricalStock> retval = new List<HistoricalStock>();
           
            using (WebClient web = new WebClient())
            {
                DateTime startDate = DateTime.Today.AddDays(-1);
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
        
        //gett the historical data from yahoo
        public static List<HistoricalStock> GetHistoricalPriceData(string ticker)
        {
            List<HistoricalStock> retval = new List<HistoricalStock>();

            using (WebClient web = new WebClient())
            {
                DateTime startDate = DateTime.Today.AddDays(-40);
                DateTime endDate = DateTime.Today;

                //"http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=d&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&ignore=.csv"
                //http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=w&ignore=.csv

                // string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&c={1}", ticker, yearToStartFrom));
                try
                {
                    string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s=" + ticker + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year + "&g=d&a=" + (startDate.Month - 1) + "&b=" + startDate.Day + "&c=" + startDate.Year + "&ignore=.csv"));
                    data = data.Replace("r", "");

                    string[] rows = Regex.Split(data, @"\n");
                    //string[] rows = data.Split('n'); string[] rows = Regex.Split(data, @"\n");

                    //First row is headers so Ignore it
                    for (int i = 1; i < rows.Length; i++)
                    {
                        //if it is the repeater method feed the live prices in at the beginning
                        if ((Form1.Instance.isRepeat() || Form1.Instance.repeatGlobal.RepeaterIsRunning) && i == 1)
                        {
                            try {
                            HistoricalStock hs = new HistoricalStock();
                            hs.Date = DateTime.Now;
                            hs.Open = Form1.Instance.repeatGlobal.OpenPrice;
                            hs.High = 0;
                            hs.Low = 0;
                            hs.Close = Form1.Instance.repeatGlobal.CurrentPrice;
                            hs.Volume = 0;
                            hs.AdjClose = 0;
                            retval.Add(hs);
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message + " Error in setting the current price in the historical data list"); }
                        }
                        else
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
                }
                catch (Exception)
                { return null; }
                
                return retval;
            }
        }

        //get the yahoo stock fundamentals 

        /*Symbol = s
        r = P / E
        r7 Price/ EPS Estimate Next Year
       r6  Price / EPS Estimate Current Year
         p6  Price / Book     
      m3	50-day Moving Average	
      m4	200-day Moving Average
m5	Change From 200-day Moving Average	
m6	Percent Change From 200-day Moving Average	
m7	Change From 50-day Moving Average
m8	Percent Change From 50-day Moving Average
    t8  1 yr Target Price
    t7  Ticker Trend
    d1 Last Trade Date
    d2 Trade Date
    w1  Day's Value Change
    r5: PEG Ratio
    */
    private static int retryFundamentals = 0;
        public static Hashtable getFundamentals(string ticker)
        {
            retryFundamentals++;
            Hashtable hs = new Hashtable();
            using (WebClient web = new WebClient())
            {
                           
                try
                {
                    string data = web.DownloadString(string.Format("http://finance.yahoo.com/d/quotes.csv?s="+ticker+ 
                        "&f=sm3m4m6m8m5m7p6r5dt7n"));
                    data = data.Replace("r", "");

                    string[] cols = data.Split(',');
                    hs.Add("ticker", cols[0].ToString());                
                    hs.Add("50Average", cols[1].ToString());
                    hs.Add("200Average", cols[2].ToString());
                    hs.Add("200ChangePercent", cols[3].ToString());
                    hs.Add("50ChangePercent", cols[4].ToString());
                    hs.Add("200Change", cols[5].ToString());
                    hs.Add("50Change", cols[6].ToString());
                    hs.Add("PB", cols[7].ToString());
                    hs.Add("PEG", cols[8].ToString());
                    hs.Add("Dividend", cols[9].ToString());
                    hs.Add("Trend", cols[10].ToString());
                    hs.Add("Name", cols[11].ToString());
                    //store the variables in the form1 instance for saving later
                    Form1.Instance.scanMetrics.PriceBook = cols[7].ToString();
                    Form1.Instance.scanMetrics.Peg = cols[8].ToString();
                    Form1.Instance.scanMetrics.Dividend = cols[9].ToString();
                    Form1.Instance.scanMetrics.Moving50 = cols[4].ToString();
                    Form1.Instance.scanMetrics.Moving200 = cols[3].ToString();                     

  
                //this is will change the values if it is part of the repeat
                if(Form1.Instance.isRepeat() || Form1.Instance.repeatGlobal.RepeaterIsRunning)
                {
                        try {
                  double currentPrice =  Form1.Instance.repeatGlobal.CurrentPrice;
                  double MA50 = Convert.ToDouble(cols[1].ToString());
                  double MA200 = Convert.ToDouble(cols[2].ToString());
                  hs["200ChangePercent"] = calculateFromMA(currentPrice,MA200).ToString();
                  hs["50ChangePercent"] = calculateFromMA(currentPrice, MA50).ToString();
                        }
                        catch (Exception ex) { Console.WriteLine("Failed to CHANGE fundamental data " + ex.Message); }
                    }
              
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load fundamental data " + ex.Message);
                    if(retryFundamentals <3)
                    {
                        Console.WriteLine("Retry to load fundamentals " + retryFundamentals); 
                    hs = getFundamentals(ticker);
                    }
                    return null;
                }
                Form1.Instance.scanMetrics.Fundamentals = hs;
                return hs;
            }


        }

        //calculate the percentage from moving average
        private static double calculateFromMA(double current, double MA)
        {
            double percentage = 0;
            double realDif = current - MA;
            percentage = (current / 100) * realDif; 
            return percentage;
        }

    }
    }

