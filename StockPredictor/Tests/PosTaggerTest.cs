using java.util;
using edu.stanford.nlp.ling;
using StockPredictor.Helpers;
using Console = System.Console;
using System.IO;
using System.Windows.Forms;
using edu.stanford.nlp.tagger.maxent;
using System.Collections.Generic;

namespace StockPredictor.Tests
{
    class PosTaggerTest
    {
        PosTagger pt = new PosTagger();
        // Text for tagging
       string text = "A Part-Of-Speech Tagger (POS Tagger) is Gileard a piece of software that reads text"
                   + "in some language and assigns parts of speech to each word (and other token),"
                   + " such as noun, verb, adjective, etc., although generally computational "
                   + "applications use more fine-grained POS tags like 'noun-plural'."
                   + "Here is a new sentence to be scanned for sintax with GILD."
                    + "Here is a new sentence to be scanned for sintax with IBB."
            +"Now for sibbling checking for named entites."
                    + "Here is a new sentence  to be scanned for sintax with NASDAQ.";

        string text2 = "gild. Ibb. Nasdaq. Hlep.";
        
        // this method tests that the tagger is working 
        public void testTagger()
        {
            fileReaderWriter frw = new fileReaderWriter();
            var jarRoot = Path.Combine(frw.GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
            Console.WriteLine(jarRoot.ToString());
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-bidirectional-distsim.tagger");

         
            // Put a space before full stops
            text = text.Replace(".", " .");

            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
            foreach (ArrayList sentence in sentences)
            {
                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine(Sentence.listToString(taggedSentence, false));
            }
        }

        public void getNamedEntitiesTest()
        {
            pt.getNameEntites(text);
        }
        //test the POS tagger which extracts named entites
        public void posTaggerNamedTest()
        {
           // Counter counter = new Counter();
            List<ArrayList> collection = new List<ArrayList>();
            collection = pt.getNameEntites(text);
          //  int sentences = counter.countSentenceCollection(collection);
         //   int words = counter.countWordCollection(collection);
         //   Console.WriteLine("There are " + sentences + " sentences in the named test");
         //   Console.WriteLine("There are " + words + " words in the named test");
        }
        //test the Tagger that extracts noun phrases
        public void posTaggerNounTest()
        {
          //  Counter counter = new Counter();
            List<ArrayList> collection = new List<ArrayList>();
            collection = pt.getNounPhrase(text);
          //  int sentences = counter.countSentenceCollection(collection);
          //  int words = counter.countWordCollection(collection);
          //  Console.WriteLine("There are " + sentences + " sentences in the noun test");
          //  Console.WriteLine("There are " + words + " words in the noun test");
        }

    }
}
