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
        public int CalculateRSI(string ticker)
        {
            //  List<HistoricalStock> data = YahooStockMethods.GetHistoricalPriceData(ticker);
            List<HistoricalStock> data = Form1.Instance.HistoricalPriceData;

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
                    change = stock.Close - stock.Open;
                    if (i >=2 && i < 16)
                    {
                       
                        if (change > 0) { positiveChange += change; }
                        else { negativeChange += change *-1; }
                       
                        //save the price from two days ago for use later
                        if (i == 2) { Form1.Instance.TwoDayOldClosePrice = stock.Close; } 
                    }
                    if (i >= 1 && i<15)
                    {
                        change = stock.Close - stock.Open;
                        if (change > 0) { positiveChangeLast += change; }
                        else { negativeChangeLast += change * -1; }
                        Form1.Instance.LastClosePrice = stock.Close;
                        // Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));
                        //get the last days change in price
                        if (i ==1) { Form1.Instance.PriceChange = change; }
                    }

                    Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));
                    i++;
                }

                //this is the standard RSI equation
                double positiveChangeAverage = positiveChange / 14;
                double negativeChangeAverage = negativeChange / 14;
                Double rs = positiveChangeAverage / negativeChangeAverage;
                rsi = 100 - (100 / (1 + rs));
                Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nTwo day old RSI = " + rsi);
                //calculate todays RSI and if it has crossed the halfway mark
                // double positiveChangeAverageLast = (positiveChangeAverage * 13 + positiveChangeLast)/14;
                // double negativeChangeAverageLast = (negativeChangeAverage * 13 + negativeChangeLast)/14;
                double positiveChangeAverageLast = positiveChangeLast/ 14;
                double negativeChangeAverageLast = negativeChangeLast/ 14;
                Double rsLast = positiveChangeAverageLast / negativeChangeAverageLast;
                rsiLast = 100 - (100 / (1 + rsLast));
                Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nLast close RSI = " + rsiLast);
                Form1.Instance.RealRSI = rsiLast;

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
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI sell signal = " + rsiLast);
                    return -2;
                }
                if (rsiLast <= 30)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI buy signal = "+ rsiLast);
                    return 2;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return 0;
        }

        //calculate the Bollinger Bonds over a 20 day period
        public int calculateBollingerBands()
        {
            //  List<HistoricalStock> data = YahooStockMethods.getHistoricalPriceData(ticker);
            List<HistoricalStock> data = Form1.Instance.HistoricalPriceData;
            double mean = 0;
            double[] closes = new double[20];
            double deviation = 0;
            double squared = 0;
            double standardDeviation = 0;
            double upperBand = 0;
            double lowerBand = 0;
            double singleDeviationUpperBand = 0;
            double singleDeviationLowerBand = 0;
            double lastClose =0 ;
            double difference = 0;
            int i = 0;
            try
            {
                foreach (HistoricalStock stock in data)
                {
                    if (i==0) { lastClose = stock.Close; }
                    if (i < 20)
                    {
                        mean += stock.Close;
                        closes[i] = stock.Close;
                        Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));

                    }
                   
                    i++;
                    if(i == 20) { break; }
                }
                //find the actual mean of the 63 days of close prices
                mean = mean / closes.Length;
                for(int j=0; j < closes.Length-1; j++ )
                {
                    //find the diference between the mean the value
                   difference = closes[j] - mean;
                    //square it
                  squared = Math.Pow(difference, 2);
                    //add the squared values together
                    deviation += squared;
                }
                deviation = deviation / 20;
                standardDeviation = Math.Sqrt(deviation);
                upperBand = mean + (2 * standardDeviation);
                lowerBand = mean - (2 * standardDeviation);
                singleDeviationUpperBand = mean + standardDeviation;
                singleDeviationLowerBand = mean - standardDeviation;

                Form1.Instance.AppendOutputText("\r\nBollinger band information"+"\r\nMean value : " + mean + 
                    "\r\nUpper band : " + upperBand
                    + "\r\nLower band : " + lowerBand +
                    "\r\nSingle Deviation upper band : " + singleDeviationUpperBand +
                    "\r\nSingle deviation lower band : " + singleDeviationLowerBand);

                int verdict = 0;
                //provide a number to represent if it is a strong buy or sell etc.
                if (lastClose <= upperBand && lastClose >= singleDeviationUpperBand)
                {
                    Form1.Instance.AppendOutputText("\r\nPrice is in buy zone");
                    verdict = 2;
                }
                else if (lastClose >= lowerBand && lastClose <= singleDeviationLowerBand)
                {
                    Form1.Instance.AppendOutputText("\r\nPrice is in the sell zone");
                    verdict = -2;
                }
                else if (lastClose < lowerBand)
                {
                    Form1.Instance.AppendOutputText("\r\nPrice is may experience a rally");
                    verdict = 1;
                }
                else if (lastClose > upperBand)
                {
                    Form1.Instance.AppendOutputText("\r\nPrice is may experience a pullback");
                    verdict = -1;
                  
                }
                else { verdict = 0; }
                Form1.Instance.BollingerVerdict = verdict;
                return verdict;
            }
            catch (Exception) { Form1.Instance.BollingerVerdict = 0; return 0; }
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
                lastClose = Form1.Instance.LastClosePrice;
            }
            catch (Exception) { }
            Form1.Instance.AppendOutputText("\r\n ");

              //check for a cross over first 
             if (twoDayOldPrice > average50 && lastClose < average50)
            {
                Form1.Instance.AppendOutputText("\r\nCrossed over 50 day moving avergae in a bearish manner");
                Moving50 = -2;
            }

            else if (twoDayOldPrice < average50 && lastClose > average50)
            {
                Form1.Instance.AppendOutputText("\r\nCrossed over the 50 day moving average in a bullish manner");
                Moving50 = 2;
            }

            else if (twoDayOldPrice > average200 && lastClose < average200)
            {
                Form1.Instance.AppendOutputText("\r\nCroessed over the 200 day moving avergae in a bearish manner");
                Moving200 = -2;
            }

            else if (twoDayOldPrice < average200 && lastClose > average200)
            {
                Form1.Instance.AppendOutputText("\r\nCroseed over the 200 day moving average in a bullish manner");
                Moving200 = 2;
            }

             //check for movement near the moving average
            else if (ChangePercent50Here <= 0)
            {
                if (ChangePercent50Here > -1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                        + " : Verdict : Strong sell" );
                    Moving50 = -2;
                }
                if (ChangePercent50Here <= -1 && ChangePercent50Here > -2 )
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : sell");
                    Moving50 = -1;
                }
                if (ChangePercent50Here <= -1 && ChangePercent50Here > -3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : neutral");
                    Moving50 = 0;
                } 
                        
            }
           else if(ChangePercent50Here > 0)
            {
                if (ChangePercent50Here < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : strong buy");
                    Moving50 = 2;
                }
                if (ChangePercent50Here >= 1 && ChangePercent50Here < 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                        + " : Verdict : buy");
                    Moving50 = 1;
                }
                if (ChangePercent50Here >= 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50Here
                         + " : Verdict : neutral");
                    Moving50 = 0;
                }

              

            }

            if (ChangePercent200Here <= 0)
            {
                if (ChangePercent200Here > -1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent50Here
                        + " : Verdict : Strong sell");
                    Moving200 = -2;
                }
                if (ChangePercent200Here <= -1 && ChangePercent200Here > -2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : sell");
                    Moving200 = -1;
                }
                if (ChangePercent200Here <= -2 )
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : neutral");
                    Moving200 = 0;
                }
               
            }
            if (ChangePercent200Here > 0)
            {
                if (ChangePercent200Here < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : strong buy");
                    Moving200 = 2;
                }
                if (ChangePercent200Here >= 1 && ChangePercent200Here < 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                        + " : Verdict : buy");
                    Moving200 = 1;
                }
                if (ChangePercent200Here >= 2)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200Here
                         + " : Verdict : neutral");
                    Moving200 = 0;
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
            double priceBookHere= 0;
                double pegHere = 0;
            try
            {
               priceBookHere = Convert.ToDouble(funda["PB"]);
            pegHere = Convert.ToDouble(funda["PEG"]);
           
            dividendHere = Convert.ToDouble(funda["Dividend"]);
            }
            catch (Exception) { dividendHere = 0; }
          
            //process the price to book
            if (priceBookHere <= 1)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong buy");
                PriceBook = 2;
            }
            if (priceBookHere >1 && priceBookHere <=4)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: buy");
                PriceBook = 1;
            }
            if (priceBookHere >4 && priceBookHere <=7)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: neutral");
                PriceBook = 0;
            }
            if (priceBookHere >7 && priceBookHere <=9)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: sell");
                PriceBook = -1;
            }
            if (priceBookHere >9)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong sell");
                PriceBook = -2;
            }


            //process peg
            
            if (pegHere <= 1 && pegHere > 0) {
                Form1.Instance.AppendOutputText("\r\nPEG : strong buy");
                Peg = 2;
                    }
            if (pegHere > 1 && pegHere <=2)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : buy");
                Peg = 1;
                    }
            if (pegHere >2 && pegHere <=3)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : neutral");
                Peg = 0;
                    }
            if (pegHere >3 && pegHere <=4)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : sell");
                Peg = -1;
                    }
            if (pegHere > 4 || pegHere < 0 )
            {
                Form1.Instance.AppendOutputText("\r\nPEG : strong sell");
                Peg = -2;
                    }
          
            //process dividends
            if (dividendHere ==0)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong sell");
                Dividend = -2;
                    }
            if (dividendHere >0 && dividendHere <=1)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : sell");
                Dividend = -1;
                    }
            if (dividendHere >1 && dividendHere <=2)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : neutral");
                Dividend = 0;
                    }
            if (dividendHere >2 && dividendHere <=4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : buy");
                Dividend = 1;
                    }
            if (dividendHere > 4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong buy");
                Dividend = 2;
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
            if (sentiment >= 25)
            {
                return 2;
            }
            return 0;
        }

       
//variables that are used by several methods
        private int Moving50 { get; set; }
        private int Moving200 { get; set; }
        private int Dividend { get; set; }
        private int Peg { get; set; }
        private int PriceBook { get; set; }
        private int Bollinger { get; set; }
        private int SentimentScore { get; set; }
        private int RSI { get; set; }

        //use the methoeds to calcualte the total prospects for the stock
        public double ProcessAllMetrics(Hashtable funda, int sentimentScore, int rsi, string method, int bands)
        {
            //set the global variables
            RSI = rsi;
            Bollinger = bands;
            //process the moving averages
            MovingAverages(funda);
            //process the fundamentals
            ProcessFundamentals(funda);
            //process the sentiment score : weight the more significant scores. Total 13 - min -32 max +32
            int sentiment = ProcessSentimentScores(sentimentScore);
            SentimentScore = sentiment;
            sentiment = sentiment * 5;
            rsi = rsi * 4;
            int peg = Peg * 2;
            bands = bands * 3;


            double total = (sentiment + rsi + bands + peg + Moving50 + Moving200 + Dividend + PriceBook ) ;
            Form1.Instance.AppendOutputText("\r\n\r\nMethod : " + method);
            if (total >= 18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong buy : " + total);
                Form1.Instance.Verdict = "Strong Buy";
            }
            if (total > 4 && total < 18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : buy : " + total);
                Form1.Instance.Verdict = "Buy";
            }
            if (total <= 4 && total >= -4)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : neutral : " + total);
                Form1.Instance.Verdict = "Neutral";
            }
            if (total < -4 && total > -18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : sell : " + total);
                Form1.Instance.Verdict = "Sell";
            }
            if (total <= -18)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong sell :" + total + "\r\n");
                Form1.Instance.Verdict = "Strong Sell";
            }

            //retrun the total value
            return total;
        }

        //display the results on the form
        public void displayResults()
        {
            string results = "";

            results += ConvertScoreToString(SentimentScore) + ",";
            results += ConvertScoreToString(RSI) + ","; 
            results += ConvertScoreToString(Peg) + ","; 
            results += ConvertScoreToString(Bollinger) + ","; 
            results += ConvertScoreToString(PriceBook) + ","; 
            results += ConvertScoreToString(Dividend) + ","; 
            results += ConvertScoreToString(Moving50) + ",";
            results += ConvertScoreToString(Moving200) + ","; 

            Form1.Instance.AppendTextBoxes(results);

        }
        //convert the scores to read able strings
        private string ConvertScoreToString(int score)
        {
            string str = "Neutral";
            if (score ==2) { str = "Strong Buy";  }
            else if (score == 1) { str = "Buy"; }
            else if (score == 0) { str = "Neutral"; }
            else if (score == -1) { str = "Sell"; }
            else if (score == -2) { str = "Strong Sell"; }
            return str;

        }
    }
}
