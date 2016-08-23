using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class RandomGenerator
    {
        //generate random results and store them in an excel file
        public void generateRandomResults(string fileName)
        {
            //randomly generate the percentage results
            Random rnd = new Random();
            //int posWordPercentage = rnd.Next(1, 100);
            //int negWordPercentage = 100 - posWordPercentage;
            //int posPhrasePercentage = rnd.Next(1,100);
            //int negPhrasePercentage = 100 - posPhrasePercentage;
            int totalScore = rnd.Next(1, 100);
            string elapsedMs = "0";
            //add the output data to an excel file
            ExcelMethods em = new ExcelMethods();
            //add the data to special excel file for only this specific out put for this stock
            em.savePredictorDataToExcel(fileName, "Random", elapsedMs, totalScore, 0,0,0,0,0,0,0,0,0,0);
        }
    }
}
