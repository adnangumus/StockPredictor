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
      //this class will get html data and save guard against time outs
        public static string GetURLData(string URL)
        {           
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                request.UserAgent = "jonh86";
                request.Timeout = 12000;
                WebResponse response = request.GetResponse();                
                Stream stream = response.GetResponseStream();
                stream.ReadTimeout = 15000;
                StreamReader reader = new StreamReader(stream);
                    Console.WriteLine("reader peak " + reader.Peek() );
                    if (reader.Peek() >= 0)
                    { return reader.ReadToEnd(); }      
                
                throw new System.Net.WebException();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Receive DATA Error : " + URL + ex.ToString());
                return "";
            }

        }


        //get the news article from the URLS 
        public string getArticle(string url)
            {
            Console.WriteLine("started retrieving " + url);
             //load classes 
                TextCleaner tc = new TextCleaner();
                string Text = "";
                HtmlWeb hw = new HtmlWeb();
            // count the number of retries
            int retries = 0;              
                try
                {
                //get the data and retry if it times out
                Console.WriteLine("getting url data");
                String Data = GetURLData(url);
                //if there was an error then the system will retry up to three times
                while (Data == "" && retries < 3)
                {
                    Console.WriteLine("retrying");
                    retries++;
                    Data = GetURLData(url);
                }               
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                Console.WriteLine("loading data to HTML agility pack");
                doc.LoadHtml(Data);
              //  HtmlAgilityPack.HtmlDocument doc = hw.Load(url);                
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//p"))
                    {

                        //remove the non letters from the text
                        string str = tc.removeNonLetters(node);
                        Text += str;
                    }

                }
                catch (Exception ex) { Console.WriteLine("get article exception :" + ex.Message); }

            Console.WriteLine("finished retrieving " + url);
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
