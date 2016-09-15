using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;
using StockPredictor.Models;
using System.Collections;

namespace StockPredictor.Helpers
{
    class CalculatorMethods
    {
        //a method to determine what percentage of the total the values are
        public int getPositivePercentage(int p, int n)
        {
            if (p > 0)
            {
                int total = p + n;
                int tp = 10000 / total;
                int pp = tp * p;
                int positivepercentage = pp / 100;
                return positivepercentage;
            }
            return 0;
        }


        //a method to determine what percentage of the total the values are
        public int getNegativePercentage(int p, int n)
        {
            if (n > 0)
            {
                int total = p + n;
                int tp = 10000 / total;
                int nn = tp * n;
                int negativePercentage = nn / 100;
                return negativePercentage;
            }
            return 0;
        }

        //a method to determine what percentage of the total the values are
        public int getTotalSentimentScore(int pw, int pp, int nw, int np)
        {
            pp = pp * 6;
            np = np * 6;
            int p = pp + pw;
            int n = np + nw;
            if (p > 0)
            {
                int total = p + n;
                int tp = 10000 / total;
                int ppp = tp * p;
                int positivepercentage = ppp / 100;
               
                //remove 5% of positive hits as because of noise, then add trend 
                return ((positivepercentage - 55) *2);
            }
            else if (n > 0)
            {
                int total = p + n;
                int tp = 10000 / total;
                int nn = tp * n;
                int negativePercentage = nn / 100;
                negativePercentage = (negativePercentage + 55) % 100;
                negativePercentage = negativePercentage * 2;
                negativePercentage = negativePercentage * -1;
             
                return negativePercentage;

            }


           
            return 0;
        }

        //calculate the RSI 
        //get the rsi get the sum of positive and negaitve change and divide it by 14. Then use the RSI formula
        public int RSI(string ticker)
        {
            List<HistoricalStock> data = YahooStockMethods.getHistoricalPriceData(ticker);
            if (data == null)
            {
                Console.WriteLine("data from yahoo return null. No interent?");
                return 0;
            };
            int last = data.Count;
            int i = 1;
            double positiveChange = 0;
            double negativeChange = 0;
            double positiveChangeLast = 0;
            double negativeChangeLast = 0;
            double rsi = 0;
            double rsiLast = 0;
            double change = 0;
            try
            {
                foreach (HistoricalStock stock in data)
                {
                    if (i >=2 && i < 16)
                    {
                        change = stock.Open - stock.Close;
                        if (change > 0) { positiveChange += change; }
                        else { negativeChange += change *-1; }
                        Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));
                        //save the price from two days ago for use later
                        if (i == 2) { Form1.Instance.TwoDayOldClosePrice = stock.Close; } 
                    }
                    if (i < 2)
                    {
                        change = stock.Open - stock.Close;
                        if (change > 0) { positiveChangeLast += change; }
                        else { negativeChangeLast += change * -1; }
                        Form1.Instance.lastClosePrice = stock.Close;
                        Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));
                    }
                    i++;
                }

                //this is the standard RSI equation
                double positiveChangeAverage = positiveChange / 14;
                double negativeChangeAverage = negativeChange / 14;
                Double rs = positiveChangeAverage / negativeChangeAverage;
                rsi = 100 - (100 / (1 + rs));
                Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nDay old RSI = " + rsi);
                //calculate todays RSI and if it has crossed the halfway mark
                double positiveChangeAverageLast = (positiveChangeAverage * 13 + positiveChangeLast)/14;
                double negativeChangeAverageLast = (negativeChangeAverage * 13 + negativeChangeLast)/14;
                Double rsLast = positiveChangeAverageLast / negativeChangeAverageLast;
                rsiLast = 100 - (100 / (1 + rsLast));
                Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nToday's RSI = " + rsiLast);
                Form1.Instance.realRSI = rsiLast;

                if (rsi < 50 && rsiLast > 50)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI buy signal = " + rsi + " " + rsiLast);
                    return  1;
                }
                if (rsi > 50 && rsiLast < 50)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI sell signal = " + rsi + " " + rsiLast);
                    return -1;
                }
                if(rsiLast >= 70)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI sell signal = " + rsi + " " + rsiLast);
                    return -2;
                }
                if (rsiLast <= 30)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI buy signal = " + rsi + " " + rsiLast);
                    return 2;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return 0;
        }

        //calculate the Bollinger Bonds over a 20 day period
        public void calculateBollingerBonds(string ticker)
        {
            List<HistoricalStock> data = YahooStockMethods.getHistoricalPriceData(ticker);
           
            double mean = 0;
            double[] closes = new double[63];
            double deviation = 0;
            double squared = 0;
            double standardDeviation = 0;
            double upperBand = 0;
            double lowerBand = 0;
            int i = 0;
            try
            {
                foreach (HistoricalStock stock in data)
                {
                    if (i < 63)
                    {
                        mean += stock.Close;
                        closes[i] = stock.Close;
                        Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));

                    }
                   
                    i++;
                    if(i == 63) { break; }
                }
                //find the actual mean of the 20 days of close prices
                mean = mean / closes.Length;
                for(int j=0; j < closes.Length; j++ )
                {
                    squared = Math.Pow(closes[j] - mean, 2);
                    deviation += squared;
                }
                standardDeviation = Math.Sqrt(deviation);
                upperBand = mean + (2 * standardDeviation);
                lowerBand = mean - (2 * standardDeviation);
                Form1.Instance.AppendOutputText("\r\nBollinger band information"+"\r\nMean value : " + mean + 
                    "\r\nUpper band : " + upperBand
                    + "\r\nLower band : " + lowerBand);
              
            }
            catch (Exception) { }
            }


        /* string[] cols = data.Split(',');
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
                    */
        //calculate the moving average and store them in variables on the main form
        private void MovingAverages(Hashtable htFunda)
        {
           
            string cp200 = htFunda["200ChangePercent"].ToString();
            string cp50 = htFunda["50ChangePercent"].ToString();
          
           cp50 = cp50.Replace("%", "");
           cp200 =  cp200.Replace("%", "");

            double ChangePercent200Here = 0;
            double ChangePercent50Here = 0;
            double average50 = 0;
            double average200 = 0;
            double twoDayOldPrice = 0;
            double lastClose = 0;
            try
            {
                ChangePercent200Here = Convert.ToDouble(cp200);
                ChangePercent50Here = Convert.ToDouble(cp50);
                average50 = Convert.ToDouble(htFunda["50Average"]);
                average200 = Convert.ToDouble(htFunda["200Average"]);
                twoDayOldPrice = Form1.Instance.TwoDayOldClosePrice;
                lastClose = Form1.Instance.lastClosePrice;
            }
            catch (Exception) { }
            Form1.Instance.AppendOutputText("\r\n ");

              //check for a cross over first 
             if (twoDayOldPrice > average50 && lastClose < average50)
            {
                Form1.Instance.AppendOutputText("\r\nCrossed over 50 day moving avergae in a bearish manner");
                moving50 = -2;
            }

            else if (twoDayOldPrice < average50 && lastClose > average50)
            {
                Form1.Instance.AppendOutputText("\r\nCrossed over the 50 day moving average in a bullish manner");
                moving50 = 2;
            }

            else if (twoDayOldPrice > average200 && lastClose < average200)
            {
                Form1.Instance.AppendOutputText("\r\nCroessed over the 200 day moving avergae in a bearish manner");
                moving200 = -2;
            }

            else if (twoDayOldPrice < average200 && lastClose > average200)
            {
                Form1.Instance.AppendOutputText("\r\nCroseed over the 200 day moving average in a bullish manner");
                moving200 = 2;
            }

             //check for movement near the moving average
            else if (ChangePercent50Here <= 0)
            {
                if (ChangePercent50Here > -1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                        + " : Verdict : Strong sell" );
                    moving50 = -2;
                }
                if (ChangePercent50Here <= -1 && ChangePercent50Here > -2 )
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : sell");
                    moving50 = -1;
                }
                if (ChangePercent50Here <= -1 && ChangePercent50Here > -3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : neutral");
                    moving50 = 0;
                } 
                        
            }
           else if(ChangePercent50Here > 0)
            {
                if (ChangePercent50Here < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : strong buy");
                    moving50 = 2;
                }
                if (ChangePercent50Here >= 1 && ChangePercent50Here < 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                        + " : Verdict : buy");
                    moving50 = 1;
                }
                if (ChangePercent50Here >= 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : neutral");
                    moving50 = 0;
                }

              

            }

            if (ChangePercent200Here <= 0)
            {
                if (ChangePercent200Here > -0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent50Here
                        + " : Verdict : Strong sell");
                    moving200 = -2;
                }
                if (ChangePercent200Here <= -0.5 && ChangePercent200Here > -1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : sell");
                    moving200 = -1;
                }
                if (ChangePercent200Here <= -1 && ChangePercent200Here > -3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : neutral");
                    moving200 = 0;
                }
                if (ChangePercent200Here <= -3 && ChangePercent200Here > -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                          + " : Verdict : buy");
                    moving200 = 1;
                }
                if (ChangePercent200Here <= -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                        + " : Verdict : strong buy");
                    moving200 = 2;
                }
            }
            if (ChangePercent200Here > 0)
            {
                if (ChangePercent200Here < 0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : strong buy");
                    moving200 = 2;
                }
                if (ChangePercent200Here >= 0.5 && ChangePercent200Here < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                        + " : Verdict : buy");
                    moving200 = 1;
                }
                if (ChangePercent200Here >= 1 && ChangePercent200Here < 3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : neutral");
                    moving200 = 0;
                }
                if (ChangePercent200Here >= 3 && ChangePercent200Here < 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                        + " : Verdict : sell");
                    moving200 = -1;
                }
                if (ChangePercent200Here >= 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : strong sell");
                    moving200 = -2;
                }
            }

        }
        /* string[] cols = data.Split(',');
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
                  */
                  //process the fundamentals of the stock
       private void ProcessFundamentals(Hashtable funda)
        {
            double dividendHere = 0;
           double priceBookHere = Convert.ToDouble(funda["PB"]);
            double pegHere = Convert.ToDouble(funda["PEG"]);
            try { 
            dividendHere = Convert.ToDouble(funda["Dividend"]);
            }
            catch (Exception) { dividendHere = 0; }
          
            //process the price to book
            if (priceBookHere <= 1)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong buy");
                priceBook = 2;
            }
            if (priceBookHere >1 && priceBookHere <=3)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: buy");
                priceBook = 1;
            }
            if (priceBookHere >3 && priceBookHere <=6)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: neutral");
                priceBook = 0;
            }
            if (priceBookHere >6 && priceBookHere <=8)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: sell");
                priceBook = -1;
            }
            if (priceBookHere >8)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong sell");
                priceBook = -2;
            }


            //process peg
            
            if (pegHere <= 1 && pegHere > 0) {
                Form1.Instance.AppendOutputText("\r\nPEG : strong buy");
                peg = 2;
                    }
            if (pegHere > 1 && pegHere <=2)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : buy");
                peg = 1;
                    }
            if (pegHere >2 && pegHere <=3)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : neutral");
                peg = 0;
                    }
            if (pegHere >3 && pegHere <=4)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : sell");
                peg = -1;
                    }
            if (pegHere > 4 && pegHere < 0 )
            {
                Form1.Instance.AppendOutputText("\r\nPEG : strong sell");
                peg = -2;
                    }
          
            //process dividends
            if (dividendHere ==0)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong sell");
                dividend = -2;
                    }
            if (dividendHere >0 && dividendHere <=1)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : sell");
                dividend = -1;
                    }
            if (dividendHere >1 && dividendHere <=2)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : neutral");
                dividend = 0;
                    }
            if (dividendHere >2 && dividendHere <=4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : buy");
                dividend = 1;
                    }
            if (dividendHere > 4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong buy");
                dividend = 2;
                    }

        }
        //convert sentiment score sperately
        private int ProcessSentimentScores(int sentiment)
        {
            

            if (sentiment <= -25)
            {
                return -2;
            }
            if (sentiment <= -5 && sentiment > -25)
            {
                return -1;
            }
            if (sentiment > -5 && sentiment <= 5)
            {
                return 0;
            }
            if (sentiment >= 5 && sentiment < 25)
            {
                return 1;
            }
            if (sentiment >= -25)
            {
                return 2;
            }
            return 0;
        }

       

        private int moving50 { get; set; }
        private int moving200 { get; set; }
        private int dividend { get; set; }
        private int peg { get; set; }
       private int priceBook { get; set; }
      

        //use the methoeds to calcualte the total prospects for the stock
        public double ProcessAllMetrics(Hashtable funda, int sentimentScore, int rsi, string method)
        {
            //process the moving averages
            MovingAverages(funda);
            //process the fundamentals
            ProcessFundamentals(funda);
            //process the sentiment score
            int sentiment = ProcessSentimentScores(sentimentScore) *5;
            rsi = rsi * 3;
            peg = peg * 2;
            double realRSI = Form1.Instance.realRSI;          
             
            double total = (sentiment + rsi + moving50 + moving200 + dividend + peg + priceBook) ;
            Form1.Instance.AppendOutputText("\r\n\r\nMethod : " + method);
            if (total >= 18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong buy : " + total);
                Form1.Instance.verdict = "strong buy";
            }
            if (total >= 6 && total < 18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : buy : " + total);
                Form1.Instance.verdict = "buy";
            }
            if (total <= 5 && total > -6)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : neutral : " + total);
                Form1.Instance.verdict = "neutral";
            }
            if (total < -6 && total > -18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : sell : " + total);
                Form1.Instance.verdict = "sell";
            }
            if (total <= -18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong sell :" + total + "\r\n");
                Form1.Instance.verdict = "strong sell";
            }

            //retrun the total value
            return total;
        }

    }
}
