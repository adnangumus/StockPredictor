using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Helpers
{
    class HtmlHelper
    {
        //this class will get html data and save guard against time outs
        public static string GetURLData(string URL)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                request.UserAgent = "jonh86";
                request.Timeout = 6000;
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                //add a stream read time out to prevent hanging threads
                stream.ReadTimeout = 10000;
                StreamReader reader = new StreamReader(stream);
                Console.WriteLine("reader peak " + reader.Peek());
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
        //load the data to the htmlAgilityPack variable and return it
        public HtmlAgilityPack.HtmlDocument loadURL(string url)
        {
            int retries = 0;
            HtmlWeb hw = new HtmlWeb();
            String Data = GetURLData(url);
            //if there was an error then the system will retry up to three times
            while (Data == "" && retries < 3)
            {
                Console.WriteLine("retrying");
                retries++;
                Data = GetURLData(url);
            }
            try { 
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            Console.WriteLine("loading data to HTML agility pack");
            doc.LoadHtml(Data);
                return doc;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Receive DATA Error : " + url + ex.ToString());
                return null;
            }

        }
    }
}
