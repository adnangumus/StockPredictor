using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Tests
{
    class MiningTest
    {
        // Text for tagging
        string text = "A Part-Of-Speech Tagger (POS Tagger) is Gileard a piece of software that reads text"
                    + "in some language and assigns parts of speech to each word (and other token),"
                    + " such as noun, verb, adjective, etc., although generally computational "
                    + "applications use more fine-grained POS tags like 'noun-plural'."
                    + "Here is a new sentence to be scanned for sintax with GILD."
                     + "Here is a new sentence to be scanned for sintax with IBB."
                     + "Here is a new sentence to be scanned for sintax with NASDAQ.";

        public async void TestLiveScrap()
        {
            // string url = "http://cn.bing.com/search?q=hznp&go=Submit&qs=n&pq=hznp&sc=8-4&sp=-1&sk=&cvid=5C7A1AF09DF3490881B8B4B575229306&intlF=1&FORM=TIPEN1";
            string input = "aapl";
            //  string url = "http://cn.bing.com/search?q=" + input + "&go=Submit&qs=n&pq="+ input +"&sc=8-4&sp=-1&sk=&cvid=5C7A1AF09DF3490881B8B4B575229306&intlF=1&FORM=TIPEN1";
            //change the input value
          Mining.RunBrowserThread(input);          
            //Task.WaitAll(taskA);
            Console.WriteLine("scrapped apple");
            input = "hznp";
            //stat the tasks 
            Mining.RunBrowserThread(input);
                  
            Console.WriteLine("scrapped hznp");
            input = "gild";
            //stat the tasks  
           Mining.RunBrowserThread(input);
          
            Console.WriteLine("scrapped gild");
        }
    }
}
