using edu.stanford.nlp.ling;
using System.Collections;
using Console = System.Console;
using System.IO;
using System.Windows.Forms;
using edu.stanford.nlp.tagger.maxent;
using System.Collections.Generic;
using StockPredictor.Helpers;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace StockPredictor
{
    class NamedNoun

    {
        //load the term frequency class with the methods for measuring the frequency of positive and negatiive terms
        private static TermFrequency termFrequency = new TermFrequency();
        //load the porter stemmer
        private static Porter2 stemmer = new Porter2();
        private static fileReaderWriter frw = new fileReaderWriter();
        //load spell checker
        private static SpellCheck sc = new SpellCheck();
        //this class contains methods to remove the stop words 
        private static StopWords sw = new StopWords();

        //process named and noun phrases together
        public List<Hashtable> processNamedNoun(string articles, string fileName, bool dontSave)
        {
            ExcelMethods em = new ExcelMethods();

            List<Hashtable> hts = new List<Hashtable>();
            Hashtable nounHT = new Hashtable();
            Hashtable namedHT = new Hashtable();
            //tag the articles first
            string taggedArticles = tagArticles(articles);
            //process the named entites
            Task taskA = Task.Run(() => nounHT = nounPhrase(taggedArticles, fileName, dontSave));
            //process the noun phrases
            Task taskB = new Task(() => namedHT = namedEntites(taggedArticles, fileName, dontSave));
            //run the tasks and wait. Get awaiter us used because the threads are using a static instance
            taskB.RunSynchronously();
            Task.WaitAll(taskA, taskB);
            //add the data to the hashtable list
            hts.Add(nounHT);
            hts.Add(namedHT);
            return hts;

        }
        //a variable to store the time it takes to take the sentences
        private long tagTime = new long();
        //tag the artilces
        public string tagArticles(string str)
        {
            Console.WriteLine("tag article started");
            //start the stop watch for measuing speed of method
            Stopwatch watchTag = new Stopwatch();
            watchTag.Start();
            //put a space between the full stops so as the tagger will recognise them
            str = str.Replace(".", " . ");

           
            try
            {
                var jarRoot = Path.Combine(frw.GetAppFolder(), @"packages\stanford-postagger-2015-12-09");
                Console.WriteLine(jarRoot.ToString());
                var modelsDirectory = jarRoot + @"\models";
                // Loading POS Tagger
                var tagger = new MaxentTagger(modelsDirectory + @"\english-left3words-distsim.tagger");
                var taggedSentence = tagger.tagString(str);

                Console.WriteLine("all articles have been tagged :");
                watchTag.Stop();
                tagTime = watchTag.ElapsedMilliseconds;
                //return the sentences with the labels
                return taggedSentence;

            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            watchTag.Stop();
            tagTime = watchTag.ElapsedMilliseconds;
            return str;
        }

        //extract the noun phrases form the article. Return an array list of noun phrase sentences
        public Hashtable nounPhrase(string article, string fileName, bool dontSave)
        {

            //start a stop watch to time method
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            Hashtable ht = new Hashtable();
            //integer to hold the sentnece count
            int sentenceCount = 0;
            //integer to hold word count
            int wordCount = 0;
            //count positive and negative phrases and strong words
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;
            int strongPositiveWordCount = 0;
            int strongNegativeWordCount = 0;
            int negWordCount = 0;
            int posWordCount = 0;
          
           


            // Put a space before full stops. This is so the tagger can read the full stops
            var text = article.Replace("._.", " . ");
            List<string> sentences = new List<string>(text.Split('.'));
            try
            {
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
                            ht = processNounPhrases(sentence);
                            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
                            posWordCount += (int)ht["pw"];
                            negWordCount += (int)ht["nw"];
                            positivePhraseCount += (int)ht["pp"];
                            negativePhraseCount += (int)ht["np"];
                            wordCount += (int)ht["wc"];
                            sentenceCount += (int)ht["sc"];
                        }//end try
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                    });
                }//end try
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //stop the watch timing the processes
            watch.Stop();
            //add the time from the tagging method to find the total time for the method
            var elapsedMs = watch.ElapsedMilliseconds + tagTime;
            Console.WriteLine("Noun phrases process time MS : " + elapsedMs);
            Console.WriteLine("Words = " + wordCount + " Sentences = " + sentenceCount);
            Console.WriteLine("P W = " + posWordCount + " N W = " + negWordCount);
            Console.WriteLine("P P = " + positivePhraseCount + " N P = " + negativePhraseCount);
            //calculate the precentages
            CalculatorMethods cm = new CalculatorMethods();
            int posWordPercentage = cm.getPositivePercentage(posWordCount, negWordCount);
            int negWordPercentage = cm.getNegativePercentage(posWordCount, negWordCount);
            int posPhrasePercentage = cm.getPositivePercentage(positivePhraseCount, negativePhraseCount);
            int negPhrasePercentage = cm.getNegativePercentage(positivePhraseCount, negativePhraseCount);
            int totalScore = cm.getTotalSentimentScore(posWordCount, positivePhraseCount, negWordCount, negativePhraseCount);
            Console.WriteLine("Percantage of Words Positive = " + posWordPercentage + "% " + "Negative word percentage = " + negWordPercentage + "% ");
            Console.WriteLine("Total Score : " + totalScore);

            //out put information to text box
            Form1.Instance.AppendOutputText("\r\n" + fileName + "\r\n" +
                "Noun phrase method :" + "\r\n" +
               "Percantage of Words Positive = " + posWordPercentage + " % " + "\r\n" +
                 "Percentage of words Negative = " + negWordPercentage + " % " + "\r\n" +
                 "Percentage of Phrases Postive = " + posPhrasePercentage + " % " + "\r\n" +
                 "Percentage of Phrases Negative = " + negPhrasePercentage + " % " + "\r\n" +
                "Words = " + wordCount + " Sentences = " + sentenceCount + "\r\n" +
                 "Positive words detected = " + posWordCount + "\r\n" + "Negative words detected  = " + negWordCount + "\r\n" +
                "Postive phrases detected = " + positivePhraseCount + "\r\n" + "Negative phrases dectected = " + negativePhraseCount + "\r\n" +
                 "Total Score : " + totalScore + "\r\n" +
                fileName + "-Noun : processing time : " + elapsedMs + "\r\n"
                );

            Hashtable returnTable = new Hashtable();
            returnTable.Add("pw", posWordCount);
            returnTable.Add("nw", negWordCount);
            returnTable.Add("pp", positivePhraseCount);
            returnTable.Add("np", negativePhraseCount);
            returnTable.Add("wc", wordCount);
            returnTable.Add("sc", sentenceCount);
            returnTable.Add("pwp", posWordPercentage);
            returnTable.Add("nwp", negWordPercentage);
            returnTable.Add("npp", negPhrasePercentage);
            returnTable.Add("ppp", posPhrasePercentage);
            returnTable.Add("total", totalScore);
            returnTable.Add("tt", elapsedMs.ToString());
            returnTable.Add("method", "Noun");


            return returnTable;
        }//end class


        private Hashtable processNounPhrases(string sentence)
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

            //clear the snetence string at the begining of each loop
            string nounSentenceString = "";
            try
            {
                List<string> words = new List<string>(sentence.Split(' '));
                //extract the words from the sentences
                foreach (string word in words)
                {
                    //check if the word is an important word
                    if (isNounPhrase(word))
                    {
                        string cleanWord = removeTag(word);
                        // Console.WriteLine(cleanWord);
                        if (cleanWord != "label")
                        {
                            //stem the word
                            cleanWord = stemmer.stem(cleanWord.ToLower());
                            //check if the word appears on the positive or negatice keyword lists                            
                            posWordCount += termFrequency.isPositiveWord(cleanWord);
                            negWordCount += termFrequency.isNegativeWord(cleanWord);
                            nounSentenceString += cleanWord + " ";
                            //count the words
                            wordCount++;
                        }
                    }//end if
                }//end foreach
                //count the senetences
                sentenceCount++;
                //check if the sentence contians a phrase
                negativePhraseCount += termFrequency.containsNegativePhrase(nounSentenceString);
                positivePhraseCount += termFrequency.containsPositivePhrase(nounSentenceString);

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

        //--------------------------------------------named entites---------------------------------------------------------------///
        //extract sentences with named entities form the article. Return an array list of noun phrase sentences
        public Hashtable namedEntites(string article, string fileName, bool dontSave)
        {
            //start a stop watch to time method
            var watch = System.Diagnostics.Stopwatch.StartNew();
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            Hashtable ht = new Hashtable();
            //integer to hold the sentnece count
            int sentenceCount = 0;
            //integer to hold word count
            int wordCount = 0;
            //count positive and negative phrases and strong words
            int positivePhraseCount = 0;
            int negativePhraseCount = 0;
            int strongPositiveWordCount = 0;
            int strongNegativeWordCount = 0;
            int negWordCount = 0;
            int posWordCount = 0;
          
            List<ArrayList> nounPhrases = new List<ArrayList>();
            ArrayList namedSentence = new ArrayList();

            try
            {

                // Put a space before full stops. This is so the tagger can read the full stops
                var text = article.Replace("._.", " . ");
                List<string> sentences = new List<string>(text.Split('.'));
                try
                {
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
                                ht = processNamedEntities(sentence);
                                //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
                                posWordCount += (int)ht["pw"];
                                negWordCount += (int)ht["nw"];
                                positivePhraseCount += (int)ht["pp"];
                                negativePhraseCount += (int)ht["np"];
                                wordCount += (int)ht["wc"];
                                sentenceCount += (int)ht["sc"];
                            }//end try
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        });
                    }//end try
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }//end try
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //stop the watch timing the processes
            watch.Stop();
            //add the time from the tagging method to find the total time for the method
            var elapsedMs = watch.ElapsedMilliseconds + tagTime;
            Console.WriteLine("Named Entites process time MS : " + elapsedMs);
            Console.WriteLine("Words = " + wordCount + " Sentences = " + sentenceCount);
            Console.WriteLine("P W = " + posWordCount + " N W = " + negWordCount);
            Console.WriteLine("P P = " + positivePhraseCount + " N P = " + negativePhraseCount);
            //calculate the precentages
            CalculatorMethods cm = new CalculatorMethods();
            int posWordPercentage = cm.getPositivePercentage(posWordCount, negWordCount);
            int negWordPercentage = cm.getNegativePercentage(posWordCount, negWordCount);
            int posPhrasePercentage = cm.getPositivePercentage(positivePhraseCount, negativePhraseCount);
            int negPhrasePercentage = cm.getNegativePercentage(positivePhraseCount, negativePhraseCount);
            int totalScore = cm.getTotalSentimentScore(posWordCount, positivePhraseCount, negWordCount, negativePhraseCount);
            Console.WriteLine("Percantage of Words Positive = " + posWordPercentage + "% " + "Negative word percentage = " + negWordPercentage + "% ");
            Console.WriteLine("Total Score : " + totalScore);

            Form1.Instance.AppendOutputText("\r\n" + fileName + "\r\n" +
              "Named entities method :" + "\r\n" +
             "Percantage of Words Positive = " + posWordPercentage + " % " + "\r\n" +
               "Percentage of words Negative = " + negWordPercentage + " % " + "\r\n" +
              "Percentage of Phrases Postive = " + posPhrasePercentage + " % " + "\r\n" +
               "Percentage of Phrases Negative = " + negPhrasePercentage + " % " + "\r\n" +
              "Words = " + wordCount + " Sentences = " + sentenceCount + "\r\n" +
               "Positive words detected = " + posWordCount + "\r\n" + "Negative words detected  = " + negWordCount + "\r\n" +
              "Postive phrases detected = " + positivePhraseCount + "\r\n" + "Negative phrases dectected = " + negativePhraseCount + "\r\n" +
               "Total Score : " + totalScore + "\r\n" +
              fileName + "-Named : processing time : " + elapsedMs + "\r\n"
              );
            //check if dontsave is ticked
            Hashtable returnTable = new Hashtable();
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc 9=pwp 10=nwp 11=npp 12=ppp 13=total 14=tt 15=method
            returnTable.Add("pw", posWordCount);
            returnTable.Add("nw", negWordCount);
            returnTable.Add("pp", positivePhraseCount);
            returnTable.Add("np", negativePhraseCount);
            returnTable.Add("wc", wordCount);
            returnTable.Add("sc", sentenceCount);
            returnTable.Add("pwp", posWordPercentage);
            returnTable.Add("nwp", negWordPercentage);
            returnTable.Add("npp", negPhrasePercentage);
            returnTable.Add("ppp", posPhrasePercentage);
            returnTable.Add("total", totalScore);
            returnTable.Add("tt", elapsedMs.ToString());
            returnTable.Add("method", "Named");


            return returnTable;

        }//end method 

       

        private Hashtable processNamedEntities(string sentence)
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
            //load the term frequency class with the methods for measuring the frequency of positive and negatiive terms
            //TermFrequency tf = new TermFrequency();
            //load spell checker
            SpellCheck sc = new SpellCheck();
            //load stemmer class
            Porter2 stemmer = new Porter2();
            //clear the snetence string at the begining of each loop
            string namedSentenceString = "";

            try
            {

                //find if the sentence contains a named entity
                if (sentence.Contains("NNP") || sc.hasNamedEntity(sentence))
                {

                    //   Console.WriteLine("sentences wrote on new lines");
                    //  Console.WriteLine(taggedSentence);               
                    List<string> words = new List<string>(sentence.Split(' '));
                    //extract the words from the sentences
                    foreach (string word in words)
                    {

                        string cleanWord = removeTag(word);
                        // Console.WriteLine(cleanWord);
                        if (cleanWord != "label")
                        {
                            //check that it isn't a stop word
                            if (!sw.isStopWord(word))
                            { 
                                //stem the word
                                cleanWord = stemmer.stem(cleanWord.ToLower());
                            //check if the word appears on the positive or negatice keyword lists                            
                            posWordCount += termFrequency.isPositiveWord(cleanWord);
                            negWordCount += termFrequency.isNegativeWord(cleanWord);
                            //ad word to string for processing as a sentence
                            namedSentenceString += cleanWord + " ";
                            //count the word added
                            wordCount++;
                            }
                        }//end if

                    }//end foreach
                    //count the senetences processed
                    sentenceCount++;
                    //check if the sentence contians a phrase
                    negativePhraseCount += termFrequency.containsNegativePhrase(namedSentenceString);
                    positivePhraseCount += termFrequency.containsPositivePhrase(namedSentenceString);

                }//end if is a named entity
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
            List<string> cleanWords = new List<string>(word.Split('_'));
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