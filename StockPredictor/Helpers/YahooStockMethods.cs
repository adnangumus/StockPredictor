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

                decimal openPrice = Decimal.Parse(quote.Open.ToString());
                decimal closePrice = Decimal.Parse(quote.PreviousClose.ToString());
                decimal percentageChange = Decimal.Parse(quote.ChangeInPercent.ToString());
                Console.WriteLine(quote.Name);
                Console.WriteLine("Stock open price : " + openPrice);
                Console.WriteLine("Stock close price : " + closePrice);
                Console.WriteLine("Percentage change : " + percentageChange);
                Console.WriteLine("Percentage change : " + quote.Symbol);
                //store the price information in an excel file
                ExcelMethods ex = new ExcelMethods();
                ex.savePriceData(quote.Name, quote.Symbol,openPrice,closePrice,percentageChange);

            }

            Console.WriteLine("Stock prices retrieved");
        }

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
            }

        }
    }
}
