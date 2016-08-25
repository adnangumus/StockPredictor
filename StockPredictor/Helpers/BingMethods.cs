using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class BingMethods
    {
        //add to make english  &intlF=1&FORM=TIPEN1
        // http://cn.bing.com/news/search?q=gild+Gilead&qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1
        //"http://cn.bing.com/news/search?q="+ + "+" +  + &qft=interval%3d%227%22&form=PTFTNR&intlF=1&FORM=TIPEN1"
        //get the links from the google url
        public List<string> getBinglinks(string url)
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
                    if (hrefValue.Contains("http") && !hrefValue.Contains("microsoft") && !hrefValue.Contains("sina") && !hrefValue.Contains("qq")
                        && !hrefValue.Contains("bioon") && !hrefValue.Contains("wiki") && !hrefValue.Contains("bing.com"))
                    {
                        linkValue = hrefValue;
                        Console.WriteLine(linkValue);                    
                      links.Add(linkValue);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return links;

        }

      
    }
}


