﻿using System;
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
    }
}
