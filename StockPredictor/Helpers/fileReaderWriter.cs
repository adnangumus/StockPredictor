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
    class fileReaderWriter
    {
        // get the base folder for the project
        public string GetAppFolder()
        {
            string path = "";
            try
            {
                return Path.GetDirectoryName(Application.ExecutablePath).Replace(@"StockPredictor\bin\Debug", string.Empty);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return path;
        }

        public string readTextFile(string fileName)
        {
            string text = "";
            try {
                text = System.IO.File.ReadAllText(Path.Combine(GetAppFolder(), @"packages\Words\" + fileName));
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            return text;
        }
        //write a text file
        public void writeTextFile(string saveFile, List<String> data)
        {
            try
            {
                System.IO.File.WriteAllLines(Path.Combine(GetAppFolder(), @"packages\Words\" + saveFile), data);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
    

        //getwords from the text document
        public List<string> getWordsFromList(string fileName)
        {
            //this reads the positive words in the text document and returns them as a list.
            string text = readTextFile(fileName);
            string textNew = text.Replace("\t", " ");
            Regex pattern = new Regex("[;,\t\r\n ]|[\n]{2}");
            textNew = pattern.Replace(text, " ");
            //replace the double space with a single space
            text = textNew.Replace("  ", " ");
            List<string> words = new List<string>(text.Split(' '));
            //foreach(string word in words)
            //{
            //    Console.WriteLine(word);
            //}
            return words;
        }

        //getwords from the text document
        public List<string> getPharesFromList(string fileName)
        {                  
            //this reads the positive words in the text document and returns them as a list.
            string text = readTextFile(fileName);
            Regex pattern = new Regex("[;,\r\n]");
            string textNew = pattern.Replace(text, "-");
            //split the document into phrases using two dashes. This is because the regex puts two dashes into the gaps between sentences 
            text = textNew.Replace("--","-");                  
            List<string> phrases = new List<string>(text.Split('-'));           
            return phrases;
        }
    }
}
