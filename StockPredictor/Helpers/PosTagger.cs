using java.util;
using edu.stanford.nlp.ling;

using Console = System.Console;
using System.IO;
using System.Windows.Forms;
using edu.stanford.nlp.tagger.maxent;
using System.Collections.Generic;
using StockPredictor.Helpers;

namespace StockPredictor
{
    class PosTagger

    {
       
        
        // get the base folder for the project
        public static string GetAppFolder()
        {
            return Path.GetDirectoryName(Application.ExecutablePath).Replace(@"TextMiningPractice\bin\Debug", string.Empty);
        }
       

        //extract the noun phrases form the article. Return an array list of noun phrase sentences
        public List<ArrayList> getNounPhrase(string article)
        {
            
            fileReaderWriter frw = new fileReaderWriter();
            
            List<ArrayList> nounPhrases = new List<ArrayList>();
            ArrayList nounSentence = new ArrayList();

            var jarRoot = Path.Combine(GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
            Console.WriteLine(jarRoot.ToString());
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-bidirectional-distsim.tagger");

            // Put a space before full stops. This is so the tagger can read the full stops
            var text = article.Replace(".", " . "); 
           
            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
            foreach (ArrayList sentence in sentences)
            {
                //clear the arraylist at the start of each loop
                nounSentence.clear();

                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine("sentences wrote on new lines");
                Console.WriteLine(Sentence.listToString(taggedSentence, false));
                //convert the tagged labels to strings and then find the noun phrases.
                string sentenceString = Sentence.listToString(taggedSentence, false);
                //check that the sentence string contains only one sentence
                Console.WriteLine(sentenceString);
                List<string> words = new List<string>(sentenceString.Split(' '));
                //extract the words from the sentences
                foreach (string word in words)
                    {
                    if (isNounPhrase(word))
                    {
                        string cleanWord = removeTag(word);
                        // Console.WriteLine(cleanWord);
                        if (cleanWord != "label")
                        {
                            nounSentence.add(cleanWord.ToLower());
                        }
                    }//end if
                }//end foreach
                //add the sentences to the return variable
                nounPhrases.Add(nounSentence);
                Console.WriteLine(nounSentence.toString());
            }
            return nounPhrases;
        }//end class

        //extract sentences with named entities form the article. Return an array list of noun phrase sentences
        public List<ArrayList> getNameEntites(string article)
        {
            //load spell checker
            SpellCheck sc = new SpellCheck();
            //load the porter stemmer
            Porter2 stemmer = new Porter2();
            List<ArrayList> nounPhrases = new List<ArrayList>();
            ArrayList nounSentence = new ArrayList();

            var jarRoot = Path.Combine(GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
            Console.WriteLine(jarRoot.ToString());
            var modelsDirectory = jarRoot + @"\models";
            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\english-bidirectional-distsim.tagger");

            // Put a space before full stops. This is so the tagger can read the full stops
            var text = article.Replace(".", " . ");

            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(text)).toArray();
            foreach (ArrayList sentence in sentences)
            {
                //clear the arraylist at the start of each loop
                nounSentence.clear();

                var taggedSentence = tagger.tagSentence(sentence);
                Console.WriteLine("sentences wrote on new lines");
                Console.WriteLine(Sentence.listToString(taggedSentence, false));
                //convert the tagged labels to strings and then find the noun phrases.
                string sentenceString = Sentence.listToString(taggedSentence, false);

                //use this varible to indicate if a sentence contains a named entity
                bool hasNamedEntity = false;
                List<string> words = new List<string>(sentenceString.Split(' '));
                //extract the words from the sentences
                foreach (string word in words)
                {
                    if (word.Contains("NNP") || sc.isNamedEntity(word))
                    {
                        hasNamedEntity = true;
                    }//end if named entity
                     //check if the words are noun phrases
                    if (isNounPhrase(word))
                        {
                            string cleanWord = removeTag(word);
                            // Console.WriteLine(cleanWord);
                            if (cleanWord != "label" && sc.spellChecker(cleanWord))
                            {
                                //stem the word before adding it
                                nounSentence.add(stemmer.stem(cleanWord.ToLower()));
                            }
                        }//end if
                  
                }//end foreach
                //check if the sentences has a named entity
                if(hasNamedEntity) { 
                 //add the sentences to the return variable
                nounPhrases.Add(nounSentence);
                Console.WriteLine(nounSentence.toString());
                }
            }         
            return nounPhrases;
        }//end method 

       
        //check if a word is taged as part of a noun phrase
        private bool isNounPhrase(string word)
        {
            bool isNoun = false;
            //check if the words are noun phrases
            if (word.Contains("NN") || word.Contains("VB") || word.Contains("JJ") || word.Contains("RB"))
            {
                isNoun = true;
                return isNoun;
            }
            return isNoun;
        }
        //remove the tags from the POS tagger
        public string removeTag(string word)
        {
            //remove the labels
            List<string> cleanWords = new List<string>(word.Split('/'));
            foreach (string cleanWord in cleanWords)
            {
                if (cleanWord.Contains("NN") || cleanWord.Contains("VB") || cleanWord.Contains("JJ") || cleanWord.Contains("RB"))
                {//do nothing  
                    return "label";                  
                }
                //add the actual words to the nounsentence
                else
                {
                    // Console.WriteLine(cleanWord);
                    return cleanWord.ToLower();
                }//end else  
            }//end foreach
            return "label";
        }
    }//end class
}//end name space