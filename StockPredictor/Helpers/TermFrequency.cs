using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class TermFrequency
    {
        public int isNegativeWord(string word)
        {
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get terms form a list stored in text document
            List<string> terms = frw.getWordsFromList("Negative_stem_word_list.txt");
            foreach (string term in terms)
            {
                if (word.ToLower() == term.ToLower() && term != "" && term != " ") {
                    //Console.WriteLine("Negative word : " + term);
                    return 1;
                }
            }
            return 0;
        }
             public int isStrongNegativeWord(string word)
        {
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get terms form a list stored in text document
            List<string> terms = frw.getWordsFromList("Strong_Negative_stem_word_list.txt");
            foreach (string term in terms)
            {
                if (word.ToLower() == term.ToLower() && term != "" && term != " ") { return 1; }
            }
            return 0;
        }
        //check if a word corresponds to the positive word list and return a value of 1 or 0
        public int isPositiveWord(string word)
        {
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get named entites form a list stroed in a text document
            List<string> terms = frw.getWordsFromList("Positive_stem_word_list.txt");
            foreach (string term in terms)
            {
                if (word.ToLower() == term.ToLower() && term != "" && term != " ")
                {
                    //Console.WriteLine("Positive word : " + term);
                    return 1;                  
                }
            }
            return 0;
        }
        //check if a word corresponds to the psoitive word list
        public int isStrongPositiveWord(string word)
        {
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get named entites form a list stroed in a text document
            List<string> terms = frw.getWordsFromList("Strong_Positive_stem_word_list.txt");
            foreach(string term in terms)
            {
                if (word.ToLower() == term.ToLower() && term != "" && term != " ") { return 1; }
            }        
            return 0;
        }
        //check if a sentence contains a phrase
        public int containsPositivePhrase(string sentence)
        {
            int count = 0;
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get phrases form a list stroed in a text document
            List<string> terms = frw.getPharesFromList("Positive_phrases_stemmed.txt");
            foreach (string term in terms)
            {
                //check that phrase is in the list and that there isn't a false positive
                if (sentence.Contains(term) && term != "" && term != " ")
                {
                    count ++;
                    //Console.WriteLine("P term : " + term);
                }
            }
            return count;
        }

        //check if a sentence contains a phrase
        public int containsNegativePhrase(string sentence)
        {
            int count = 0;
            //initiate class to read files
            fileReaderWriter frw = new fileReaderWriter();
            //get phrases form a list stroed in a text document
            List<string> terms = frw.getPharesFromList("Negative_phrases_stemmed.txt");
            foreach (string term in terms)
            {
                //check that phrase is in the list and that there isn't a false positive
                if (sentence.Contains(term) && term != "" && term != " ")
                {
                    count++;
                }
            }
            return count;
        }
    }
}
