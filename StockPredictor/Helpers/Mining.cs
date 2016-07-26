using HtmlAgilityPack;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class Mining
    {
            //get the news article from the URLS 
            public string getArticle(string url)
            {
              
                TextCleaner tc = new TextCleaner();
                string Text = "";
                HtmlWeb hw = new HtmlWeb();
               
                try
                {
                HtmlAgilityPack.HtmlDocument doc = hw.Load(url);
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//p"))
                    {

                        //remove the non letters from the text
                        string str = tc.removeNonLetters(node);
                        Text += str;
                        //  //turn all text to lower case
                        //  Text.ToLower();
                        //  //remove non words and stop words
                        //swText= sw.removeStopWords(Text);

                    }

                }
                catch (Exception ex) { Console.WriteLine("get article line 37 :" + ex.Message); }
            
                return Text;
            }

            //get the articles using a list of links
            public string getAllArticles(List<string> links)
            {
                //SpellChecker class
                SpellCheck sc = new SpellCheck();
               
                //load the pos tagger class
                TextCleaner tc = new TextCleaner();
            fileReaderWriter frw = new fileReaderWriter();

                string allArticles = "";
                string articles = "";
                string article = "";
                try
                {
                    //remove duplicate links
                    List<string> distinctLinks = links.Distinct().ToList();
                    //start a stop watch to time method
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    //get the text from the articles using the links
                    Parallel.ForEach(distinctLinks,
                    new ParallelOptions { MaxDegreeOfParallelism = 30 }, link =>
                    {
                        Console.WriteLine(link);
                        article = getArticle(link);
                       // Console.WriteLine(article);
                        allArticles += article;

                    });
                    //stop the watch to time method
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("Parallel Text retrival time : " + elapsedMs);                
                    //  tc.removePunctuation(allArticles)
                    articles = sc.spellCheckArticle(allArticles);

                }
                catch (Exception ex) { Console.WriteLine("get all articles : " + ex.Message); }
                return articles;
            }
        }

        }
