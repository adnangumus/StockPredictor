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
        public void simulateTradeMaster(string symbol, bool isShort, bool is20, decimal sellPrice, bool isBag, bool isNoun, bool isNamed, bool isRandom, bool isStrong)
        {
            decimal openPrice = TradingForm.Instance.getOpenPrice();
            decimal closePrice = TradingForm.Instance.getClosePrice();

            string[] prices = new string[2]; ;
            if (openPrice > 0 && closePrice > 0)
            {
                prices[0] = openPrice.ToString();
                prices[1] = closePrice.ToString();
            }
            else
            {
                //yahoo methods to find change in stock prices
                YahooStockMethods yahoo = new YahooStockMethods();
                prices = yahoo.getStockPriceChange(symbol);
                //ensure that the data loads correctly
                if (string.IsNullOrEmpty(prices[0]))
                {
                    TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load price data " + symbol);
                    //check if the user added the trading prices manually
                    if (TradingForm.Instance.getOpenPrice() == 0 || TradingForm.Instance.getClosePrice() == 0)
                    {
                        TradingForm.Instance.AppendOutputText("\r\n" + "Consider entering the open and close prices manually" + "\r\n");

                        return;
                    }
                    prices[0] = openPrice.ToString();
                    prices[1] = closePrice.ToString();
                }
            }
            Console.WriteLine(symbol); ;
            Console.WriteLine("Open : " + prices[0]);
            Console.WriteLine("Close : " + prices[1]);

            //check if it a 20 minute trade and set the sell price
            if (is20)
            {
                if (closePrice > 0)
                {
                    prices[1] = closePrice.ToString();
                }
                else { TradingForm.Instance.AppendOutputText("\r\n" + "Please enter the 20 minute price" + "\r\n"); return; }                             
            }


            //start the excel application object
            ExcelMethods exl = new ExcelMethods();
            Application myPassedExcelApplication = exl.startExcelApp();
            //save the data based on the method used
            if (isBag) { simulateTrade(symbol, isShort, is20, "Bag", prices, myPassedExcelApplication, isStrong); }
            if (isNoun) { simulateTrade(symbol, isShort, is20, "Noun", prices, myPassedExcelApplication, isStrong); }
            if (isNamed) { simulateTrade(symbol, isShort, is20,"Named", prices, myPassedExcelApplication, isStrong); }
            if (isRandom) { simulateTrade(symbol, isShort, is20, "Random", prices, myPassedExcelApplication, isStrong); }
            if(!isBag && !isNoun && !isNamed && !isRandom) { TradingForm.Instance.AppendOutputText("\r\n" + "Please choose a method - Noun, Bag, Named" + "\r\n");
                //destroy the excel application
                exl.quitExcel(myPassedExcelApplication); return; }
            //destroy the excel application
            exl.quitExcel(myPassedExcelApplication);
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

        // auto trade used for most of the classes
        //this method checks that the prices loaded correctly
        public void autoTrade(string symbol, bool is20)
        {
           decimal openPrice = TradingForm.Instance.getOpenPrice();
            decimal closePrice = TradingForm.Instance.getClosePrice();

            string[] prices = new string[2]; ;
            if (openPrice > 0 && closePrice >0)
            {
                prices[0] = openPrice.ToString();
                prices[1] = closePrice.ToString();
            }
            else { 
            //yahoo methods to find change in stock prices
            YahooStockMethods yahoo = new YahooStockMethods();
            prices = yahoo.getStockPriceChange(symbol);
            //ensure that the data loads correctly
            if (string.IsNullOrEmpty(prices[0]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load price data " + symbol);
                //check if the user added the trading prices manually
               if (TradingForm.Instance.getOpenPrice() == 0 || TradingForm.Instance.getClosePrice() == 0)
                {
                    TradingForm.Instance.AppendOutputText("\r\n" + "Consider entering the open and close prices manually" + "\r\n");
                   
                    return;
                }
                    prices[0] = openPrice.ToString();
                    prices[1] = closePrice.ToString();
                }   
            }
            Console.WriteLine(symbol); ;
            Console.WriteLine("Open : " + prices[0]);
            Console.WriteLine("Close : " + prices[1]);

            //check if it a 20 minute trade and set the sell price
            if (is20)
            {
                if (closePrice > 0)
                {
                    prices[1] = closePrice.ToString();
                }
                else
                {
                    TradingForm.Instance.AppendOutputText("\r\n" + "Please enter the 20 minute price" + "\r\n"); return;
                }
            }
                //start the excel application object
                ExcelMethods exl = new ExcelMethods();
            Application myPassedExcelApplication = exl.startExcelApp();

            tradeNow(symbol, "Bag", prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Noun", prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Named", prices, is20, myPassedExcelApplication);
            tradeNow(symbol, "Random", prices, is20, myPassedExcelApplication);

            //destroy the excel application
            exl.quitExcel(myPassedExcelApplication);
        }
        //method used to allow multithreading in the auto trade section
        private void tradeNow(string symbol, string method, string[] prices, bool is20, Application myPassedExcelApplication)
        {
            bool isShort = false;
            ExcelMethods ex = new ExcelMethods();
            int score = ex.ReadLatestFinalScore(myPassedExcelApplication, symbol, method);
            bool isStrong = false;
            //if neutral data is stored then stop the auto trade
            if (score <= 4 && score >= -4 && !TradingForm.Instance.sellLong()) { TradingForm.Instance.AppendOutputText("\r\n" + "Neutral : No trading!" + "\r\n" + method + "\r\n"); return; }  
            //negative scores sell and strong sell        
            if (score < -4 && score > -15 )
            {
                isShort = true; isStrong = false;
                TradingForm.Instance.AppendOutputText("\r\n" + "Sell " + "\r\n" + method + "\r\n");
            }
            if (score <= -15)
            {
                isShort = true; isStrong = true;
                TradingForm.Instance.AppendOutputText("\r\n" + "Strong sell :" + "\r\n" + method + "\r\n");
            }
            //positive scores buy and strong buy
            if (score > 4 && score <15)
            {
                isShort = false; isStrong = false;
                TradingForm.Instance.AppendOutputText("\r\n" + "Buy : " + "\r\n" + method + "\r\n");
            };
            if(score >= 15)
            {
                isShort = false; isStrong = true;
                TradingForm.Instance.AppendOutputText("\r\n" + "Strong buy :" + "\r\n" + method + "\r\n");
            }
            //this checks if it is only a long trade
            if (!TradingForm.Instance.isLongTrade() && !TradingForm.Instance.sellLong()) { 
            simulateTrade(symbol, isShort, is20, method, prices, myPassedExcelApplication, isStrong);
            }
            //don't run the long trades with the 20 minute trades to prevent them from being run twice
            if(!is20)
            { 
            //run the long trades
            simulateTradeLongStrategy(symbol, isShort, method, prices, myPassedExcelApplication);
            }

        }

        //simulate day trading
        private void simulateTrade(string symbol, bool isShort, bool is20, string method, string[] prices, Application myPassedExcelApplication, bool isStrong)
        {
            ExcelMethods ex = new ExcelMethods();
            //set the fileName to be passed for retreiving data
            string fileName = symbol.ToUpper() + method + "Trade";
            //get the starting priciple
            decimal principle = ex.ReadPrinciple(myPassedExcelApplication, fileName, symbol, is20, false);
            string startPrinciple = principle.ToString();
            decimal startPrice = 0;
            decimal closePrice = 0;
            decimal change = 0;
            //only use half of the principle on risky trades
            decimal principleHalf = principle / 2;

            //check that data loaded correctly
            if (string.IsNullOrEmpty(prices[0]) || string.IsNullOrEmpty(prices[1]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo financial data" + "\r\n");
                return;
            }
            try
            {
                startPrice = Decimal.Parse(prices[0]);
                closePrice = Decimal.Parse(prices[1]);
                change = calculateChangePercent(startPrice, closePrice);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo financial data" + "\r\n"); return;
            }
            bool profitable = false;
          
            if (isShort)
            {
                if (!isStrong) { principle = principleHalf; }
                principle -= principle * (change / 100);
                if (!isStrong) { principle += principleHalf; }
            }
            else
            {
                if (!isStrong) { principle = principleHalf; }
                principle += principle * (change / 100);
                if (!isStrong) { principle += principleHalf; }
            }
            //roud the principle
            principle = Math.Round(principle, 2);
            if (principle > Decimal.Parse(startPrinciple)) { profitable = true; }
            //save the trading data to an excel sheet
            ex.saveTradingData(myPassedExcelApplication, symbol, fileName, is20, principle.ToString(), startPrinciple, prices[0], prices[1], isShort, change.ToString(), profitable, isStrong);
            //write information to the text box
            TradingForm.Instance.AppendOutputText("\r\n" + symbol + "  " + method + "\r\n" + "Start Price : " + prices[0] + "\r\n" +
                "End Price :" + prices[1] + "\r\n" + "Start Principle : " + startPrinciple + "\r\n" +
                "Principle Now : " + principle + "\r\n" + "Percentage change : " + change + "\r\n" +
                "Short Traded : " + isShort + "\r\nStrong trade :" + isStrong
                );
        }

        //trade a hold and long strategy - in this method isSell means that the position should be sold. There is no short trading in this strategy
        private void simulateTradeLongStrategy(string symbol, bool isSell, string method, string[] prices, Application myPassedExcelApplication)
        {
            if (TradingForm.Instance.sellLong()) { isSell = true; };
            ExcelMethods ex = new ExcelMethods();
            //set the fileName to be passed for retreiving data
            string fileName = symbol.ToUpper() + method + "TradeLongHold";
            //get the starting priciple
            decimal principle = ex.ReadPrinciple(myPassedExcelApplication, fileName, symbol, false, true);
            bool positionHeld = ex.CheckIsHolding(myPassedExcelApplication, fileName, symbol);
          
            double startPrice = 0;
         
            double change = 0;
            //only use half of the principle on risky trades
            decimal principleHalf = principle / 2;
            double newPrinciple = 0;
            double principleDouble = Convert.ToDouble(principle);
            //check that data loaded correctly
            if (string.IsNullOrEmpty(prices[0]) || string.IsNullOrEmpty(prices[1]))
            {
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo financial data" + "\r\n");
                return;
            }
            try
            {
                startPrice = Convert.ToDouble(prices[0]);
               
               // change = calculateChangePercent(startPrice, closePrice);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
                TradingForm.Instance.AppendOutputText("\r\n" + "Failed to load Yahoo financial data" + "\r\n"); return;
            }
            bool profitable = false;
          
            if (isSell && positionHeld)
            {
                //get percent of chnage from the orginal position
              change =  ex.GetLongHoldPriceChangePrecentage(myPassedExcelApplication, fileName, symbol, startPrice);
                //change the percentage into a decimal for multiplying
              double realChange = change / 100;
                newPrinciple = principleDouble;
                newPrinciple += principleDouble * realChange;             
            }

            //roud the principle
            newPrinciple = Math.Round(newPrinciple, 2);
            if (newPrinciple > principleDouble) { profitable = true; }
            //ensure that a position isn't held already
            if (!positionHeld && !isSell) { 
            //save the trading data to an excel sheet
            ex.saveLongHoldTrade(myPassedExcelApplication, symbol, fileName, newPrinciple.ToString(), startPrice, startPrice, isSell, change, profitable);
                //write information to the text box
                TradingForm.Instance.AppendOutputText("\r\n" + symbol + "  " + method + "\r\n" + "Start Price : " + prices[0] + "\r\n" +
                    "Position Opened :" + prices[0] + "\r\n"

                    );
            }
            else if (isSell && positionHeld) {
                //save the trading data to an excel sheet
                ex.saveLongHoldTrade(myPassedExcelApplication, symbol, fileName, newPrinciple.ToString(), startPrice, startPrice, isSell, change, profitable);
                //write information to the text box
                TradingForm.Instance.AppendOutputText("\r\n" + symbol + "  " + method + "\r\n" + "Start Price : " + prices[0] + "\r\n" +
                "End Price :" + prices[1] + "\r\n" + "Remaining Prinicple : " + newPrinciple + "\r\n" +
                "Previous Principle : " + principleDouble + "\r\n" + "Percentage change : " + change + "\r\n" 
               
                );
            }           
        }

    }

}


