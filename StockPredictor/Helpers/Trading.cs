using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Helpers
{
    class Trading
    {
        //simulate trade based on method used
        public void simulateTradeMaster(string symbol, bool isShort, bool is20, decimal sellPrice, bool isBag, bool isNoun, bool isNamed, bool isRandom)
        {
            //save the data based on the method used
            if (isBag) { simulateTrade(symbol, isShort, is20, sellPrice, "Bag"); }
            if (isNoun) { simulateTrade(symbol, isShort, is20, sellPrice, "Noun"); };
            if(isNamed){ simulateTrade(symbol, isShort, is20, sellPrice, "Named"); }
            if (isRandom) { simulateTrade(symbol, isShort, is20, sellPrice, "Random"); }
            else { TradingForm.Instance.AppendOutputText("\r\n" + "Please choose a method - Noun, Bag, Named" + "\r\n"); return; }
        }
        //simulate day trading
        private void simulateTrade(string symbol, bool isShort, bool is20, decimal sellPrice, string method)
        {
            ExcelMethods ex = new ExcelMethods();
            //set the fileName to be passed for retreiving data
            string fileName = symbol.ToUpper() + method + "Trade";
            //get the starting priciple
            decimal principle = ex.readPrinciple(fileName, symbol, is20);
            string startPrinciple = principle.ToString();
            //yahoo methods to find change in stock prices
            YahooStockMethods yahoo = new YahooStockMethods();
            //price[0] = open, price[1] = close 
            string[] prices = yahoo.getStockPriceChange(symbol);           
            decimal startPrice = Decimal.Parse(prices[0]);
            decimal closePrice = Decimal.Parse(prices[1]);
            decimal change = calculateChangePercent(startPrice, closePrice);
            bool profitable = false;
            //check if it is a 20 minute trade
            if (is20)
            {
                //ex.saveTradingData(symbol, is20, principle.ToString(), prices[1], prices[2], isShort, prices[0]);
                prices[2] = sellPrice.ToString();
                change = calculateChangePercent(startPrice, sellPrice); ;
            }
            if (isShort)
            {
                
                principle -= principle * (change/100);
            }
            else
            {
                principle += principle * (change / 100);
            }
            //roud the principle
            principle = Math.Round(principle, 2);
            if (principle > Decimal.Parse(startPrinciple)) { profitable = true; }
                //save the trading data to an excel sheet
                ex.saveTradingData(symbol, fileName, is20, principle.ToString(), startPrinciple, prices[0], prices[1], isShort, change.ToString(), profitable);
            //write information to the text box
            TradingForm.Instance.AppendOutputText("\r\n" + symbol + "\r\n" + "Start Principle : " + startPrinciple + "\r\n" +
                "Principle Now : " + principle + "\r\n" + "Percentage change : " + change + "\r\n" +
                "Short Traded : " + isShort 
                );
        }
    
        //calculate the change percentage in a day
        private decimal calculateChangePercent(decimal open, decimal sell)
            {
            decimal change = 0;
            decimal realChange = sell - open;
            change = (100 / open) * realChange;
            //return the change rounded to decimal places
            return Math.Round(change, 2);
            }

        //simulate trading within the first 20 minutes
    }
}
