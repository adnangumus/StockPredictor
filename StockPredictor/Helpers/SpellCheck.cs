
using System;
using System.Collections.Generic;

using System.Windows.Forms;
using StockPredictor.Helpers;
using System.IO;
using System.Linq;

namespace StockPredictor
{
    class SpellCheck
    {
 
        //spell check words
        public string spellCheckArticle(string sentence)
        {
            //reads and write files as well as returns urls
            fileReaderWriter frw = new fileReaderWriter();
            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();

            oDict.DictionaryFile = Path.Combine(frw.GetAppFolder(), @"packages\NetSpell.2.1.7\dic\en-US.dic");
            //load and initialize the dictionary 
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
            //spell check words for returning
            string speltchecked = "";
            //break the string into its words
            List<string> words = new List<string>(sentence.Split(' '));
            //iterate through the word list returned from the search
            foreach (string word in words)
            {
                if (oSpell.TestWord(word) || isNamedEntity(word))
                {
                    speltchecked += word + " ";
                    // Console.WriteLine(word);
                }

            }
            return speltchecked;
        }

        //check if an individual work is spelled correctly
        public bool spellChecker(string word)
        {
            //reads and write files as well as returns urls
            fileReaderWriter frw = new fileReaderWriter();
            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();

            oDict.DictionaryFile = Path.Combine(frw.GetAppFolder(), @"packages\NetSpell.2.1.7\dic\en-US.dic");
            //load and initialize the dictionary 
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
            if (oSpell.TestWord(word) || isNamedEntity(word))
            {
                return true;
            }
            return false;
        }

        //check if the sentence contains any of the named entites in the text file
        public bool isNamedEntity(string word)
        {
            //file writer reader class
            fileReaderWriter frw = new fileReaderWriter();
            //load pos tagger
            PosTagger pt = new PosTagger();
           
                    if (word.Contains("/"))
                    {
                        word = pt.removeTag(word);
                    }                           
                // Console.WriteLine("cleanword : " + cleanWord);
                //get named entites form a list
                List<string> terms = frw.getWordsFromList("Named_Entities.txt");
            //check if the named entites match the word
            foreach (string term in terms)
            {
                if (word.ToLower() == term.ToLower()) { return true; }
            }
            return false;

        }

    }
}
