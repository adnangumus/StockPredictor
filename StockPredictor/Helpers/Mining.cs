using HtmlAgilityPack;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class Mining
    {
     

        //get the news article from the URLS 
        public string getArticle(string url)
            {
            Console.WriteLine("started retrieving " + url);
             //load classes 
                TextCleaner tc = new TextCleaner();
                string Text = "";              
                try
                {
                //use the htmlhelper class to load the urls
                HtmlHelper hh = new HtmlHelper();
                HtmlAgilityPack.HtmlDocument doc = hh.loadURL(url);
                //  HtmlAgilityPack.HtmlDocument doc = hw.Load(url);                
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//p"))
                    {

                        //remove the non letters from the text
                        string str = tc.removeNonLetters(node);
                        Text += str;
                    }

                }
                catch (Exception ex) { Console.WriteLine("get article exception :" + ex.Message + " " + url); }

            Console.WriteLine("finished retrieving " + url);
            return Text;
            }

            //get the articles using a list of links
            public string getAllArticles(List<string> links)
            {
                //SpellChecker class
                SpellCheck sc = new SpellCheck();
               
               
            fileReaderWriter frw = new fileReaderWriter();

                string allArticles = "";
                string articles = "";
                string article = "";
                try
                {
               
                    //remove duplicate links
                    List<string> distinctLinks = links.Distinct().ToList();
                //display amount of links in the text box
                Form1.Instance.AppendOutputText("\r\n" + "Total links : " + links.Count + "\r\n");
                Form1.Instance.AppendOutputText("\r\n" + "Distinct links : " + distinctLinks.Count + "\r\n");
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
                //out put information to text box   
                Form1.Instance.AppendOutputText("\r\n");
                Form1.Instance.AppendOutputText("Parallel Text retrival time : " + elapsedMs + "\r\n");
                    //  tc.removePunctuation(allArticles)
                    articles = sc.spellCheckArticle(allArticles);

                }
                catch (Exception ex) { Console.WriteLine("get all articles : " + ex.Message); }
                return articles;
            }
        }

        }
