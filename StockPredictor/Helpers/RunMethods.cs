using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class RunMethods
    {
        public void runStockPredictor(string input)
        {
            //intiate classes used
            Mining miner = new Mining();
            GoogleMethods gm = new GoogleMethods();
            YahooStockMethods yahoo = new YahooStockMethods();
           string companyName = yahoo.getStockName(input);
            List<string> links = new List<string>();
            String url = "https://www.google.com/search?q=NASDAQ+" + input + companyName + "+News&tbm=nws&tbs=qdr:d";
            links = gm.getGooglelinks(url);
            string articles = miner.getAllArticles(links);
            //intialize and process the named and noun entities
            PosTagger pt = new PosTagger();
            Task taskA = new Task(() => pt.processNamedNoun(articles, input));
            //intialize and set up a thread for processing bag of words
            BagOfWords bag = new BagOfWords();
            Task taskB = new Task(() => bag.processBagOfWords(articles, input));
            //stat the tasks
            taskA.Start();
            taskB.RunSynchronously();
            //  taskC.RunSynchronously();
            taskA.GetAwaiter();
            taskB.Wait();
            //randomly generate results
            RandomGenerator rg = new RandomGenerator();
            rg.generateRandomResults(input);

        }
    }
}
