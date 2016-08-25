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
        //https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=gild&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t
        //"https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=" +  + "+" + + "&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t
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
