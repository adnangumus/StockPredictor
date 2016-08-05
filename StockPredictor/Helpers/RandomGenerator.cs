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
            int posWordPercentage = rnd.Next(1, 100);
            int negWordPercentage = 100 - posWordPercentage;
            int posPhrasePercentage = rnd.Next(1,100);
            int negPhrasePercentage = 100 - posPhrasePercentage;

            string elapsedMs = "0";
            int wordCount = 0;
                int sentenceCount = 0;
            int posWordCount = 0;
            int negWordCount = 0;
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;

            //add the output data to an excel file
            ExcelMethods em = new ExcelMethods();
            //add the data to special excel file for only this specific out put for this stock
            em.savePredictorDataToExcel(fileName, "Random", elapsedMs, wordCount, sentenceCount, posWordCount, negWordCount,
          posWordPercentage, negWordPercentage,
           positivePhraseCount, negativePhraseCount,
           posPhrasePercentage, negPhrasePercentage);
        }
    }
}
