using java.util;
using edu.stanford.nlp.ling;
using StockPredictor.Helpers;
using Console = System.Console;
using System.IO;
using System.Windows.Forms;
using edu.stanford.nlp.tagger.maxent;
using System.Collections.Generic;
using System;

namespace StockPredictor.Tests
{
    class PosTaggerTest
    {
        PosTagger pt = new PosTagger();
        string text = "Is Gileard a piece of software that reads text or has it increased earnings after a major acquisition and is the best performing stock?"
                   + "In some language and assigns parts of speech to each word (and other token),"
                   + " such as noun, verb, adjective, etc., although generally computational and disastrous after the credit crunch."
                   + "applications use more fine-grained POS tags like 'noun-plural'."
                   + "Here is a new sentence to decrease earnings and be scanned high risk for sintax with GILD . "
            + " Gileard best performing stock this year, with a significant decrease in short interest."
                    + "Here is a new sentence to be scanned for sintax with IBB."
         + "Here is a new sentence to be scanned for sintax with NASDAQ.";

        string text2 = "gild. Ibb. Nasdaq. Hlep.";
        // get the base folder for the project
        public static string GetAppFolder()
        {
            return Path.GetDirectoryName(Application.ExecutablePath).Replace(@"TextMiningPractice\bin\Debug", string.Empty);
        }

        // this method tests that the tagger is working 
        public void testTagger()
        {
            var jarRoot = Path.Combine(GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
            Console.WriteLine(jarRoot.ToString());
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-bidirectional-distsim.tagger");

         
            // Put a space before full stops
            text = text.Replace(".", " .").Replace("!"," . ").Replace("?"," . ");

            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
            foreach (ArrayList sentence in sentences)
            {
                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine(Sentence.listToString(taggedSentence, false));
            }
        }

        public void getNamedEntitiesTest()
        {
            pt.nameEntites(text, "exampleExcel");
        }
        //test the POS tagger which extracts named entites
        public void posTaggerNamedTest()
        {
           pt.nameEntites(text, "exampleExcel");

        }
        //test the Tagger that extracts noun phrases
        public void posTaggerNounTest()
        {
           string str = pt.tagArticles(text);
           pt.nounPhrase(str, "exampleExcel");

        }

        public void testNounNamed()
        {
            pt.processNamedNoun(text, "exampleExcel1");
        }

        public string tagArticle(string str)
        {
           str = text;
            str = str.Replace(".", " . ");
            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            fileReaderWriter frw = new fileReaderWriter();
            try
            {
                var jarRoot = Path.Combine(frw.GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
                Console.WriteLine(jarRoot.ToString());
                var modelsDirectory = jarRoot + @"\models";
                // Loading POS Tagger
                var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");
                var taggedSentence = tagger.tagString(str);

                Console.Write("all articles have been tagged");
                watch2.Stop();
                var elapsedMs21 = watch2.ElapsedMilliseconds;
                Console.WriteLine("Overall Time " + elapsedMs21);
                return taggedSentence;

            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            watch2.Stop();
            var elapsedMs2 = watch2.ElapsedMilliseconds;
            Console.WriteLine("Overall Time " + elapsedMs2);
            return str;
        }


    }
}
