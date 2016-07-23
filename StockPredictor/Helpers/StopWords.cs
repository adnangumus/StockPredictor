using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using StockPredictor.Helpers;

namespace StockPredictor
{
    class StopWords
    {
       

        //remove stop words from a string
        public string removeStopWords(string str)
        {
            fileReaderWriter frw = new fileReaderWriter();
            string stopWordsRemoved = "";
            List<string> stopWords = frw.getWordsFromList("stop_words.txt");
            //break the string into its words
            List<string> words = new List<string>(str.Split(' '));

            //iterate through the word list returned from the search
            foreach (string word in words)
            {

                if (!stopWords.Contains(word))
                {
                    stopWordsRemoved += word + " ";
                   // Console.WriteLine(word);
                }

            }

            return stopWordsRemoved;
        }

        //check if a word is a top word
        public bool isStopWord(string word)
        {
            fileReaderWriter frw = new fileReaderWriter();
            List<string> stopWords = frw.getWordsFromList("stop_words.txt");
            if (stopWords.Contains(word.ToLower()))
            {
                return true;
            }
            return false;
        }

       

    }
}
