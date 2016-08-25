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
            //check if bing is used or google
            if (Form1.Instance.useBing() && !Form1.Instance.useGoogle())
            {
                BingMethods bing = new BingMethods();
                string bingUrl = "http://cn.bing.com/news/search?q=" + input + "+" + companyName + "&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1";
                Form1.Instance.AppendOutputText("\r\n" + "URL used : " + bingUrl + "\r\n");
                links = bing.getBinglinks(bingUrl);             
            }
            else if(!Form1.Instance.useBing() && Form1.Instance.useGoogle())
            {
                GoogleMethods gm = new GoogleMethods();
                String url = "https://www.google.com/search?q=NASDAQ+" + input + "+" + companyName + "+News&tbm=nws&tbs=qdr:d";
                links = gm.getGooglelinks(url);

            }
            //this is the defualt 
            else {
                GoogleMethods gm = new GoogleMethods();
                String url = "https://www.google.com/search?q=NASDAQ+" + input + "+" + companyName + "+News&tbm=nws&tbs=qdr:d";
            links = gm.getGooglelinks(url);
               
             BingMethods bing = new BingMethods();
             string bingUrl = "http://cn.bing.com/news/search?q=" + input + "+" + companyName + "&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1";
              Form1.Instance.AppendOutputText("\r\n" + "URL used : " + bingUrl + "\r\n");
              links.AddRange(bing.getBinglinks(bingUrl));
               
            }
            //check if the there are any links available 
            if (links.Count == 0) { Form1.Instance.AppendOutputText("\r\n" + "Failed to load links : " + "\r\n"); return; }
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
