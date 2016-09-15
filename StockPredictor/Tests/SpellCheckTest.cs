using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Tests
{
    class SpellCheckTest
    {
        // Text for tagging
        string text = "A Part-Of-Speech Tagger (POS Tagger) is Gileard stock decreased a piece of software that reads text"
                    + "in some language and assigns parts of short sellers increased speech to each word (and other token),"
                    + " such as noun, verb, adjective, etc., although generally computational "
                    + "applications use more fine-grained POS tags like 'noun-plural'."
                    + "Here is a new sentence great news to be scanned for sintax with GILD."
                     + "Here is a new sentence terrible to be scanned for sintax with IBB."
                     + "Here is a new sentence to be crashed scanned for sintax with NASDAQ.";

        SpellCheck sc = new SpellCheck();
        public void isNamedEntityTest()
        {
           string text = "IBB";
            var q = sc.isNamedEntity(text);
        }

        public void testSpellCheckSpeed()
        {
            //start a stop watch to time method
            var watch = System.Diagnostics.Stopwatch.StartNew();
            SpellCheck sp = new SpellCheck();
            sp.spellCheckArticle(text);
            //stop the watch to time method
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("SpellCheck Article " + elapsedMs);
        }

        public void testSpellCheckSpeed2()
        {
            SpellCheck sc = new SpellCheck();
            List<string> sentences = new List<string>(text.Split('.'));
            Int64 totalTime = 0;
            foreach (string sentence in sentences)
            {

                //create an array list to store the words
                ArrayList cleanSentenceArray = new ArrayList();
                //split the sentences into words
                List<string> words = new List<string>(sentence.Split(' '));
                foreach (string word in words)
                {
                    //start a stop watch to time method
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    //check that word is spelt correctly
                    if (sc.spellChecker(word))
                    {
                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    totalTime += elapsedMs;
                }

            }//end first foreach
            Console.WriteLine("SpellCheck sentence " + totalTime);

        }
    }
}
