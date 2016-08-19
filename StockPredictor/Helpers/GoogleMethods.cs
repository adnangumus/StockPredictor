using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using StockPredictor.Helpers;

namespace StockPredictor
{
    class GoogleMethods
    {
       

       
        //get the news summaries from google news 
        public string getGoogleSummaries(string url)
        {
           
            TextCleaner tc = new TextCleaner();
            string googleText = "";
            //use the htmlhelper class to load the urls
            HtmlHelper hh = new HtmlHelper();
            HtmlAgilityPack.HtmlDocument doc = hh.loadURL(url);
            try
            {
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[contains(@class,'st')]"))
                {

                    //remove the non letters from the text
                    string str = tc.removeNonLetters(node);
                    googleText += str;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return googleText.ToLower();
        }

        //get the links from the google url
        public List<string> getGooglelinks(string url)
        {
            
            List<string> links = new List<string>();
            //use the htmlhelper class to load the urls
            HtmlHelper hh = new HtmlHelper();
            HtmlAgilityPack.HtmlDocument doc = hh.loadURL(url);
            try {
             
                string linkValue = "";
               
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    // Get the value of the HREF attribute
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (hrefValue.Contains("/url?"))
                    {
                        linkValue = hrefValue;
                       // Console.WriteLine("https://www.google.co.jp" + linkValue);
                       //add the prefix to the link so it will work
                        string newUrl = "https://www.google.co.jp" + linkValue;
                        //the link is always redirected, this method returns the correct link
                        links.Add(redirectLink(newUrl));
                    }

                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message); }

            return links;
        }

        //the inks retrieved from the seach page will lead to a redirect page. 
        //This methods gets the correct link from the redirect page
        public string redirectLink(string url)
        {
            string redirectLink = "";
            //use the htmlhelper class to load the urls
            HtmlHelper hh = new HtmlHelper();
            HtmlAgilityPack.HtmlDocument doc = hh.loadURL(url);
            try
            {
                
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    {
                        string hrefValue = link.GetAttributeValue("href", string.Empty);
                        if (hrefValue == "#") { }
                        else{ 
                        redirectLink = hrefValue;
                       // Console.WriteLine(hrefValue);
                        }
                    }
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return redirectLink;
            }
    }
    }


