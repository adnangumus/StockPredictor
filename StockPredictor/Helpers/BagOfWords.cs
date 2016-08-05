
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class BagOfWords
    {
        ////get the sentences as list from a string. Check that words are spelt 
        ////correctly and then put the senteces back in to the lists
        //public List<string> getSentences(string s)
        //{
        //    StemMethods sm = new StemMethods();
        //    //SpellChecker class
        //    SpellCheck sc = new SpellCheck();
        //    List<string> cleanSentences = new List<string>();
        //    string cleanSentence = "";
        //    try
        //    {
        //        List<string> sentences = new List<string>(s.Split('.'));
        //        foreach (string sentence in sentences)
        //        {
        //            //spell check the words
        //            cleanSentence = sc.spellSentenceChecker(sentence);
        //            //stem the words and add to list
        //            cleanSentences.Add(sm.stemSentence(cleanSentence));
        //        }
        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); }
        //    return cleanSentences;
        //}

        //get the sentences as list from a string. Check that words are spelt 
        //correctly and then put the senteces back in to the lists. Words are stemmed. 
        //Count the amount of words and sentences processed
        public void processBagOfWords(string articles, string fileName)
        {
            Console.WriteLine("BagOfWords started");
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            Hashtable ht = new Hashtable();
            //start a stop watch to time method
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //integer to hold the sentnece count
            int sentenceCount = 0;
            //integer to hold word count
            int wordCount = 0;
            //get a list of all negative words
          //  List<String> negativeWords = new List<string>();
            //get a list of all positive words
          //  List<String> positiveWords = new List<string>();
            //store the sentences in a string
            string cleanSentenceString;
            //count positive and negative phrases and strong words
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;
            int strongPositivePhraseCount = 0;
            int strongNegativePhraseCount = 0;
            int negWordCount = 0;
            int posWordCount = 0;
            //stemmer methods
            Porter2 stemmer = new Porter2();
            //load class to remove stop words
            StopWords sw = new StopWords();
            TextCleaner tc = new TextCleaner();
          
     
            try
            {
                //change the marks into full stops
               articles = tc.replaceMarks(articles);
                List<string> sentences = new List<string>(articles.Split('.'));
                ParallelOptions po = new ParallelOptions();
                //Manage the MaxDegreeOfParallelism instead of .NET Managing this. We dont need 500 threads spawning for this.
                po.MaxDegreeOfParallelism = System.Environment.ProcessorCount * 2;
                try
                {
                    Parallel.ForEach(sentences, po, sentence =>
                    {
                        try
                        {
                            //process the sentences for named entites and return pos neg count and word count
                            ht = processSentences(sentence);
                            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
                            posWordCount += (int)ht["pw"];
                            negWordCount += (int)ht["nw"];
                            positivePhraseCount += (int)ht["pp"];
                            negativePhraseCount += (int)ht["np"];
                            wordCount += (int)ht["wc"];
                            sentenceCount += (int)ht["sc"];
                        }//end try
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    });//end parallel foreach  
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //count the negative and positive words
            // int negWordCount = negativeWords.Count();
            //  int posWordCount = positiveWords.Count();
            //stop the stop watch
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
           
            Console.WriteLine("Bag of words processing time : " + elapsedMs);
            Console.WriteLine("Words = " + wordCount + " Sentences = " + sentenceCount);
            Console.WriteLine("P W = " + posWordCount + " N W = " + negWordCount);         
            Console.WriteLine("P P = " + positivePhraseCount + " N P = " + negativePhraseCount);
            //calculate the precentages
            CalculatorMethods cm = new CalculatorMethods();
            int posWordPercentage = cm.getPositivePercentage(posWordCount, negWordCount);
            int negWordPercentage = cm.getNegativePercentage(posWordCount, negWordCount);
            int posPhrasePercentage = cm.getPositivePercentage(positivePhraseCount, negativePhraseCount);
            int negPhrasePercentage = cm.getNegativePercentage(positivePhraseCount, negativePhraseCount);
            Console.WriteLine("Percentage Positive = " + cm.getPositivePercentage(posWordCount, negWordCount) + "% " + "Negative percentage = " + cm.getNegativePercentage(posWordCount, negWordCount) + "% ");

            //out put information to text box
            Form1.Instance.AppendOutputText("\r\n");
            Form1.Instance.AppendOutputText("Bag of words processing time : " + elapsedMs + "\r\n");
            Form1.Instance.AppendOutputText("Words = " + wordCount + " Sentences = " + sentenceCount + "\r\n");
            Form1.Instance.AppendOutputText("P W = " + posWordCount + " N W = " + negWordCount + "\r\n");
            Form1.Instance.AppendOutputText("P P = " + positivePhraseCount + " N P = " + negativePhraseCount + "\r\n");
            Form1.Instance.AppendOutputText("Percentage Positive = " + cm.getPositivePercentage(posWordCount, negWordCount) + " % " + "Negative percentage = " + cm.getNegativePercentage(posWordCount, negWordCount) + " % " + "\r\n");
            //Form1.Instance.AppendOutputText(+"\r\n");
        //add the output data to an excel file
        ExcelMethods em = new ExcelMethods();
            //add the data to special excel file for only this specific out put for this stock
            em.savePredictorDataToExcel(fileName, "Bag", elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
          posWordPercentage, negWordPercentage,
           positivePhraseCount, negativePhraseCount,
           posPhrasePercentage, negPhrasePercentage);
        }//end method

        //process the sentences in the bag of words seperately to allow for multi threading
        private Hashtable processSentences(string sentence)
        {
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            Hashtable ht = new Hashtable();
            //integer to hold the sentnece count
            int sentenceCount = 0;
            //integer to hold word count
            int wordCount = 0;
            //count positive and negative phrases and strong words
            int negWordCount = 0;
            int posWordCount = 0;
            int strongPositiveWordCount = 0;
            int strongNegativeWordCount = 0;
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;
            //load class to remove stop words
            StopWords sw = new StopWords();

            try {
               string cleanSentenceString = "";
                //count the sentences while processing text
                sentenceCount++;
                //create an array list to store the words
                ArrayList cleanSentenceArray = new ArrayList();
                //intiate the term frequency object
                TermFrequency tf = new TermFrequency();
                //load stemmer class
                Porter2 stemmer = new Porter2();
                //split the sentences into words
                List<string> words = new List<string>(sentence.Split(' '));
                foreach (string word in words)
                {
                    if (!sw.isStopWord(word))
                    {
                        //add another word to the count
                        wordCount++;
                        //stem the words
                        string stemmedWord = (stemmer.stem(word));
                        // check if the word is a positive or negative word
                        posWordCount += tf.isPositiveWord(stemmedWord);
                        negWordCount += tf.isNegativeWord(stemmedWord);
                        //stem the word and add it to the array
                        //  cleanSentenceArray.add(stemmedWord);
                        //add the word to a string for processign phrases
                        cleanSentenceString += stemmedWord + " ";
                    }//end if
                }//end foreach
                 // if(cleanSentenceArray.size() > 0) {
                 //count the positive phrases
                positivePhraseCount += tf.containsPositivePhrase(cleanSentenceString);
                //count the positive phrases
                negativePhraseCount += tf.containsNegativePhrase(cleanSentenceString);
                //add the sentence arraylist to the list
                // cleanSentences.Add(cleanSentenceArray);
                //  }//end if                             
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            ht.Add("pw", posWordCount);
            ht.Add("nw", negWordCount);
            ht.Add("pp", positivePhraseCount);
            ht.Add("np", negativePhraseCount);
            ht.Add("wc", wordCount);
            ht.Add("sc", sentenceCount);
            return ht;
        }
    }
}
