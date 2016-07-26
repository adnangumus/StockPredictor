using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockPredictor.Helpers
{
    class StemMethods
    {
        //stem all the lists
        //this method will stem the words in the positive and negative word and phrase lists
        //it will create a new lists to store them
        public void stemAll()
        {
            String fromFile = "Negative_phrases.txt";
            String toFile = "Negative_phrases_stemmed.txt";

            String fromFileP = "Positive_phrases.txt";
            String toFileP = "Positive_phrases_stemmed.txt";

            stemPhraseList(fromFile, toFile);
            stemPhraseList(fromFileP, toFileP);

            String fromFileWords = "negative_words.txt";
            String toFileWords = "Negative_stem_word_list.txt";

            String fromFileWordsP = "Positive_word_list.txt";
            String toFileWordsP = "Positive_stem_word_list.txt";

            stemList(fromFileWords, toFileWords);
            stemList(fromFileWordsP, toFileWordsP);
        }
        //This will stem the list of words and then write them on text file
        public void stemList(string fromFile, string saveFile)
        {
            fileReaderWriter frw = new fileReaderWriter();
            Porter2 stemmer = new Porter2();
            List<String> stemmedWords = new List<string>();
            ////this reads the positive words in the text document and returns them as a list.
            //string text = frw.readTextFile(fromFile);
            //string textNew = text.Replace("\t", "-");
            //Regex pattern = new Regex("[;,\t\r ]|[\n]{2}");
            //pattern.Replace(text, " ");             
            List<string> words = frw.getWordsFromList(fromFile);
            foreach (string word in words)
            {
                string stemmedWord = stemmer.stem(word);
                Console.WriteLine(stemmedWord);
                stemmedWords.Add(stemmedWord);
            }
            //sort the stemmed words alaphetically
            stemmedWords.Sort();
            //write to text file
            frw.writeTextFile(saveFile, stemmedWords);
        }

        //This will stem the list of words and then write them on text file
        public void stemPhraseList(string fromFile, string saveFile)
        {
            fileReaderWriter frw = new fileReaderWriter();
            Porter2 stemmer = new Porter2();
            List<String> stemmedPhrases = new List<string>();
            //get the phrases from the file reader class
            List<string> phrases = frw.getPharesFromList(fromFile);
            foreach (string phrase in phrases)
            {
                string stemmedPhrase = "";
                //split the phrases into word                 
                List<string> words = new List<string>(phrase.Split(' '));
                foreach (string word in words)
                {
                    string stemmedWord = stemmer.stem(word);
                    stemmedPhrase += stemmedWord + " ";
                }
                Console.WriteLine(stemmedPhrase);
                stemmedPhrases.Add(stemmedPhrase);
            }
            //sort the stemmed words alaphetically
            stemmedPhrases.Sort();
            //write to text file
            frw.writeTextFile(saveFile, stemmedPhrases);
        }

        //stem a sentence as a string
        public string stemSentence(string sentence)
        {
            Porter2 stemmer = new Porter2();
            string stemmedSentence = "";
            if (String.IsNullOrEmpty(sentence))
            {
                return stemmedSentence;
            }
            List<string> words = new List<string>(sentence.Split(' '));
            foreach (string word in words)
            {
                stemmedSentence += " " + stemmer.stem(word);
            }
            return stemmedSentence;
        }

    }
}