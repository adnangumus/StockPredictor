using HtmlAgilityPack;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                string text = "";              
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
                        text += str;
                    }

                }
                catch (Exception ex) { Console.WriteLine("get article exception :" + ex.Message + " " + url); return text; }

            Console.WriteLine("finished retrieving " + url);
            return text;
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
            //remove duplicate links
            List<string> distinctLinks = links.Distinct().ToList();
            //add the old links in a repeat execution to distingush breaking news
            if (Form1.Instance.isRepeat() || Form1.Instance.repeatData.RepeaterIsRunning )
            {
                distinctLinks = CompareLinkLists(distinctLinks);
            }
            try
                {
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
                Form1.Instance.AppendOutputText("Parallel Text retrival time : " + elapsedMs + "\r\n" + "\r\nPlease wait.... This can take a few minutes");
                    //  tc.removePunctuation(allArticles)
                    articles = sc.spellCheckArticle(allArticles);

                }
                catch (Exception ex) { Console.WriteLine("get all articles : " + ex.Message); }                   
                return articles;
            }

        //check if the links are the same and remove the links that were present in the previous search
        private List<string> CompareLinkLists(List<string> links)
        {
            List<string> oldLinks = Form1.Instance.repeatData.LinksOld;
            List<string> newLinks = new List<string>();
            try
            {
                //check if there is a record of older links

                newLinks = links.Except(oldLinks).ToList();
                //add the new links to the linksold list
                if (newLinks.Count() > 0) { Form1.Instance.repeatData.LinksOld.AddRange(newLinks); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                    Console.WriteLine("There are no old links stored");
                //if there isn't older links just make the new links the current links
                newLinks = links;
                Form1.Instance.repeatData.LinksOld = newLinks;
            }         
            return newLinks;
        }


        //http://stackoverflow.com/questions/29302259/htmlagility-pack-screen-scraping-unable-to-find-a-div-with-hyphen-in-class-name
        //To parse such pages where javascript need to be executed first, for that you could use a web browser control and then pass the html to HAP.
        public void ScrapLivePricesFromBing(string link)
        {
            string text ="";
            var browser = new System.Windows.Forms.WebBrowser() { ScriptErrorsSuppressed = true };

            //This will be called when the web page loads, it better be a class member since this is just a simple demonstration
            WebBrowserDocumentCompletedEventHandler onDocumentCompleted = new WebBrowserDocumentCompletedEventHandler((s, evt) => {
                //Do your HtmlParsingHere
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(browser.DocumentText);

                foreach (HtmlNode div in doc.DocumentNode.SelectNodes("//div[contains(@class,'b_focusTextMedium')]"))
                {
                    //remove the non letters from the text
                    string str = div.InnerText;
                    text += str;
                    Console.WriteLine(str);

                }
            });
            
            //subscribe to the DocumentCompleted event using our above handler before navigating
            browser.DocumentCompleted += onDocumentCompleted;

            browser.Navigate(link);
          
        }

        //run the browser thread for getting the live results
        public void RunBrowserThread(string url)
        {
            var th = new Thread(() => {
                var br = new WebBrowser();
                br.DocumentCompleted += browser_DocumentCompleted;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                Console.WriteLine("Natigated to {0}", e.Url);
                //Do your HtmlParsingHere
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(br.DocumentText);
                foreach (HtmlNode div in doc.DocumentNode.SelectNodes("//div[contains(@class,'b_focusTextMedium')]"))
                {
                    //remove the non letters from the text
                    string str = div.InnerText;
                  
                    Console.WriteLine(str);

                }
                Application.ExitThread();   // Stops the thread
            }
        }
    }

}
