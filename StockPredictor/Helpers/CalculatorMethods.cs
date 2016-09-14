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
            /*code that added the trend
           // double trend = 0;
           // int trendInt = 0;
           // try { 
           //     //get the trend and multiply it by 10 and add its impact to the total score
           //trend = Form1.Instance.trend;
           //     trend = trend * 10;
           //     trendInt = System.Convert.ToInt32(System.Math.Round(trend));
           //     Form1.Instance.AppendOutputText("\n\rThe trend as an int : "+trendInt);
           // }
            catch (Exception) { }
           */

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
                negativePercentage  = (negativePercentage + 55)%100 ;
                negativePercentage = negativePercentage * 2;
                negativePercentage = negativePercentage * -1;
                return negativePercentage;
            }
           
            return 0;
        }

        //calculate the RSI 
        //get the rsi get the sum of positive and negaitve change and divide it by 14. Then use the RSI formula
        public double getRSI(string ticker)
        {
            List<HistoricalStock> data = YahooStockMethods.getTwoWeekData(ticker);
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
                       
                    }
                    if (i < 2)
                    {
                        change = stock.Open - stock.Close;
                        if (change > 0) { positiveChangeLast += change; }
                        else { negativeChangeLast += change * -1; }
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
                Form1.Instance.rsi = 0;

                if (rsi < 50 && rsiLast > 50)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI buy signal = " + rsi + " " + rsiLast);
                    Form1.Instance.rsi = 1;
                }
                if (rsi > 50 && rsiLast < 50)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI sell signal = " + rsi + " " + rsiLast);
                    Form1.Instance.rsi = -1;
                }
                if(rsiLast >= 70)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI sell signal = " + rsi + " " + rsiLast);
                    Form1.Instance.rsi = -2;
                }
                if (rsiLast <= 30)
                {
                    Form1.Instance.AppendOutputText("\r\n" + ticker + "\r\nRSI buy signal = " + rsi + " " + rsiLast);
                    Form1.Instance.rsi = 2;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return rsi;
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
        public void MovingAverages(Hashtable htFunda)
        {
           
            string cp200 = htFunda["200ChangePercent"].ToString();
            string cp50 = htFunda["50ChangePercent"].ToString();
           cp50 = cp50.Replace("%", "");
           cp200 =  cp200.Replace("%", "");

            double ChangePercent200 = Convert.ToDouble(cp200);
            double ChangePercent50 = Convert.ToDouble(cp50);

            if(ChangePercent50 <= 0)
            {
                if (ChangePercent50 > -0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                        + " : Verdict : Strong sell" );
                    Form1.Instance.moving50 = -2;
                }
                if (ChangePercent50 <= -0.5 && ChangePercent50 > -1 )
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                         + " : Verdict : sell");
                    Form1.Instance.moving50 = -1;
                }
                if (ChangePercent50 <= -1 && ChangePercent50 > -3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                         + " : Verdict : neutral");
                    Form1.Instance.moving50 = 0;
                }
                if (ChangePercent50 <= -3 && ChangePercent50 > -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                          + " : Verdict : buy");
                    Form1.Instance.moving50 = 1;
                }
                if (ChangePercent50 <= -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                        + " : Verdict : strong buy");
                    Form1.Instance.moving50 = 2;
                }
            }
            if(ChangePercent50 > 0)
            {
                if (ChangePercent50 < 0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                         + " : Verdict : strong buy");
                    Form1.Instance.moving50 = 2;
                }
                if (ChangePercent50 >= 0.5 && ChangePercent50 < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                        + " : Verdict : buy");
                    Form1.Instance.moving50 = 1;
                }
                if (ChangePercent50 >= 1 && ChangePercent50 < 3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                         + " : Verdict : neutral");
                    Form1.Instance.moving50 = 0;
                }
                if (ChangePercent50 >= 3 && ChangePercent50 < 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                        + " : Verdict : sell");
                    Form1.Instance.moving50 = -1;
                }
                if (ChangePercent50 >= 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 50 day moving average" + ChangePercent50
                         + " : Verdict : strong sell");
                    Form1.Instance.moving50 = -2;
                }
            }

            if (ChangePercent200 <= 0)
            {
                if (ChangePercent200 > -0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent50
                        + " : Verdict : Strong sell");
                    Form1.Instance.moving200 = -2;
                }
                if (ChangePercent200 <= -0.5 && ChangePercent200 > -1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                         + " : Verdict : sell");
                    Form1.Instance.moving200 = -1;
                }
                if (ChangePercent200 <= -1 && ChangePercent200 > -3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                         + " : Verdict : neutral");
                    Form1.Instance.moving200 = 0;
                }
                if (ChangePercent200 <= -3 && ChangePercent200 > -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                          + " : Verdict : buy");
                    Form1.Instance.moving200 = 1;
                }
                if (ChangePercent200 <= -4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                        + " : Verdict : strong buy");
                    Form1.Instance.moving200 = 2;
                }
            }
            if (ChangePercent200 > 0)
            {
                if (ChangePercent200 < 0.5)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                         + " : Verdict : strong buy");
                    Form1.Instance.moving200 = 2;
                }
                if (ChangePercent200 >= 0.5 && ChangePercent200 < 1)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                        + " : Verdict : buy");
                    Form1.Instance.moving200 = 1;
                }
                if (ChangePercent200 >= 1 && ChangePercent200 < 3)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                         + " : Verdict : neutral");
                    Form1.Instance.moving200 = 0;
                }
                if (ChangePercent200 >= 3 && ChangePercent200 < 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                        + " : Verdict : sell");
                    Form1.Instance.moving200 = -1;
                }
                if (ChangePercent200 >= 4)
                {
                    Form1.Instance.AppendOutputText("\r\nPercentage from 200 day moving average" + ChangePercent200
                         + " : Verdict : strong sell");
                    Form1.Instance.moving200 = -2;
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
        public void ProcessFundamentals(Hashtable funda)
        {
           double priceBook = Convert.ToDouble(funda["PB"]);
            double peg = Convert.ToDouble(funda["PEG"]);
            double dividend = Convert.ToDouble(funda["Dividend"]);

            //process the price to book
            if (priceBook <= 1)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong buy");
                Form1.Instance.priceBook = 2;
            }
            if (priceBook >1 && priceBook <=3)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: buy");
                Form1.Instance.priceBook = 1;
            }
            if (priceBook >3 && priceBook <=6)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: neutral");
                Form1.Instance.priceBook = 0;
            }
            if (priceBook >6 && priceBook <=8)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: sell");
                Form1.Instance.priceBook = -1;
            }
            if (priceBook >8)
            {
                Form1.Instance.AppendOutputText("\r\nPrice to book: strong sell");
                Form1.Instance.priceBook = -2;
            }         


//process peg
            if (peg <= 1) {
                Form1.Instance.AppendOutputText("\r\nPEG : strong buy");
                Form1.Instance.peg = 2;
                    }
            if (peg > 1 && peg <=2)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : buy");
                Form1.Instance.peg = 1;
                    }
            if (peg >2 && peg <=3)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : neutral");
                Form1.Instance.peg = 0;
                    }
            if (peg >3 && peg <=4)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : sell");
                Form1.Instance.peg = -1;
                    }
            if (peg > 4)
            {
                Form1.Instance.AppendOutputText("\r\nPEG : strong sell");
                Form1.Instance.peg = -2;
                    }

            //process dividends
            if (dividend ==0)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong sell");
                Form1.Instance.dividend = -2;
                    }
            if (dividend >0 && dividend <=1)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : sell");
                Form1.Instance.dividend = -1;
                    }
            if (dividend >1 && dividend <=2)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : neutral");
                Form1.Instance.dividend = 0;
                    }
            if (dividend >2 && dividend <=4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : buy");
                Form1.Instance.dividend = 1;
                    }
            if (dividend > 4)
            {
                Form1.Instance.AppendOutputText("\r\nDividends : strong buy");
                Form1.Instance.dividend = 2;
                    }

        }

    }
}
