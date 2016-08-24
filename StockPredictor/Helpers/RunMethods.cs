using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace StockPredictor.Helpers
{
    class RunMethods
    {
        public void runStockPredictor(string input, bool dontSave)
        {
            //intiate classes used
            Mining miner = new Mining();
            GoogleMethods gm = new GoogleMethods();
            //get the companies name from yahoo's api to make the search more specific.
            YahooStockMethods yahoo = new YahooStockMethods();
            string companyName;
            try { 
            companyName = yahoo.getStockName(input);
            }
            catch(Exception ex)
            {
                companyName = "";
            }
            List<string> links = new List<string>();
            String url = "https://www.google.com/search?q=NASDAQ+" + input + "+" + companyName + "+News&tbm=nws&tbs=qdr:d";
            links = gm.getGooglelinks(url);
            string articles = miner.getAllArticles(links);
            //intialize and process the named and noun entities
            PosTagger pt = new PosTagger();
            //check if the user wants to save the data
            Application myPassExcelApp = null;
            ExcelMethods exl = new ExcelMethods();
            if (!dontSave) {
              
                myPassExcelApp = exl.startExcelApp();
            }
            Task taskA = new Task(() => pt.processNamedNoun(articles, input, dontSave, myPassExcelApp));
            //intialize and set up a thread for processing bag of words
            BagOfWords bag = new BagOfWords();
            Task taskB = new Task(() => bag.processBagOfWords(articles, input, dontSave, myPassExcelApp));
            //stat the tasks
            taskA.Start();
            taskB.RunSynchronously();
            //  taskC.RunSynchronously();
            taskA.GetAwaiter();
            taskB.Wait();
            //if the user wants to save the results run the random generator
            if (!dontSave) { 
            //randomly generate results
            RandomGenerator rg = new RandomGenerator();
            rg.generateRandomResults(input, myPassExcelApp);
                exl.quitExcel(myPassExcelApp);
            }

        }
    }
}
