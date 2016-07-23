using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Tests
{
    class BagOfWordsTest
    {
        // Text for tagging
        string text = "Is Gileard a piece of software that reads text or has it increased earnings after a major acquisition?"
                   + "In some language and assigns parts of speech to each word (and other token),"
                   + " such as noun, verb, adjective, etc., although generally computational and disastrous after the credit crunch."
                   + "applications use more fine-grained POS tags like 'noun-plural'."
                   + "Here is a new sentence to be scanned for sintax with GILD."
            + "Gileard best performing stock this year, with a significant decrease in short interest."
                    + "Here is a new sentence to be scanned for sintax with IBB."
         + "Here is a new sentence to be scanned for sintax with NASDAQ.";

        string text2 = "Is Gileard a piece of software that reads text or has it increased earnings after a major acquisition?"
                  + "In some language and assigns parts of speech to each word (and other token), it could be disastrous";
        //public void getSentenceTest()
        //{
        //    BagOfWords bg = new BagOfWords();
        //    Mining miner = new Mining();
        //    List<string> ms = new List<string>();
        //    ms = bg.getSentences(text);
        //    foreach (string m in ms)
        //    {
        //        Console.WriteLine(m);
        //    }
        //}

        public void getSentenceBagArrayTest()
        {
            BagOfWords bg = new BagOfWords();
          
            Mining miner = new Mining();
          
            bg.processBagOfWords(text2, "exampleExcel1");
           // int sentences = counter.countSentenceCollection(collection);
           // int words = counter.countWordCollection(collection);
           // Console.WriteLine("There are " + sentences + " sentences in the bag test");
            //Console.WriteLine("There are " + words + " words in the bag test");
        }

        public void processBagOfWordsTest()
        {
            BagOfWords bg = new BagOfWords();
            bg.processBagOfWords(text2, "exampleExcel1");
        }
      
    }
}
