using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int getTotalScore(int pw, int pp, int nw, int np)
        {
            double trend = 0;
            int trendInt = 0;
            try { 
                //get the trend and multiply it by 10 and add its impact to the total score
           trend = Form1.Instance.trend;               
           trendInt = System.Convert.ToInt32(System.Math.Round(trend));
                trendInt = trendInt * 10;
                Form1.Instance.AppendOutputText("\n\rThe trend as an int : "+trendInt);
            }
            catch (Exception) { }
       
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
                return ((positivepercentage - 55) *2)+ trendInt;
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
                return negativePercentage + trendInt;
            }
           
            return 0;
        }
    }
}
