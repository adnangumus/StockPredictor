using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;
using Microsoft.Office.Interop.Excel;

namespace StockPredictor.Helpers
{
    class Trading
    {
        //simulate trade based on method used
        public void simulateTradeMaster(string symbol, bool isShort, bool is20, decimal sellPrice, bool isBag, bool isNoun, bool isNamed, bool isRandom)
        {
            //yahoo methods to find change in stock prices
            YahooStockMethods yahoo = new YahooStockMethods();
            string[] prices = yahoo.getStockPriceChange(symbol);
            //ensure that the data loads correctly
            if (string.IsNullOrEmpty(prices[0]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load price data " + symbol);
                //check if the user added the trading prices manually
                if (TradingForm.Instance.getOpenPrice() == 0 || TradingForm.Instance.getClosePrice() == 0)
                {
                    TradingForm.Instance.AppendOutputText("\r\n" + "Consider entering open and close prices manually" + "\r\n");

                    return;
                }
            }
            //get the price data from the text boxes 
            prices[0] = TradingForm.Instance.getOpenPrice().ToString();
            prices[1] = TradingForm.Instance.getClosePrice().ToString();
//start the excel application object
            ExcelMethods exl = new ExcelMethods();
            Application myPassedExcelApplication = exl.startExcelApp();
            //save the data based on the method used
            if (isBag) { simulateTrade(symbol, isShort, is20, sellPrice, "Bag", prices, myPassedExcelApplication); }
            if (isNoun) { simulateTrade(symbol, isShort, is20, sellPrice, "Noun", prices, myPassedExcelApplication); }
            if (isNamed) { simulateTrade(symbol, isShort, is20, sellPrice, "Named", prices, myPassedExcelApplication); }
            if (isRandom) { simulateTrade(symbol, isShort, is20, sellPrice, "Random", prices, myPassedExcelApplication); }
            else { TradingForm.Instance.AppendOutputText("\r\n" + "Please choose a method - Noun, Bag, Named" + "\r\n"); return; }
            //destroy the excel application
            exl.quitExcel(myPassedExcelApplication);
        }

        //simulate day trading
        private void simulateTrade(string symbol, bool isShort, bool is20, decimal sellPrice, string method, string[] prices, Application myPassedExcelApplication)
        {
            ExcelMethods ex = new ExcelMethods();
            //set the fileName to be passed for retreiving data
            string fileName = symbol.ToUpper() + method + "Trade";
            //get the starting priciple
            decimal principle = ex.readPrinciple(myPassedExcelApplication, fileName, symbol, is20);
            string startPrinciple = principle.ToString();
            decimal startPrice = 0;
            decimal closePrice = 0;
            decimal change = 0;
           
           
            //check that data loaded correctly
            if(string.IsNullOrEmpty(prices[0]) || string.IsNullOrEmpty(prices[1]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo fincial data" + "\r\n");
                return;
            }
            try { 
             startPrice = Decimal.Parse(prices[0]);
             closePrice = Decimal.Parse(prices[1]);
             change = calculateChangePercent(startPrice, closePrice);
            }
            catch (Exception ex2) { TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo fincial data" + "\r\n"); return; }
            bool profitable = false;
            //check if it is a 20 minute trade
            if (is20)
            {
                //ex.saveTradingData(symbol, is20, principle.ToString(), prices[1], prices[2], isShort, prices[0]);
                prices[1] = sellPrice.ToString();
                change = calculateChangePercent(startPrice, sellPrice); ;
            }
            if (isShort)
            {

                principle -= principle * (change / 100);
            }
            else
            {
                principle += principle * (change / 100);
            }
            //roud the principle
            principle = Math.Round(principle, 2);
            if (principle > Decimal.Parse(startPrinciple)) { profitable = true; }
            //save the trading data to an excel sheet
            ex.saveTradingData(myPassedExcelApplication, symbol, fileName, is20, principle.ToString(), startPrinciple, prices[0], prices[1], isShort, change.ToString(), profitable);
            //write information to the text box
            TradingForm.Instance.AppendOutputText("\r\n" + symbol + "\r\n" + "Start Price : " + prices[0] + "\r\n" +
                "End Price :" + prices[1] + "\r\n" + "Start Principle : " + startPrinciple + "\r\n" +
                "Principle Now : " + principle + "\r\n" + "Percentage change : " + change + "\r\n" +
                "Short Traded : " + isShort + "\r\n"
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

        // auto trade
        public void autoTrade(string symbol, bool is20, decimal sellPrice)
        {
           
          
            //yahoo methods to find change in stock prices
            YahooStockMethods yahoo = new YahooStockMethods();
            string[] prices = yahoo.getStockPriceChange(symbol);
            //ensure that the data loads correctly
            if (string.IsNullOrEmpty(prices[0]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load price data " + symbol);
                //check if the user added the trading prices manually
                if (TradingForm.Instance.getOpenPrice() == 0 || TradingForm.Instance.getClosePrice() == 0)
                {
                    TradingForm.Instance.AppendOutputText("\r\n" + "Consider entering open and close prices manually" + "\r\n");
                   
                    return;
                }
                prices[0] = TradingForm.Instance.getOpenPrice().ToString();
                Console.WriteLine(symbol); ;
                Console.WriteLine("Open : " + prices[0]);
                Console.WriteLine("Close : " + prices[1]);            
                prices[1] = TradingForm.Instance.getClosePrice().ToString();
            }
            //start the excel application object
            ExcelMethods exl = new ExcelMethods();
            Application myPassedExcelApplication = exl.startExcelApp();

            tradeNow(symbol, "Bag", sellPrice, prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Noun", sellPrice, prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Named", sellPrice, prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Random", sellPrice, prices, is20, myPassedExcelApplication);

            //destroy the excel application
            exl.quitExcel(myPassedExcelApplication);
        }
        //method used to allow multithreading in the auto trade section
        private void tradeNow(string symbol, string method, decimal sellPrice, string[] prices, bool is20, Application myPassedExcelApplication)
        {
            bool isShort = false;
            ExcelMethods ex = new ExcelMethods();
            int score = ex.readLatestSentimentScore(myPassedExcelApplication, symbol, method);
            //if no data is stored then stop the auto trade
            if (score == 0) { TradingForm.Instance.AppendOutputText("\r\n" + "Trade canceled. Sentiment information is zero!" + "\r\n" + method + "\r\n"); return; }          
            if (score < 0) { isShort = true; }
            if (score > 0) { isShort = false; };
            simulateTrade(symbol, isShort, is20, sellPrice, method, prices, myPassedExcelApplication);
           
        }
    }

}


