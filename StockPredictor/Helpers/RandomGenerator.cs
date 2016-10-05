﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace StockPredictor.Helpers
{
    class RandomGenerator
    {
        //generate random results and store them in an excel file
        public void generateRandomResults(string fileName, Application myPassedExcelApplication)
        {
            //randomly generate the percentage results
            Random rnd = new Random();
            //int posWordPercentage = rnd.Next(1, 100);
            //int negWordPercentage = 100 - posWordPercentage;
            //int posPhrasePercentage = rnd.Next(1,100);
            //int negPhrasePercentage = 100 - posPhrasePercentage;
            int total = rnd.Next(-22, 22);

            if (total >= 15)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong buy : " + total);
                Form1.Instance.scanMetrics.Verdict = "Strong Buy";
            }
            if (total > 4 && total < 15)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : buy : " + total);
                Form1.Instance.scanMetrics.Verdict = "Buy";
            }
            if (total <= 4 && total >= -4)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : neutral : " + total);
                Form1.Instance.scanMetrics.Verdict = "Neutral";
            }
            if (total < -4 && total > -15)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : sell : " + total);
                Form1.Instance.scanMetrics.Verdict = "Sell";
            }
            if (total <= -15)
            {
                Form1.Instance.AppendOutputText("\r\nTotal score : strong sell :" + total + "\r\n");
                Form1.Instance.scanMetrics.Verdict = "Strong Sell";
            }

            string elapsedMs = "0";
            //add the output data to an excel file
            ExcelMethods em = new ExcelMethods();
            //add the data to special excel file for only this specific out put for this stock
            em.savePredictorDataToExcel(myPassedExcelApplication, fileName, "Random", elapsedMs, 0, 0,0,0,0,0,0,0,0,0,0,total);


        }
    }
}
