using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockPredictor
{
    //this class cleans the text and returns letters
    class TextCleaner
    {
        //remove non letters
        public string removeNonLetters(HtmlNode node)
        {
            Regex rgx = new Regex("[^a-zA-Z!?.,-]");
            string str = rgx.Replace(node.InnerHtml, " ");          
            return str;
        }
        //replace the exclamation marks and question marks with full stops
        public string replaceMarks(string str)
        {
            str = str.Replace("!", ".").Replace("?", ".");
            return str;
        }
        //remove punctuation form the words
        public string removePunctuation(string str)
        {
            Regex rgx = new Regex("[^a-zA-Z]");
            string str2 = rgx.Replace(str, " ");

            return str2;
        }

        //remove punctuation form the words
        public string removePunctuationNotStops(string str)
        {
            Regex rgx = new Regex("[^a-zA-Z.]");
            string str2 = rgx.Replace(str, " ");

            return str2;
        }       
    }
}
