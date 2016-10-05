using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Models
{
   public class ScanMetric
    {
        //ranging from -2 to 2 Strong sell sell neutral buy strong buy
        public int Rsi { get; set; }
        //the real rsi
        public double RealRSI { get; set; }
        //the scores from the moving average ranging from -2 to 2 Strong sell sell neutral buy strong buy
        public string Moving200 { get; set; }
        public string Moving50 { get; set; }
        public string PriceBook { get; set; }
        public string Peg { get; set; }
        public string Dividend { get; set; }
        public Hashtable Fundamentals { get; set; }
        public string Verdict { get; set; }

      
        public double TwoDayOldClosePrice { get; set; }
        public double LastClosePrice { get; set; }
        public string UpperBand { get; set; }
        public string LowerBand { get; set; }
        public string Mean { get; set; }
        public int BollingerVerdict { get; set; }
        public string Ticker { get; set; }
        public double PriceChange { get; set; }
        public bool IsHolding { get; set; }
    }
}
