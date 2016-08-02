
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

namespace StockPredictor
{
    class PosTagger

    {
        //process named and noun phrases together
        public void processNamedNoun(string articles, string fileName)
        {
            //tag the articles first
            string taggedArticles = tagArticles(articles);
            //process the named entites
            Task taskA = new Task(() => nameEntites(taggedArticles, fileName));
            //process the noun phrases
            Task taskB = new Task(() => nounPhrase(taggedArticles, fileName));
               taskA.Start();
               taskB.RunSynchronously();
                taskA.Wait();
              taskB.Wait();
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
           
            fileReaderWriter frw = new fileReaderWriter();
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
        public void nounPhrase(string article, string fileName)
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
            //load the term frequency class with the methods for measuring the frequency of positive and negatiive terms
            TermFrequency tf = new TermFrequency();
            //load the porter stemmer
            Porter2 stemmer = new Porter2();
            fileReaderWriter frw = new fileReaderWriter();
           

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
            Console.WriteLine("Percentage Positive = " + posWordPercentage + "% " + "Negative percentage = " + negWordPercentage + "% ");

            //get the date time to insert into the excel sheet
            string dt = DateTime.Now.ToString();
            //add the output data to an excel file
            ExcelMethods em = new ExcelMethods();
           // em.saveDataToExcel(fileName, "noun", elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
           //posWordPercentage, negWordPercentage,
           // positivePhraseCount, negativePhraseCount,
           // posPhrasePercentage, negPhrasePercentage);
            //add the data to special excel file for only this specific out put for this stock
          
            em.saveDataToExcel(fileName, "Noun", elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
          posWordPercentage, negWordPercentage,
           positivePhraseCount, negativePhraseCount,
           posPhrasePercentage, negPhrasePercentage);
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
            //load the term frequency class with the methods for measuring the frequency of positive and negatiive terms
            TermFrequency tf = new TermFrequency();
            //load spell checker
            SpellCheck sc = new SpellCheck();
            //load stemmer class
            Porter2 stemmer = new Porter2();
            //clear the snetence string at the begining of each loop
            string nounSentenceString = "";
            try
            {
               
                // Console.WriteLine("sentences wrote on new lines");
                // Console.WriteLine(Sentence.listToString(taggedSentence, false));
                //check that the sentence string contains only one sentence
               // Console.WriteLine(taggedSentence);
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
                            posWordCount += tf.isPositiveWord(cleanWord);
                            negWordCount += tf.isNegativeWord(cleanWord);
                            nounSentenceString += cleanWord + " ";
                            //count the words
                            wordCount++;
                        }
                    }//end if
                }//end foreach
                //count the senetences
                sentenceCount++;
                //check if the sentence contians a phrase
                negativePhraseCount += tf.containsNegativePhrase(nounSentenceString);
                positivePhraseCount += tf.containsPositivePhrase(nounSentenceString);
            
        }//end try
            catch(Exception ex) { Console.WriteLine(ex.Message); }
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
        public void nameEntites(string article, string fileName)
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
            //load the term frequency class with the methods for measuring the frequency of positive and negatiive terms
            TermFrequency tf = new TermFrequency();
            fileReaderWriter frw = new fileReaderWriter();
           
            //load the porter stemmer
            Porter2 stemmer = new Porter2();
            List<ArrayList> nounPhrases = new List<ArrayList>();
            ArrayList namedSentence = new ArrayList();
          
            try {
              
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
            Console.WriteLine("Percentage Positive = " + cm.getPositivePercentage(posWordCount, negWordCount) + "% " + "Negative percentage = " + cm.getNegativePercentage(posWordCount, negWordCount) + "% ");
            //add the output data to an excel file
            ExcelMethods em = new ExcelMethods();
           // em.saveDataToExcel(fileName, "named", elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
           //posWordPercentage, negWordPercentage,
           // positivePhraseCount, negativePhraseCount,
           // posPhrasePercentage, negPhrasePercentage);
            //add the data to special excel file for only this specific out put for this stock          
            em.saveDataToExcel(fileName, "Named", elapsedMs.ToString(), wordCount, sentenceCount, posWordCount, negWordCount,
          posWordPercentage, negWordPercentage,
           positivePhraseCount, negativePhraseCount,
           posPhrasePercentage, negPhrasePercentage);
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
            TermFrequency tf = new TermFrequency();
            //load spell checker
            SpellCheck sc = new SpellCheck();
            //load stemmer class
            Porter2 stemmer = new Porter2();
            //clear the snetence string at the begining of each loop
            string namedSentenceString = "";
         
            try {
               
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
                            //stem the word
                            cleanWord = stemmer.stem(cleanWord.ToLower());
                            //check if the word appears on the positive or negatice keyword lists                            
                            posWordCount += tf.isPositiveWord(cleanWord);
                            negWordCount += tf.isNegativeWord(cleanWord);
                            //ad word to string for processing as a sentence
                            namedSentenceString += cleanWord + " ";
                            //count the word added
                            wordCount++;
                        }//end if

                }//end foreach
                    //count the senetences processed
                    sentenceCount++;
                    //check if the sentence contians a phrase
                    negativePhraseCount += tf.containsNegativePhrase(namedSentenceString);
                    positivePhraseCount += tf.containsPositivePhrase(namedSentenceString);

                }//end if is a named entity
            }//end try
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            //hash table 1=pw 2=nw 3=spw 4=snw 5=pp 6=np 7=wc 8=sc
            ht.Add("pw",posWordCount);
            ht.Add("nw",negWordCount);
            ht.Add("pp",positivePhraseCount);
            ht.Add("np",negativePhraseCount);
            ht.Add("wc", wordCount);
            ht.Add("sc",sentenceCount);
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

        //public void paraTest()
        //{
        //    try
        //    {
        //        ParallelOptions po = new ParallelOptions();              
        //        //Manage the MaxDegreeOfParallelism instead of .NET Managing this. We dont need 500 threads spawning for this.
        //        po.MaxDegreeOfParallelism = System.Environment.ProcessorCount * 2;
        //        try
        //        {
        //            Parallel.ForEach(data, po, (object dataobject) =>
        //            {
        //                try
        //                {

        //                }
        //                catch { }
        //            });
        //        }
        //        catch { }
        //    }
        //    catch { }

        //}
    }//end class
}//end name space