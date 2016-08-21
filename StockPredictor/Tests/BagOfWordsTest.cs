using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPredictor.Helpers;

namespace StockPredictor.Tests
{
    class BagOfWordsTest
    {
        string badText = "Shares of Westport Innovations (NASDAQ:WPRT) have fallen considerably since I recommended selling the stock earlier in June. In the last five months, Westport’s stock has depreciated almost 45% in value. Value investors may be tempted to buy into Westport due to the massive plunge; however, the stock still has more downside. Westport is far from a bottom and there are several factors that can push the stock down further. " +
"The company’s struggle is evident from its latest quarterly result.The company reported loss per share of 58 cents on revenue of $22.3 million.Analysts were modeling Westport to report losses of 26 cents per share on revenue of $28 million. " +
"Westport Innovations is a company that develops alternative fuel, low-emissions technologies to allow engines to operate on clean-burning fuels such as compressed natural gas, liquefied natural gas, hydrogen and biofuels such as landfill gas. " +
"Warning! GuruFocus has detected 3 Warning Signs with WPRT. Click here to check it out."+
"WPRT 15-Year Financial Data "+
"The intrinsic value of WPRT Peter Lynch Chart of WPRT With natural gas prices near three-year lows, the sales of Westport’s engines were expected to increase this year.However, oil prices have plunged further.As a result, consumers are inclined towards using oil as a fuel source. "+
"The plunge in oil price has definitely hurt Westport Innovations and the company’s misery isn’t expected to end soon.According to Energy Information Administration’s(EIA) latest forecasts, oil prices are expected to rise very slowly in the years to come.According to the IEA’s factsheet, the agency sees oil prices reaching $80 per barrel in 2020 in their New Policies Scenario, the World Energy Outlook 2015’s central scenario. "+
"With oil prices expected to remain low for years to come, I can see Westport’s struggling further.The company has managed to reduce its losses by cutting-costs, but is still in troubled waters. The company only has $42 million in cash as opposed to a debt of $66 million.With the company losing money and reporting massive losses despite significant cost-cutting, I don’t think it can stay aflot in the long-haul. "+
"Oil prices are expected to remain low for years to come and Westport may not have enough cash to endure the negative effects of cheaper oil. Given the high cost of natural-gas engines, Westport’s business will continue to struggle for many years. Hence, the chances of the company going bankrupt are really high and investors should stay away from the stock. "+
"Conclusion With the oil prices expected to stay low for years to come, sales of Westport’s engines will continue to decline. Consumers will prefer the traditional diesel trucks that are a lot cheaper than natural gas trucks. Westport’s sales will continue to fall in the near future.Moreover, the company doesn’t have enough cash to live through the oil crash and will likely go bankrupt way before it. Given the risks, investors should still stay far away from the stock. Positive position";
        // Text for tagging
    string text = "Is Gileard a piece of software that reads text or has it increased earnings after a major acquisition?"
                   + "In some language and assigns parts of speech to each word (and other token),"
                   + " such as noun, verb, adjective, etc., although generally computational and disastrous after the credit crunch."
                   + "applications use more fine-grained POS tags like 'noun-plural'."
                   + "Here is a new sentence to be scanned for sintax with GILD."
            + "Gileard best performing stock this year, with a significant decrease in short interest."
                    + "Here is a new sentence to be scanned for sintax with IBB."
         + "Here is a new sentence to be scanned for sintax with NASDAQ.";

        string text2 = "Is Gileard a piece of software that reads text or has it increased earnings after a major acquisition?"
                  + "In some language and assigns parts of speech to each word (and other token), it could be disastrous";
        //public void getSentenceTest()
        //{
        //    BagOfWords bg = new BagOfWords();
        //    Mining miner = new Mining();
        //    List<string> ms = new List<string>();
        //    ms = bg.getSentences(text);
        //    foreach (string m in ms)
        //    {
        //        Console.WriteLine(m);
        //    }
        //}

        public void getSentenceBagArrayTest()
        {
            BagOfWords bg = new BagOfWords();
          
            Mining miner = new Mining();
          
            bg.processBagOfWords(text2, "exampleExcel1", true);
           // int sentences = counter.countSentenceCollection(collection);
           // int words = counter.countWordCollection(collection);
           // Console.WriteLine("There are " + sentences + " sentences in the bag test");
            //Console.WriteLine("There are " + words + " words in the bag test");
        }

        public void processBagOfWordsTest()
        {
            BagOfWords bg = new BagOfWords();
            bg.processBagOfWords(badText, "exampleExcel1", true);
        }
      
    }
}
