using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class YahooMiner
    {
       

        public List<string> getYahoolinks(string url)
        {
            List<string> links = new List<string>();
            //use the htmlhelper class to load the urls
            // HtmlHelper hh = new HtmlHelper();
            // HtmlAgilityPack.HtmlDocument doc = hh.loadURL(url);
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(url);
            try
            {
                string linkValue = "";

                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    // Get the value of the HREF attribute
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    // Console.WriteLine(hrefValue);
                    if (hrefValue.Contains("http") && !hrefValue.Contains("r.msn"))
                    {
                        string yahooLink = processLink(hrefValue);
                        if(!String.IsNullOrEmpty(yahooLink))
                        { 
                        links.Add(yahooLink);
                        Console.WriteLine("");
                        Console.WriteLine("");
                        Console.WriteLine("Yahoo link : " + yahooLink);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return links;

        }

        public string processLink(string link)
        {
            string decodeUrl;
            //split the links from yahoo search link
            link = link.Replace("http", "|");
            string[] urls = link.Split('|');

            foreach (string url in urls)
            {
               // Console.WriteLine(url);
                string decodedUrl = Uri.UnescapeDataString(url);
              //  Console.WriteLine(decodedUrl);
                //get the link and remove the end
                if (!decodedUrl.Contains("search.yahoo") && !decodedUrl.Contains("help.yahoo") 
                    && !decodedUrl.Contains("login.yahoo")&& !decodedUrl.Contains("mail.yahoo")
                    && !decodedUrl.Contains("mozilla") && !decodedUrl.Contains("advertising.yahoo") && !decodedUrl.Contains("info.yahoo")
                     
                    )   
                {
                  //  Console.WriteLine(decodedUrl);
                  //  Console.WriteLine("");
                    //reattach the http to the url
                   
                    if(decodedUrl.Contains("s://")) { link = decodedUrl.Replace("s://", "https://");  }
                    else { link = decodedUrl.Replace("://", "http://"); }
                    
                    // ... This will separate all the words.
                    if (link.Contains("?source=")) { decodedUrl = link.Replace("?source", "|"); }
                    if (link.Contains("?puc")) { decodedUrl = link.Replace("?puc", "|"); }
                    if(link.Contains("RK=")) { decodedUrl = link.Replace("RK=", "|"); }
                    string[] realUrls = decodedUrl.Split('|');
                    foreach (string realUrl in realUrls)
                    {
                        if (realUrl.Contains("http") && !realUrl.Contains("yahoo.com"))
                        {
                            // Console.WriteLine("");
                            // Console.WriteLine(realUrl);
                            return realUrl;
                        }
                    }
                }

            }
            //return the link
            return null;
        }
    }
}
