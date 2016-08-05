﻿using System;
using System.Collections.Generic;

using System.Windows.Forms;
using StockPredictor.Helpers;
using System.IO;
using System.Linq;

namespace StockPredictor
{
    class SpellCheck
    {

        //spell check articles
        public string spellCheckArticle(string article)
        {
            TextCleaner tc = new TextCleaner();
            fileReaderWriter frw = new fileReaderWriter();
            string returnArticle = "";
            //load dictionary
            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();
            oDict.DictionaryFile = Path.Combine(frw.GetAppFolder(), @"packages\NetSpell.2.1.7\dic\en-US.dic");
            //load and initialize the dictionary 
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
            //spell check words for returning
            string spellChecked = "";
            try {
                //break the string into its words
                List<string> sentences = new List<string>(article.Split('.'));
                foreach(string sentence in sentences)
                {
                    //clean spellchecked
                    spellChecked = "";
                //remove punctuation and replace with a space and a full stop
                article = tc.removePunctuation(article);
            //break the string into its words
            List<string> words = new List<string>(sentence.Split(' '));
            //iterate through the word list returned from the search
            foreach (string word in words)
            {
                if (oSpell.TestWord(word) || isNamedEntity(word))
                {
                    spellChecked += word + " ";
                    // Console.WriteLine(word);
                }
            }
            //add the spell check article to the list with a full stop
                    returnArticle += spellChecked + " . ";
                }
            }//end try
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            return returnArticle;
        }

        //spell check sentences
        public string spellCheckSentence(string sentence)
        {
            TextCleaner tc = new TextCleaner();
            fileReaderWriter frw = new fileReaderWriter();
            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();

            oDict.DictionaryFile = Path.Combine(frw.GetAppFolder(), @"packages\NetSpell.2.1.7\dic\en-US.dic");
            //load and initialize the dictionary 
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
            //spell check words for returning
            string speltchecked = "";
            //remove punctuation and replace with a space and a full stop
            sentence = tc.removePunctuationNotStops(sentence);
            sentence.Replace(".", " . ");
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
            oSpell.Dispose();
            oDict.Dispose();
            return speltchecked;
        }
        //check if an individual work is spelled correctly
        public bool spellChecker(string word)
        {
            fileReaderWriter frw = new fileReaderWriter();

            NetSpell.SpellChecker.Dictionary.WordDictionary oDict = new NetSpell.SpellChecker.Dictionary.WordDictionary();

            oDict.DictionaryFile = Path.Combine(frw.GetAppFolder(), @"packages\NetSpell.2.1.7\dic\en-US.dic");
            //load and initialize the dictionary 
            oDict.Initialize();
            NetSpell.SpellChecker.Spelling oSpell = new NetSpell.SpellChecker.Spelling();
            oSpell.Dictionary = oDict;
            if (oSpell.TestWord(word) || isNamedEntity(word))
            {
                //dispose of the dictionary objects
                oSpell.Dispose();
                oDict.Dispose();
                return true;
            }
            //dispose of the dictionary objects
            oSpell.Dispose();
            oDict.Dispose();
            return false;
        }

        //check if the sentence contains any of the named entites in the text file
        public bool isNamedEntity(string word)
        {
            //file writer reader class
            fileReaderWriter frw = new fileReaderWriter();
            //load pos tagger
            PosTagger pt = new PosTagger();

            if (word.Contains("_"))
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
        //check if the sentence contains any of the named entites in the text file
        public bool hasNamedEntity(string sentence)
        {
            //file writer reader class
            fileReaderWriter frw = new fileReaderWriter();
            // Console.WriteLine("cleanword : " + cleanWord);
            //get named entites form a list
            List<string> terms = frw.getWordsFromList("Named_Entities.txt");
            //check if the named entites match the word
            foreach (string term in terms)
            {
                if (sentence.ToLower().Contains(term.ToLower())) { return true; }
            }
            return false;

        }

    }
}
