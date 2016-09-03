using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Collections;

namespace StockPredictor.Helpers
{
    class RunMethods
    {
        private int retries
        {
            get;
            set;
        }
        public void runStockPredictor(string input, bool dontSave)
        {
            //intiate classes used
            Mining miner = new Mining();
            //hash table for the bag of words method
            Hashtable bagHT = new Hashtable();
            List<Hashtable> hts = new List<Hashtable>();
            Hashtable nounHT = new Hashtable();
            Hashtable namedHT = new Hashtable();

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
            if (!dontSave)
            {

                myPassExcelApp = exl.startExcelApp();
            }
            Task taskA = new Task(() => hts = pt.processNamedNoun(articles, input, dontSave));
            //intialize and set up a thread for processing bag of words
            BagOfWords bag = new BagOfWords();
            Task taskB = new Task(() => bagHT = (bag.processBagOfWords(articles, input, dontSave)));
            //stat the tasks            
            taskA.Start();
            taskB.RunSynchronously();
        
            //  taskC.RunSynchronously();
            Task.WaitAll(taskA, taskB);
            
            //if the user wants to save the results run the random generator
            if (!dontSave)
            {
                hts.Add(bagHT);
                foreach (Hashtable ht in hts)
                {
                    processSaves(ht, input, exl, myPassExcelApp);
                }

                //randomly generate results
                RandomGenerator rg = new RandomGenerator();
                rg.generateRandomResults(input, myPassExcelApp);
                exl.quitExcel(myPassExcelApp);
            }
            //confirm that the input is correct
            confirmAllCompleted(input, dontSave);
        }

        private void processSaves(Hashtable ht, string input, ExcelMethods exl, Application myPassExcelApp)
        {
            //count positive and negative phrases and strong words
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;
            int strongPositivePhraseCount = 0;
            int strongNegativePhraseCount = 0;
            int negWordCount = 0;
            int posWordCount = 0;
            int wordCount = 0;
            int sentenceCount = 0;
            string elapsedMs;
            int posWordPercentage = 0;
            int negWordPercentage = 0;
            int posPhrasePercentage = 0;
            int negPhrasePercentage = 0;
            int totalScore = 0;
            string method;
            //this method will take the out put data from the methods and save it to an excel file      
            //  public void savePredictorDataToExcel(Excel.Application myPassedExcelApplication, string fileName, string method, string elapsedMs, int totalScore, int wordCount, int sentenceCount, int posWordCount, int negWordCount,
            //  int posWordPercentage, int negWordPercentage,
            //int positivePhraseCount, int negativePhraseCount,
            //int posPhrasePercentage, int negPhrasePercentage)
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc 9=pwp 10=nwp 11=npp 12=ppp 13=total 14=tt 15= method
            posWordCount = (int)ht["pw"];
            negWordCount = (int)ht["nw"];
            positivePhraseCount = (int)ht["pp"];
            negativePhraseCount = (int)ht["np"];
            wordCount = (int)ht["wc"];
            sentenceCount = (int)ht["sc"]; //6
            posPhrasePercentage = (int)ht["ppp"];
            negPhrasePercentage = (int)ht["npp"];
            posWordPercentage = (int)ht["pwp"];
            negWordPercentage = (int)ht["nwp"];
            elapsedMs = ht["tt"].ToString();
            method = ht["method"].ToString();
            totalScore = (int)ht["total"];

            exl.savePredictorDataToExcel(myPassExcelApp, input, method, elapsedMs, totalScore, wordCount, sentenceCount, posWordCount, negWordCount,
                  posWordPercentage, negWordPercentage,
                   positivePhraseCount, negativePhraseCount,
                  posPhrasePercentage, negPhrasePercentage);


        }

        private void confirmAllCompleted(string input, bool dontSave)
        {
            if (retries == 0) { retries = 0; };
            string output = Form1.Instance.getTBOutputText();
            //check that the run method executed correctly
            if (!output.Contains(input + "\r\n" + "Bag of words Method") || !output.Contains(input + "\r\n" + "Noun phrase method") || !output.Contains(input + "\r\n" + "Named entities method"))
            {
                retries++;
                if (retries < 3)
                {
                    Form1.Instance.AppendOutputText("\r\n" + "failed confirmation" + "\r\n" + input + "\r\n");
                    //run it again
                    runStockPredictor(input, dontSave);
                }
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
                Form1.Instance.AppendOutputText("\r\n" + "Used : Bing" + "\r\n");
                BingMethods bing = new BingMethods();

                links = bing.getBinglinks(bingUrl);
            }

            else if (useBing && !useGoogle && useYahoo)
            {
                Form1.Instance.AppendOutputText("\r\n" + "Used : Bing, Yahoo" + "\r\n");
                YahooMiner ym = new YahooMiner();
                links = ym.getYahoolinks(yahooUrl);

                BingMethods bing = new BingMethods();
                links.AddRange(bing.getBinglinks(bingUrl));


            }

            else if (useBing && useGoogle && !useYahoo)
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

            else if (!useBing && useGoogle && !useYahoo)
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
            else
            {
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
