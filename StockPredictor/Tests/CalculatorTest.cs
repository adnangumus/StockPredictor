using StockPredictor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Tests
{
    class CalculatorTest
    {
        //calculate the Bollinger Bonds over a 20 day period
        public int calculateBollingerBandsYesterday()
        {
            //reset the bollinger verdict
            Form1.Instance.scanMetrics.BollingerVerdict = 0;
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
            double lastClose = 0;
            double difference = 0;
            int i = 0;
            try
            {
                foreach (HistoricalStock stock in data)
                {
                    if (i == 1) { lastClose = stock.Close; }
                    if (i < 21)
                    {
                        mean += stock.Close;
                        closes[i] = stock.Close;
                        Console.WriteLine(string.Format("Date={0} High={1} Low={2} Open={3} Close{4}", stock.Date, stock.High, stock.Low, stock.Open, stock.Close));

                    }

                    i++;
                    if (i == 20) { break; }
                }
                //find the actual mean of the 63 days of close prices
                mean = mean / closes.Length;
                for (int j = 0; j < closes.Length - 1; j++)
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

                Form1.Instance.AppendOutputText("\r\nBollinger band information" + "\r\nMean value : " + mean +
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
                Form1.Instance.scanMetrics.BollingerVerdict = verdict;
                return verdict;
            }
            catch (Exception) { Form1.Instance.scanMetrics.BollingerVerdict = 0; return 0; }
        }

    }
}
