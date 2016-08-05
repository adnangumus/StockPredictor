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

        public void getBioStockPrices()
        {
            Quotes = new BindingList<Quote>();

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
//parse the infromation for dispalying and storing
                decimal openPrice = Decimal.Parse(quote.Open.ToString());
                decimal closePrice = Decimal.Parse(quote.PreviousClose.ToString());
                decimal percentageChange = Decimal.Parse(quote.ChangeInPercent.ToString());
                //wrtie the infromation to the console
                Console.WriteLine(quote.Name);
                Console.WriteLine("Stock open price : " + openPrice);
                Console.WriteLine("Stock close price : " + closePrice);
                Console.WriteLine("Percentage change : " + percentageChange);
                Console.WriteLine("Percentage change : " + quote.Symbol);
                //write the information to the text box
                Form1.Instance.AppendOutputText("\r\n");
                Form1.Instance.AppendOutputText(quote.Name + "\r\n");
                Form1.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                Form1.Instance.AppendOutputText("Stock close price : " + closePrice + "\r\n");
                Form1.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                Form1.Instance.AppendOutputText("Percentage change : " + quote.Symbol + "\r\n");
               
                //store the price information in an excel file
                ExcelMethods ex = new ExcelMethods();
                ex.savePriceData(quote.Name, quote.Symbol,openPrice,closePrice,percentageChange);

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
                decimal openPrice = Decimal.Parse(quote.Open.ToString());
                decimal closePrice = Decimal.Parse(quote.PreviousClose.ToString());
                decimal percentageChange = Decimal.Parse(quote.ChangeInPercent.ToString());
                Console.WriteLine(quote.Name);
                Console.WriteLine("Stock open price : " + openPrice);
                Console.WriteLine("Stock close price : " + closePrice);
                Console.WriteLine("Percentage change : " + percentageChange);
                Console.WriteLine("Percentage change : " + quote.Symbol);
            
                //write the information to the text box
                Form1.Instance.AppendOutputText("\r\n");
                Form1.Instance.AppendOutputText(quote.Name + "\r\n");
                Form1.Instance.AppendOutputText("Stock open price : " + openPrice + "\r\n");
                Form1.Instance.AppendOutputText("Stock close price : " + closePrice + "\r\n");
                Form1.Instance.AppendOutputText("Percentage change : " + percentageChange + "\r\n");
                Form1.Instance.AppendOutputText("Percentage change : " + quote.Symbol + "\r\n");
               
                //store the price information in an excel file
                ExcelMethods ex = new ExcelMethods();
                ex.savePriceData(quote.Name, quote.Symbol, openPrice, closePrice, percentageChange);
            }

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
