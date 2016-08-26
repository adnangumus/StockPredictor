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
          
           
            List<string> links = new List<string>();

            links = switchLinks(input);
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

        //methdo for switching between search engines
        private List<string> switchLinks(string input)
        {
            //get the companies name from yahoo's api to make the search more specific.
            YahooStockMethods yahoo = new YahooStockMethods();
            string companyName;
            try
            {
                companyName = yahoo.getStockName(input);
            }
            catch (Exception ex)
            {
                companyName = "";
            }
            //create url strings for methods
            string bingUrl = "http://cn.bing.com/news/search?q=" + input + "+" + companyName + "&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1";
            string yahooUrl = "https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=" + input + "+" + companyName + "&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t";
            String googleUrl = "https://www.google.com/search?q=NASDAQ+" + input + "+" + companyName + "+News&tbm=nws&tbs=qdr:d";
            //find out what search engines were chose
            bool useBing = Form1.Instance.useBing();
            bool useGoogle = Form1.Instance.useGoogle();
            bool useYahoo = Form1.Instance.useYahoo();
            List<string> links = new List<string>();
            
            if (useBing && !useGoogle && !useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Bing"+"\r\n");
                BingMethods bing = new BingMethods();
               
                links = bing.getBinglinks(bingUrl);
            }

             else if(useBing && !useGoogle && useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Bing, Yahoo" + "\r\n");
                YahooMiner ym = new YahooMiner();
                links = ym.getYahoolinks(yahooUrl);

                 BingMethods bing = new BingMethods();
                links.AddRange(bing.getBinglinks(bingUrl));


            }

            else if(useBing && useGoogle && !useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Bing, Google" + "\r\n");
                GoogleMethods goog = new GoogleMethods();
                links = goog.getGooglelinks(googleUrl);
                if (links.Count == 0)
                {
                    Form1.Instance.AppendOutputText("\r\n" + "Google failed to connect" + "\r\n");
                }

                    BingMethods bing = new BingMethods();
                links.AddRange(bing.getBinglinks(bingUrl));
            }

            else if(!useBing && useGoogle && !useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Google" + "\r\n");
                GoogleMethods goog = new GoogleMethods();
                links = goog.getGooglelinks(googleUrl);
                if (links.Count == 0)
                {
                    Form1.Instance.AppendOutputText("\r\n" + "Google failed to connect" + "\r\n");
                }
            }

            else if (!useBing && useGoogle && useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Yahoo, Google" + "\r\n");
                GoogleMethods goog = new GoogleMethods();
                links = goog.getGooglelinks(googleUrl);
                if (links.Count == 0)
                {
                    Form1.Instance.AppendOutputText("\r\n" + "Google failed to connect" + "\r\n");
                }

                YahooMiner ym = new YahooMiner();
                links.AddRange(ym.getYahoolinks(yahooUrl));
            }

            else if (!useBing && !useGoogle && useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Yahoo" + "\r\n");

                YahooMiner ym = new YahooMiner();
                links = ym.getYahoolinks(yahooUrl);
            }
            else {
                Form1.Instance.AppendOutputText("\r\n" + "Used : All" + "\r\n");
                GoogleMethods goog = new GoogleMethods();
                links = goog.getGooglelinks(googleUrl);
                if (links.Count == 0)
                {
                    Form1.Instance.AppendOutputText("\r\n" + "Google failed to connect" + "\r\n");
                }

                YahooMiner ym = new YahooMiner();
                links.AddRange(ym.getYahoolinks(yahooUrl));

                BingMethods bing = new BingMethods();
                links.AddRange(bing.getBinglinks(bingUrl));
            }

            return links;
        }
    }
}
